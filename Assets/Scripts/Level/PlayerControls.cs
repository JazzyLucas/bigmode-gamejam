using BigModeGameJam.Level.Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BigModeGameJam.Level.Controls
{
    /// <summary>
    /// Allows for player input to interact with other features.
    /// </summary>
    [RequireComponent(typeof(PlayerRefs))]
    public class PlayerControls : MonoBehaviour
    {
        
        [Header("Settings")]
        public float lookSensitivity = 1;

        private InputAction lookAction, moveAction, jumpAction, dashAction, crouchAction, toggleCamAction, interactAction;
        private PlayerRefs playerRefs;

        private void Awake()
        {
            playerRefs = GetComponent<PlayerRefs>();
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
            if(playerRefs.firstPersonCam.isActiveAndEnabled)
            {
                playerRefs.firstPersonCam.gameObject.SetActive(false);
                playerRefs.thirdPersonCamera.gameObject.SetActive(true);
            }
            else
            {
                playerRefs.firstPersonCam.gameObject.SetActive(true);
                playerRefs.thirdPersonCamera.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            // Handle movement input
            Vector2 moveDir = moveAction.ReadValue<Vector2>();
            Vector3 hDir = new Vector3(moveDir.x, 0, moveDir.y);
            playerRefs.playerMovement.Move(hDir);
            // Handle Rotation input
            playerRefs.playerMovement.Look(
                lookAction.ReadValue<Vector2>() * lookSensitivity
            );
            // Movement
            if (jumpAction.IsPressed())
                playerRefs.playerMovement.Jump();
            if (dashAction.WasPerformedThisFrame())
                playerRefs.playerMovement.Dash(hDir);
            if(crouchAction.WasPerformedThisFrame())
                playerRefs.playerMovement.Crouch();
            else if(crouchAction.WasReleasedThisFrame())
                playerRefs.playerMovement.Uncrouch();

            if(interactAction.WasPressedThisFrame())
                playerRefs.lookToInteract.Interact();

            if(toggleCamAction.WasPerformedThisFrame())
                ToggleCam();
        }
    }

}