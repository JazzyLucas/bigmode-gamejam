using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    /// <summary>
    /// The object that this camera will reference for angle and position.
    /// </summary>
    public Transform follow;
    public float dist = 5;

    private void Update()
    {
        transform.position = follow.position;
        transform.forward = follow.forward;
        transform.Translate(Vector3.forward * -dist);
    }
}
