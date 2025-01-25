using System.Collections;
using UnityEngine;

/// <summary>
/// Implementation of dash ability for electric character
/// </summary>
[RequireComponent(typeof(PlayerMovement))]
public class Dash : MonoBehaviour
{
    private PlayerMovement movement;
    public float dashSpeed = 20;
    public float dashDistance = 5;
    private bool dashing;

    public void StartDash(Vector3 dir)
    {
        if(!enabled || dashing) return;
        float period = dashDistance / dashSpeed;
        movement.Stun(period);
        dir = transform.TransformDirection(dir);
        StartCoroutine(DashCoroutine(dir, period));
    }

    private IEnumerator DashCoroutine(Vector3 dir, float period)
    {
        float time = 0;
        dashing = true;
        while(time < period)
        {
            transform.position += dir * dashSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
            time = Time.deltaTime;
        }
        dashing = false;
    }

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void OnDisable()
    {
        dashing = false;
    }
}
