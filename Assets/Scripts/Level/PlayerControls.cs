using BigModeGameJam.Level.Interactables;
using BigModeGameJam.UI;
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
                // FP -> TP
                playerRefs.firstPersonCam.gameObject.SetActive(false);
                playerRefs.thirdPersonCam.gameObject.SetActive(true);
                Crosshair.instance.gameObject.SetActive(false);
                PlayerRefs.curCam = playerRefs.thirdPersonCam.gameObject;
            }
            else
            {
                // TP -> FP
                playerRefs.firstPersonCam.gameObject.SetActive(true);
                playerRefs.thirdPersonCam.gameObject.SetActive(false);
                Crosshair.instance.gameObject.SetActive(true);
                PlayerRefs.curCam = playerRefs.firstPersonCam.gameObject;
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
            {
                if(playerRefs.electricMode == null || !playerRefs.electricMode.enabled)
                    playerRefs.playerMovement.Jump();
                else
                    playerRefs.electricMode.Exit(true); // Jump exit
            }
            if (dashAction.WasPerformedThisFrame())
                playerRefs.playerMovement.Dash(hDir);
            if(crouchAction.WasPerformedThisFrame())
                playerRefs.playerMovement.Crouch();
            else if(crouchAction.WasReleasedThisFrame())
                playerRefs.playerMovement.Uncrouch();

            if(interactAction.WasPressedThisFrame())
                playerRefs.lookToInteract.Interact();

            // Hold to change perspective
            if(toggleCamAction.WasPerformedThisFrame() || toggleCamAction.WasReleasedThisFrame())
                ToggleCam();
        }
    }

}