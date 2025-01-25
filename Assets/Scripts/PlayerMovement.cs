using System.Linq.Expressions;
using UnityEngine;

/// <summary>
/// Handles basic player movement using rigidbody.
/// NOTE: Rigidbody must be attached with no friction, no gravity and frozen rotation.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Extra distance added to IsGrounded() raycast to allow for imprecision.
    /// </summary>
    private const float GROUND_LENIENCE = 0.001f;

    public float acceleration = 10, maxSpeed = 10, jumpVelocity = 5, airFriction = 1,
    groundFriction = 10, gravity = 9.8f;
    new private Rigidbody rigidbody;
    new private Collider collider;
    [SerializeField] new private Camera camera;
    // Referencing camera's rotation causes issues for some reason. keep track of it here instead.
    private float camX;

    /// <summary>
    /// Checks if entity is touching (or close to) ground using raycasts.
    /// </summary>
    /// <returns>True if entity is grounded. False otherwise.</returns>
    public bool IsGrounded()
    {
        float distToBottom = collider.bounds.extents.y;
        return Physics.Raycast(
            transform.position, -Vector3.up, distToBottom + GROUND_LENIENCE
        );
    }

    public void Jump()
    {
        if (!IsGrounded()) return;
        rigidbody.linearVelocity = new Vector3(
            rigidbody.linearVelocity.x, jumpVelocity, rigidbody.linearVelocity.z
        );
    }

    /// <summary>
    /// Controls player camera looking and rotation.
    /// </summary>
    /// <param name="delta">Difference in rotation.</param>
    public void Look(Vector2 delta)
    {
        // Look left and right
        transform.Rotate(0, delta.x, 0);
        // Look up and down
        camX -= delta.y;
        camX = Mathf.Clamp(camX, -90, 90);
        camera.transform.localRotation = Quaternion.Euler(
            camX, 0, 0
        );
    }

    /// <summary>
    /// Moves player on xz plane
    /// </summary>
    /// <param name="dir">Direction to move relative to forward</param>
    public void Move(Vector3 dir)
    {
        dir = dir.normalized;
        dir = transform.TransformDirection(dir);

        bool grounded = IsGrounded();

        // Add friction to movement acceleration for tighter controls
        float totalAcc = acceleration;
        totalAcc += grounded ? groundFriction : airFriction;

        // new vector needed in order to ignore y axis
        Vector2 hVel = new Vector2(
            rigidbody.linearVelocity.x + (dir.x * totalAcc * Time.deltaTime),
            rigidbody.linearVelocity.z + (dir.z * totalAcc * Time.deltaTime)
        );

        // Handle max speed
        hVel = Vector2.MoveTowards(hVel,
            Vector2.ClampMagnitude(hVel, maxSpeed),
            totalAcc * Time.deltaTime
        );

        // Set velocity to new calculated value
        rigidbody.linearVelocity = new Vector3(
            hVel.x, rigidbody.linearVelocity.y, hVel.y
        );
    }

    private void ApplyFriction()
    {
        float delta = Time.deltaTime;
        if (IsGrounded())
        {
            delta *= groundFriction;
        }
        else
        {
            delta *= airFriction;
        }
        // Horizontal plane only. Do not affect y axis.
        Vector3 target = Vector3.MoveTowards(
            rigidbody.linearVelocity, Vector3.zero, delta);
        rigidbody.linearVelocity = new Vector3(
            target.x, rigidbody.linearVelocity.y, target.z
        );
    }

    private void ApplyGravity()
    {
        if (IsGrounded()) return;
        rigidbody.linearVelocity += Vector3.down * gravity * Time.deltaTime;
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        ApplyFriction();
        ApplyGravity();
    }
}
