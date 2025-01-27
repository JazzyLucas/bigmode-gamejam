using BigModeGameJam.Level.Interactables;
using UnityEngine;

namespace BigModeGameJam.Level.Controls
{
    /// <summary>
    /// Handles mode of electric character when conducting through metal
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ElectricMode : MonoBehaviour
    {
        public Camera fpCam;
        public ThirdPersonCamera tpCam;
        public float exitDist = 10;
        
        new private Collider collider; 

        /// <summary>
        /// Enters conduction mode
        /// </summary>
        public void Enter(Conductive con)
        {
            RaycastHit hit;
            // Ensure that the correct conductive material is hit
            if(!Physics.Raycast(fpCam.transform.position, fpCam.transform.forward, out hit, Mathf.Infinity)
                || hit.collider.gameObject != con.gameObject) return;
            transform.position = hit.point;
            // Face away from surface
            transform.forward = hit.normal;
            // We handle the camera differently
            fpCam.enabled = false;
            tpCam.enabled = false;
            tpCam.gameObject.SetActive(true);
            collider.enabled = false;

        }

        // Exits conductive mode
        public void Exit()
        {
            //transform.Translate(Vector3.forward)
        }

        private void Awake()
        {
            collider = GetComponent<Collider>();
        }
    }
}
