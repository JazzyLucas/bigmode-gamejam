using BigModeGameJam.Level.Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BigModeGameJam.Level.Controls
{
    /// <summary>
    /// Allows for player input to interact with other features.
    /// </summary>
    public class PlayerControls : MonoBehaviour
    {
        public static PlayerControls instance;
        [Header("References")]
        public PlayerMovement playerMovement;
        public LookToInteract lookToInteract;
        public Camera fpCamera;
        public Camera tpCamera;
        [Header("Settings")]
        public float lookSensitivity = 1;
        
        private InputAction lookAction, moveAction, jumpAction, dashAction, crouchAction, toggleCamAction, interactAction;

        private void Awake()
        {
            InputActionAsset actions = InputSystem.actions;
            lookAction = actions.FindAction("Look");
            moveAction = actions.FindAction("Move");
            jumpAction = actions.FindAction("Jump");
            dashAction = actions.FindAction("Sprint");
            crouchAction = actions.FindAction("Crouch");
            toggleCamAction = actions.FindAction("ToggleView");
            interactAction = actions.FindAction("Interact");
        }

        private void ToggleCam()
        {
            if(fpCamera.isActiveAndEnabled)
            {
                fpCamera.gameObject.SetActive(false);
                tpCamera.gameObject.SetActive(true);
            }
            else
            {
                fpCamera.gameObject.SetActive(true);
                tpCamera.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            // Handle movement input
            Vector2 moveDir = moveAction.ReadValue<Vector2>();
            Vector3 hDir = new Vector3(moveDir.x, 0, moveDir.y);
            playerMovement.Move(hDir);
            // Handle Rotation input
            playerMovement.Look(
                lookAction.ReadValue<Vector2>() * lookSensitivity
            );
            // Movement
            if (jumpAction.IsPressed())
                playerMovement.Jump();
            if (dashAction.WasPerformedThisFrame())
                playerMovement.Dash(hDir);
            if(crouchAction.WasPerformedThisFrame())
                playerMovement.Crouch();
            else if(crouchAction.WasReleasedThisFrame())
                playerMovement.Uncrouch();

            if(interactAction.WasPressedThisFrame())
                lookToInteract.Interact();

            if(toggleCamAction.WasPerformedThisFrame())
                ToggleCam();
        }
    }

}