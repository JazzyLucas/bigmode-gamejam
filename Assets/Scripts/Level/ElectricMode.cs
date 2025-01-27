using BigModeGameJam.Level.Interactables;
using UnityEngine;

namespace BigModeGameJam.Level.Controls
{
    /// <summary>
    /// Handles mode of electric character when conducting through metal
    /// </summary>
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerMovement))]
    public class ElectricMode : MonoBehaviour
    {
        public Camera fpCam;
        public ThirdPersonCamera tpCam;
        public float exitDist = 10;
        public float camDist = 25;
        
        new private Collider collider; 
        new private Rigidbody rigidbody;
        private PlayerMovement playerMovement;

        /// <summary>
        /// Enters conduction mode
        /// </summary>
        public void Enter(Conductive con)
        {
            RaycastHit hit;
            // Ensure that the correct conductive material is hit
            if(!Physics.Raycast(fpCam.transform.position, fpCam.transform.forward, out hit, Mathf.Infinity)
                || hit.collider.gameObject != con.gameObject) return;
            enabled = true;
            rigidbody.isKinematic = true;
            transform.position = hit.point;
            playerMovement.enabled = false;
            // Face away from surface
            transform.forward = hit.normal;
            // We handle the third person camera locally
            fpCam.gameObject.SetActive(false);
            tpCam.enabled = false;
            tpCam.gameObject.SetActive(true);
            collider.enabled = false;
        }

        // Exits conductive mode
        public void Exit(bool jump = false)
        {
            enabled = false;
            rigidbody.isKinematic = false;
            transform.Translate(Vector3.forward * exitDist);
            playerMovement.enabled = true;
            // Go back to first person
            tpCam.gameObject.SetActive(false);
            tpCam.enabled = true;
            fpCam.gameObject.SetActive(true);
            if(jump)
                playerMovement.Jump();

        }

        private void HandleCamera()
        {
            fpCam.transform.forward = transform.forward * -1;
            fpCam.transform.Translate(Vector3.back * camDist);
        }
        private void Update()
        {
            HandleCamera();
        }
        private void Awake()
        {
            enabled = false; // In case we forget to disable in editor
            collider = GetComponent<Collider>();
            rigidbody = GetComponent<Rigidbody>();
            playerMovement = GetComponent<PlayerMovement>();
        }
    }
}
