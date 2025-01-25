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
        /// <summary>
        /// The time between the end of a dash and when you can start another dash.
        /// </summary>
        public float cooldown = 0.5f;
        /// <summary>
        /// The number of times that a player can dash in the air.
        /// </summary>
        public int maxDashCharges = 1;
        private PlayerMovement movement;
        private new Rigidbody rigidbody;
        private bool dashing;
        private int dashCharges = 1;

        public void Replenish()
        {
            dashCharges = maxDashCharges;
        }
        public void StartDash(Vector3 dir)
        {
            if (!enabled || dashing || dashCharges < 1) return;
            float period = dashDistance / dashSpeed;
            movement.Stun(period);
            dashCharges--;
            dir = dir.normalized;
            dir = transform.TransformDirection(dir);
            StartCoroutine(DashCoroutine(dir, period));
        }

        private IEnumerator DashCoroutine(Vector3 dir, float period)
        {
            float time = 0;
            dashing = true;
            while (time < period)
            {
                rigidbody.linearVelocity = dir * dashSpeed;
                yield return new WaitForEndOfFrame();
                time += Time.deltaTime;
            }
            yield return new WaitForSeconds(cooldown);
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