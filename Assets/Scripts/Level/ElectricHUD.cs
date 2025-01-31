using BigModeGameJam.Level.Controls;
using UnityEngine;

namespace BigModeGameJam.Level
{
    /// <summary>
    /// Handle HUD that's unique to the electric character
    /// </summary>
    public class ElectricHUD : MonoBehaviour
    {
        public static ElectricHUD instance;
        [SerializeField] private RectTransform healthFill;
        [SerializeField] private GameObject dashStatus;
        private static float initWidth;

        /// <summary>
        /// Hides "Dash" text or icon when the player can't dash
        /// </summary>
        /// <param name="status"></param>
        public static void UpdateDashStatus(bool status)
        {
            if(!instance) return;
            instance.dashStatus.SetActive(status);
        }

        /// <summary>
        /// Updates the healthbar to show how much health is remaining.
        /// </summary>
        /// <param name="perc">Percentage of health remaining</param>
        public static void UpdateHealthbar(float perc)
        {
            RectTransform rectTransform = instance.healthFill;
            // Decrease size
            rectTransform.sizeDelta = new Vector2(initWidth * (perc / 100), rectTransform.rect.height);
            // Shift center position
            rectTransform.anchoredPosition = new Vector2(((perc / 100) - 1) * initWidth / 2, rectTransform.anchoredPosition.y);
        }

        private void Awake()
        {
            instance = this;
            initWidth = healthFill.rect.width;
        }
    }
}
