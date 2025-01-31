using System.Collections;
using UnityEngine;


namespace BigModeGameJam.Level.Controls
{
    /// <summary>
    /// Implementation of dash ability for electric character
    /// </summary>
    [RequireComponent(typeof(PlayerRefs))]
    public class Dash : MonoBehaviour
    {
        public float dashSpeed = 55;
        public float dashDistance = 15;
        /// <summary>
        /// The time between the end of a dash and when you can start another dash.
        /// </summary>
        public float cooldown = 0.5f;
        /// <summary>
        /// The number of times that a player can dash in the air.
        /// </summary>
        public int maxDashCharges = 1;
        public float depleteHealth = 8.5f;
        private PlayerRefs playerRefs;
        private PlayerMovement movement;
        private new Rigidbody rigidbody;
        private bool dashing;
        private int dashCharges = 1;

        public void Replenish()
        {
            dashCharges = maxDashCharges;
            ElectricHUD.UpdateDashStatus(!dashing);
        }
        public void StartDash(Vector3 dir)
        {
            if (!enabled || dashing || dashCharges < 1) return;
            float period = dashDistance / dashSpeed;
            movement.Stun(period);
            dashCharges--;
            dir = dir.normalized;
            dir = transform.TransformDirection(dir);
            playerRefs.playerHealth.RegenHealth(-depleteHealth);
            StartCoroutine(DashCoroutine(dir, period));
            ElectricHUD.UpdateDashStatus(false);
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
            ElectricHUD.UpdateDashStatus(dashCharges > 0);
        }

        private void Awake()
        {
            playerRefs = GetComponent<PlayerRefs>();
            movement = playerRefs.playerMovement;
            rigidbody = playerRefs.rigidbody;
        }

        private void OnDisable()
        {
            dashing = false;
            ElectricHUD.UpdateDashStatus(false);
        }

        private void OnEnable()
        {
            ElectricHUD.UpdateDashStatus(dashCharges > 0);
        }
    }
}