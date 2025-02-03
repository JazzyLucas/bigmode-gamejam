using BigModeGameJam.Level.Interactables;
using BigModeGameJam.Level.Manager;
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
        public static float lookSensitivity = 1;

        private InputAction lookAction, moveAction, jumpAction, dashAction, crouchAction, toggleCamAction, interactAction, pauseAction;
        private PlayerRefs playerRefs;

        public static bool menuIsUp;

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
            pauseAction = actions.FindAction("Pause");
            OnEnable();
        }

        private void OnEnable()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void ToggleCam()
        {
            if(playerRefs.firstPersonCam.isActiveAndEnabled)
            {
                playerRefs.ThirdPerson();
            }
            else
            {
                playerRefs.FirstPerson();
            }
        }

        

        private void Update()
        {
            //Checks when the player presses esc
            if (pauseAction.WasPressedThisFrame())
            {
                menuIsUp = !menuIsUp;

                if (menuIsUp)
                {
                    LevelManager.PauseGame();
                    return;
                }

                LevelManager.UnpauseGame();
            }

            if (menuIsUp) //Makes sure the player can't do anything during pause
                return;
            

            // Handle movement input
            Vector2 moveDir = moveAction.ReadValue<Vector2>();
            Vector3 hDir = new Vector3(moveDir.x, 0, moveDir.y);
            playerRefs.playerMovement.Move(hDir);

            bool walking = hDir.sqrMagnitude > 0.1;
            playerRefs.fpAnimator.SetBool("Walking", walking);
            

            // Handle Rotation input
            playerRefs.playerMovement.Look(
                lookAction.ReadValue<Vector2>() * lookSensitivity
            );

            // only set the trigger once or the animation repeats
			if( jumpAction.WasPressedThisFrame() ) playerRefs.fpAnimator.SetTrigger("Jump");

			// Movement
			if (jumpAction.IsPressed())
            {
                if (playerRefs.electricMode == null || !playerRefs.electricMode.enabled)
                {
                    playerRefs.playerMovement.Jump();
                }
                else
                    playerRefs.electricMode.Exit(true); // Jump exit
            }
            if (dashAction.WasPerformedThisFrame())
            {
                playerRefs.playerMovement.Dash(hDir);
                playerRefs.fpAnimator.SetTrigger("Dash");
            }
            if(crouchAction.WasPerformedThisFrame())
                playerRefs.playerMovement.Crouch();
            else if(crouchAction.WasReleasedThisFrame())
                playerRefs.playerMovement.Uncrouch();

            if(interactAction.WasPressedThisFrame())
                playerRefs.lookToInteract.Interact();

            // Hold to change perspective
            // REMOVED THIRD PERSON FUNCTIONALITY WHEN NOT CONDUCTING
            if(playerRefs.electricMode && playerRefs.electricMode.enabled &&
                (toggleCamAction.WasPerformedThisFrame() || toggleCamAction.WasReleasedThisFrame()))
                ToggleCam();
        }
    }

}