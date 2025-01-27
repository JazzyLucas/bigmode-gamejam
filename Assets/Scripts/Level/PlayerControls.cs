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
        public PlayerMovement playerMovement;
        public float lookSensitivity = 1;
        public Camera fpCamera;
        public Camera tpCamera;
        private InputAction lookAction;
        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction dashAction;
        private InputAction crouchAction;
        private InputAction toggleCamAction;
        private InputAction interactAction;

        public Interactable CurrentInteractable { get; set; } //Stores what interactable is going to be interacted with (Do not set it's value in this script, let Interactable set value)

        private void Awake()
        {
            lookAction = InputSystem.actions.FindAction("Look");
            moveAction = InputSystem.actions.FindAction("Move");
            jumpAction = InputSystem.actions.FindAction("Jump");
            dashAction = InputSystem.actions.FindAction("Sprint");
            crouchAction = InputSystem.actions.FindAction("Crouch");
            toggleCamAction = InputSystem.actions.FindAction("ToggleView");
            interactAction = InputSystem.actions.FindAction("Interact");
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
            // Handle jump input
            if (jumpAction.IsPressed())
            {
                playerMovement.Jump();
            }
            if (dashAction.WasPerformedThisFrame())
            {
                playerMovement.Dash(hDir);
            }

            if(crouchAction.WasPerformedThisFrame())
            {
                playerMovement.Crouch();
            }
            else if(crouchAction.WasReleasedThisFrame())
            {
                playerMovement.Uncrouch();
            }

            if(toggleCamAction.WasPerformedThisFrame())
            {
                ToggleCam();
            }

            //Calls the interactable to perform it's interaction
            if (interactAction.IsPressed() && CurrentInteractable)
                CurrentInteractable.Interact(gameObject);
        }
    }

}