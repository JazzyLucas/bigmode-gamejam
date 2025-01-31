using System.Collections;
using BigModeGameJam.Level.Controls;
using BigModeGameJam.UI;
using UnityEngine;

namespace BigModeGameJam.Level
{
    /// <summary>
    /// General player health management
    /// </summary>
    public class PlayerHealth : MonoBehaviour
    {
        private const float MAX_HEALTH = 100;
        private const float HUMAN_REGEN = 10;
        private const float ELEC_REGEN = -0.83f; // 2 minutes to deplete naturally
        private const float RESPAWN_PERIOD = 2; // The time it takes to run the full respawn animation
        public bool isDead = false;

        /// <summary>
        /// Health as a percentage
        /// </summary>
        [SerializeField] private float health = MAX_HEALTH;
        /// <summary>
        /// Percentage of health regenerated per second
        /// </summary>
        public float regen = HUMAN_REGEN;
        /// <summary>
        /// Prevents damage from being taken
        /// </summary>
        private bool invulnerable = false;
        private PlayerMovement.PlayerType playerType = PlayerMovement.PlayerType.Human;
        private PlayerRefs playerRefs;

        /// <summary>
        /// Causes the attached player to refrain from being alive.
        /// </summary>
        public void Die()
        {
            FadeEffect.StartAnimation(FadeEffect.Animation.Transition, Color.black, RESPAWN_PERIOD);
            health = MAX_HEALTH;
            isDead = true;
            playerRefs.playerMovement.enabled = false;
            playerRefs.lookToInteract.enabled = false;
            playerRefs.rigidbody.linearVelocity = Vector3.zero;
            if(playerRefs.electricMode) playerRefs.electricMode.Exit();
            StartCoroutine(DieCoroutine());

            IEnumerator DieCoroutine()
            {
                // Wait until peak of effect
                yield return new WaitForSeconds(RESPAWN_PERIOD / 2);
                transform.position = PlayerRefs.electricPlayer.checkpoint != null
                 ? PlayerRefs.electricPlayer.checkpoint.position : Vector3.zero;
                playerRefs.playerMovement.enabled = true;
                playerRefs.lookToInteract.enabled = true;
            }
        }

        /// <summary>
        /// Regenerates or depletes health if delta is negative
        /// </summary>
        public void RegenHealth(float delta)
        {
            if(delta < 0 && invulnerable) return;
            health = Mathf.Clamp(health + delta, 0, MAX_HEALTH);
            if(playerType == PlayerMovement.PlayerType.Electric)
            {
                ElectricHUD.UpdateHealthbar(health);

            }
            if(health == 0) Die();
        }

        private void Awake()
        {
            if(TryGetComponent<PlayerRefs>(out playerRefs) && 
                playerRefs.playerType == PlayerMovement.PlayerType.Electric)
            {
                playerType = PlayerMovement.PlayerType.Electric;
                regen = ELEC_REGEN;
            }
        }

        private void OnDisable()
        {
            // Re-enable in case of coroutine interruption
            playerRefs.playerMovement.enabled = true;
            playerRefs.lookToInteract.enabled = true;
        }

        private void Update()
        {
            RegenHealth(regen * Time.deltaTime);
        }
    }
}
