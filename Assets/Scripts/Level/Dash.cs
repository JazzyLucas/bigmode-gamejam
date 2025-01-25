using System.Collections;
using UnityEngine;


namespace BigModeGameJam.Level.Controls
{
    /// <summary>
    /// Implementation of dash ability for electric character
    /// </summary>
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(Rigidbody))]
    public class Dash : MonoBehaviour
    {
        public float dashSpeed = 20;
        public float dashDistance = 5;
        private PlayerMovement movement;
        private new Rigidbody rigidbody;
        private bool dashing;

        public void StartDash(Vector3 dir)
        {
            if (!enabled || dashing) return;
            float period = dashDistance / dashSpeed;
            movement.Stun(period);
            dir = dir.normalized;
            dir = transform.TransformDirection(dir);
            StartCoroutine(DashCoroutine(dir, period));
        }

        private IEnumerator DashCoroutine(Vector3 dir, float period)
        {
            Debug.Log(dir);
            float time = 0;
            dashing = true;
            while (time < period)
            {
                rigidbody.linearVelocity = dir * dashSpeed;
                yield return new WaitForEndOfFrame();
                time += Time.deltaTime;
            }
            dashing = false;
        }

        private void Awake()
        {
            movement = GetComponent<PlayerMovement>();
            rigidbody = GetComponent<Rigidbody>();
        }

        private void OnDisable()
        {
            dashing = false;
        }
    }
}