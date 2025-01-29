using BigModeGameJam.Level.Controls;
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

        /// <summary>
        /// Causes the attached player to refrain from being alive.
        /// </summary>
        public void Die()
        {
            // TODO: actual implementation
            Debug.Log("Died");
            transform.position = Vector3.zero;
            health = MAX_HEALTH;
        }

        /// <summary>
        /// Regenerates or depletes health if delta is negative
        /// </summary>
        public void RegenHealth(float delta)
        {
            if(delta < 0 && invulnerable) return;
            health = Mathf.Clamp(health + delta, 0, MAX_HEALTH);
            if(playerType == PlayerMovement.PlayerType.Electric)
                ElectricHUD.UpdateHealthbar(health);
            if(health == 0) Die();
        }

        private void Update()
        {
            RegenHealth(regen * Time.deltaTime);
        }

        private void Awake()
        {
            if(TryGetComponent<PlayerRefs>(out PlayerRefs p) && 
                p.playerType == PlayerMovement.PlayerType.Electric)
            {
                playerType = p.playerType;
                regen = ELEC_REGEN;
            }
        }
    }
}
