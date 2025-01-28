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


        /// <summary>
        /// Enters conduction mode
        /// </summary>
        public void Enter(Conductive con)
        {
            // Ensure that the correct conductive material is hit
            if(!Physics.Raycast(fpCam.transform.position, fpCam.transform.forward, out hit, Mathf.Infinity)
                || hit.collider.gameObject != con.gameObject) return;
            enabled = true;
            rigidbody.isKinematic = true;
            transform.position = hit.point;
            playerMovement.enabled = false;
            // Face away from surface
            transform.up = hit.normal;
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
            transform.Translate(dir * moveSpeed * Time.deltaTime);
        }

        private void HandleCamera()
        {
            tpCam.transform.forward = transform.up * -1;
            tpCam.transform.localPosition = Vector3.zero;
            tpCam.transform.Translate(Vector3.forward * -camDist);
        }
        private void Update()
        {
            HandleCamera();
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
    }
}
