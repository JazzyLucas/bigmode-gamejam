using BigModeGameJam.Core;
using BigModeGameJam.Level.Interactables;
using BigModeGameJam.UI;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.UIElements;
using static BigModeGameJam.Level.Controls.PlayerMovement;

namespace BigModeGameJam.Level.Controls
{
    /// <summary>
    /// Handles mode of electric character when conducting through metal.
    /// Disabled when not inside conductive material
    /// </summary>
    [RequireComponent(typeof(PlayerRefs))]
    public class ElectricMode : MonoBehaviour
    {

        /// <summary>
        /// Used to detect an object even if the player is percisely on the surface of it.
        /// </summary>
        private const float RAYCAST_PADDING = 1.75f;
        /// <summary>
        /// Maximum distance that the player can "zip" to in a single frame when traversing a concave shape.
        /// </summary>
        private const float MAX_ZIP_DIST = 100;

        public float exitDist = 5;
        public float exitImpulse = 10;
        public float camDist = 25;
        public float moveSpeed = 5;

        private PlayerRefs playerRefs;
        new private Collider collider;
        new private Rigidbody rigidbody;
        private PlayerMovement playerMovement;
        private LookToInteract lookToInteract;
        private Camera fpCam;
        private ThirdPersonCamera tpCam;

        private RaycastHit hit;
        private Conductive targetConductor;


        /// <summary>
        /// Enters conduction mode
        /// </summary>
        public void Enter(Conductive con)
        {
            // Ensure that the correct conductive material is hit
            if (!Physics.Raycast(fpCam.transform.position, fpCam.transform.forward, out hit, Mathf.Infinity)
                || hit.collider.gameObject != con.gameObject) return;
            con.Unhover();
            targetConductor = con;
            enabled = true;
            rigidbody.isKinematic = true;
            playerMovement.enabled = false;
            playerRefs.dash.enabled = false;
            LockToPlane();
            playerRefs.ThirdPerson();
            // We handle the third person camera locally
            tpCam.enabled = false;
            collider.enabled = false;
            // Lock interactable so that "E" will exit
            lookToInteract.SetInteractable(con);
            lookToInteract.lockInteractable = true;
            // Be the ball
            playerRefs.orb.SetActive(true);
            playerRefs.playerModel.SetActive(false);
            con.Unhover();
        }

        // Exits conductive mode
        public void Exit(bool jump = false)
        {
            enabled = false;
            rigidbody.isKinematic = false;
            rigidbody.linearVelocity += transform.up * exitImpulse;
            collider.enabled = true;
            transform.Translate(Vector3.up * exitDist);
            Vector3 tempUp = transform.up;
            transform.up = Vector3.up;
            transform.forward = new Vector3(tempUp.x, 0, tempUp.z); // Look away from surface
            playerMovement.enabled = true;
            playerRefs.FirstPerson();
            tpCam.enabled = true;
            fpCam.transform.localRotation = Quaternion.Euler(Vector3.zero); // Look straight ahead
            // Unlock interactable - back to normal
            lookToInteract.lockInteractable = false;
            lookToInteract.SetInteractable(null);
            lookToInteract.enabled = true;
            playerRefs.dash.Replenish();
            if (jump)
                playerMovement.Jump(jump);
            playerRefs.dash.enabled = true;
            // (don't) Be the ball
            playerRefs.orb.SetActive(false);
            playerRefs.playerModel.SetActive(true);
            targetConductor.Unhover();
        }

        public void Move(Vector3 dir)
        {
            // Invert x input when in first person. it makes sense trust
            if (fpCam.gameObject.activeInHierarchy)
            {
                dir = new Vector3(-dir.x, dir.y, dir.z);
            }
            transform.position += moveSpeed * Time.deltaTime *
                Vector3.ProjectOnPlane(CamRelativeDirection(dir), hit.normal);
            if (LeftConductive())
            {
                Exit();
                return;
            }
            LockToPlane();
        }

        private EventInstance ElectricMove;
        private void Start()
        {
            ElectricMove = AudioManager.instance.CreateEventInstance(FMODEvents.instance.ElectricMove);
        }

        private void Awake()
        {
            enabled = false; // In case we forget to disable in editor
            playerRefs = GetComponent<PlayerRefs>();
            collider = playerRefs.collider;
            rigidbody = playerRefs.rigidbody;
            playerMovement = playerRefs.playerMovement;
            lookToInteract = playerRefs.lookToInteract;
            fpCam = playerRefs.firstPersonCam;
            tpCam = playerRefs.thirdPersonCam;
        }

        /// <summary>
        /// Rotates input direction to a "top down" direction relative to camera
        /// </summary>
        /// <param name="dir">2D Input direction on xz plane</param>
        /// <returns></returns>
        private Vector3 CamRelativeDirection(Vector3 dir)
        {
            return tpCam.transform.right * dir.x + tpCam.transform.up * dir.z;
        }

        /// <summary>
        /// Returns true if the player has left the conductive material and it's connected objects
        /// </summary>
        /// <returns></returns>
        private bool LeftConductive()
        {
            // There is no material under player. They have definitely left
            if (!Physics.Raycast(transform.position + transform.up * RAYCAST_PADDING, -transform.up, out hit, Mathf.Infinity))
            {
                return true;
            }
            // The player is still connected to the same Conductive object
            if (hit.collider.gameObject == targetConductor.gameObject)
            {
                return false;
            }
            // If the player has left the target object but hit a new object
            // see if new object is connected
            Conductive newTarget = hit.collider.gameObject.GetComponent<Conductive>();
            if (targetConductor.IsConnected(newTarget))
            {
                targetConductor = newTarget;
                return false;
            }
            // Player has hit a conductor that is not a part of the intended group. Leave.
            return true;

        }

        private void HandleCamera()
        {
            tpCam.transform.forward = transform.up * -1;
            tpCam.transform.localPosition = Vector3.zero;
            if (Physics.Raycast(transform.position, transform.up, out hit, camDist))
            {
                // "collide" with surface
                tpCam.transform.position = hit.point;
                return;
            }
            tpCam.transform.Translate(Vector3.forward * -camDist);
            fpCam.transform.LookAt(tpCam.transform);
        }

        private void LockToPlane()
        {
            transform.position = hit.point;
            // Face away from surface
            transform.up = hit.normal;
        }

        private void Update()
        {
            HandleCamera();
            UpdateSound();
        }

        private void UpdateSound()
        {
            //start footsteps event if player has an x velocity and is on the ground
            if (playerRefs.orb == isActiveAndEnabled)
            {
                //get playback state
                PLAYBACK_STATE playbackState;
                ElectricMove.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
                {
                    ElectricMove.start();
                }

            }
            //otherwise, stop the footsteps event
            else
            {
                ElectricMove.stop(STOP_MODE.IMMEDIATE);
            }
        }
    }
}
