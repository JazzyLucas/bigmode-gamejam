using UnityEngine;

namespace BigModeGameJam
{
    public class DroneBladeRotate : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 360f; // Degrees per second, default one full rotation

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
        }
    }
}
