using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private const float VERTICAL_MOD = 0.45f;
    /// <summary>
    /// The object that this camera will reference for angle and position.
    /// </summary>
    public Transform follow;
    public float dist = 5;

    private void Update()
    {
        transform.position = follow.position;
        transform.forward = follow.forward;
        // Get final position
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.forward, out hit, dist))
        {
            // "collide" with ground
            transform.position = hit.point;
            transform.Translate(Vector3.up * VERTICAL_MOD);
            return;
        }
        transform.Translate(Vector3.forward * -dist + Vector3.up * VERTICAL_MOD);
    }
}
