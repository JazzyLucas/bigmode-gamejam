using BigModeGameJam.Level;
using BigModeGameJam.Level.Controls;
using UnityEngine;

namespace BigModeGameJam
{
    [RequireComponent(typeof(Rigidbody))]
    public class ElectricBallScript : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 10f;
        public float rotationSpeed = 2f;
        public bool invertGravity = false;
        public float gravityStrength = 20f;
        float tiltValue = -5f;
        float backValue = 6.5f;
        float upValue = 4f;
        float lookAtValue = 2f;

        private Rigidbody rb;
        private Transform cameraTransform;
        private float horizontalRotation = 0f;
        private Vector3 moveDirection;

        public GameObject thirdPersonCam;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = false; // Allow physics-based rotation
            
            // Create and setup camera
            cameraTransform = thirdPersonCam.transform;
            Camera cam = thirdPersonCam.GetComponent<Camera>();
            if (!cam)
            {
                cam = thirdPersonCam.AddComponent<Camera>();
            }
            cam.transform.position = transform.position - transform.forward * 30f;
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
            UpdateCamera();
        }

        void FixedUpdate()
        {
            ApplyMovement();
            ApplyGravity();
        }

        void HandleInput()
        {
            // Get WASD input relative to camera orientation
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            // Calculate move direction relative to camera
            Vector3 forward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
            Vector3 right = Vector3.Cross(Vector3.up, forward);
            
            moveDirection = (forward * vertical + right * horizontal).normalized;

            // Handle mouse input for camera rotation
            horizontalRotation += Input.GetAxis("Mouse X") * PlayerControls.lookSensitivity;
        }

        void UpdateCamera()
        {
            // Position camera behind ball
            Vector3 targetPosition = transform.position;
            Quaternion rotation = Quaternion.Euler(tiltValue, horizontalRotation, 0f);
            Vector3 offset = rotation * Vector3.back * backValue + Vector3.up * upValue;
            
            // Add an upward offset to the look-at target position
            Vector3 lookAtPosition = targetPosition + (Vector3.up * lookAtValue); // Adjust the 2f value to control how high to look
            
            cameraTransform.position = targetPosition + offset;
            cameraTransform.LookAt(lookAtPosition);
        }

        void ApplyMovement()
        {
            if (moveDirection.magnitude > 0)
            {
                // Apply force for movement
                rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);
                
                // Calculate rotation based on movement direction
                Vector3 moveRotation = Vector3.Cross(Vector3.up, moveDirection);
                rb.AddTorque(moveRotation * moveSpeed, ForceMode.Force);
            }
        }

        void ApplyGravity()
        {
            Vector3 gravityDir = invertGravity ? Vector3.up : Vector3.down;
            rb.AddForce(gravityDir * gravityStrength, ForceMode.Acceleration);
        }

        private void OnEnable()
        {
            PlayerRefs.electricPlayer.dash.enabled = false;
        }

        private void OnDisable()
        {
            PlayerRefs.electricPlayer.dash.enabled = true;
        }
    }
}
