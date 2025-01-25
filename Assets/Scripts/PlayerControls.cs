using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Allows for player input to interact with other features.
/// </summary>
public class PlayerControls : MonoBehaviour
{
    public static PlayerControls instance;
    public PlayerMovement playerMovement;
    public Dash dash;
    public float lookSensitivity = 1;


    private InputAction lookAction;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction dashAction;

    private void Awake()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        dashAction = InputSystem.actions.FindAction("Sprint");
    }

    private void Update()
    {
        // Handle movement input
        Vector2 moveDir = moveAction.ReadValue<Vector2>();
        playerMovement.Move(new Vector3( moveDir.x, 0, moveDir.y));
        // Handle Rotation input
        playerMovement.Look(
            lookAction.ReadValue<Vector2>() * lookSensitivity
        );
        // Handle jump input
        if(jumpAction.IsPressed())
        {
            playerMovement.Jump();
        }
        if(dash != null && dashAction.WasPerformedThisFrame())
        {
            dash.StartDash(moveDir);
        }
    }
}
