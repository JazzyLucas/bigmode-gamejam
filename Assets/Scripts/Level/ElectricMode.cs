using BigModeGameJam.Level.Interactables;
using UnityEngine;

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
        private const float RAYCAST_PADDING = 0.25f;

        public float exitDist = 10;
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
            if(!Physics.Raycast(fpCam.transform.position, fpCam.transform.forward, out hit, Mathf.Infinity)
                || hit.collider.gameObject != con.gameObject) return;
            targetConductor = con;
            enabled = true;
            rigidbody.isKinematic = true;
            playerMovement.enabled = false;
            LockToPlane();
            // We handle the third person camera locally
            fpCam.gameObject.SetActive(false);
            tpCam.enabled = false;
            tpCam.gameObject.SetActive(true);
            collider.enabled = false;
            lookToInteract.lookingAt = con;
            lookToInteract.enabled = false;
        }

        // Exits conductive mode
        public void Exit(bool jump = false)
        {
            enabled = false;
            rigidbody.isKinematic = false;
            collider.enabled = true;
            transform.Translate(Vector3.up * exitDist);
            transform.up = Vector3.up;
            playerMovement.enabled = true;
            // Go back to first person
            tpCam.gameObject.SetActive(false);
            tpCam.enabled = true;
            fpCam.gameObject.SetActive(true);
            lookToInteract.lookingAt = null;
            lookToInteract.enabled = true;
            playerRefs.dash.Replenish();
            if(jump)
                playerMovement.Jump();
        }

        public void Move(Vector3 dir)
        {
            transform.position += moveSpeed * Time.deltaTime * 
                Vector3.ProjectOnPlane(CamRelativeDirection(dir), hit.normal);
            if(!Physics.Raycast(transform.position + transform.up * RAYCAST_PADDING, -transform.up, out hit, Mathf.Infinity)
                || hit.collider.gameObject != targetConductor.gameObject) 
            {
                Exit();
            }
            LockToPlane();
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

        private void HandleCamera()
        {
            tpCam.transform.forward = transform.up * -1;
            tpCam.transform.localPosition = Vector3.zero;
            tpCam.transform.Translate(Vector3.forward * -camDist);
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
        }
    }
}
