using BigModeGameJam.Level.Manager;
using UnityEngine;
using TMPro;
using BigModeGameJam.Core.Manager;
using BigModeGameJam.Level.Controls;

namespace BigModeGameJam.Level
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] GameObject moneyText;

        void Awake()
        {
            LevelManager.PauseMenu = this;
            gameObject.SetActive(false);
        }

        internal void PauseGame()
        {
            Time.timeScale = 0.0f;
            gameObject.SetActive(true);
            moneyText.GetComponent<TextMeshProUGUI>().text = "$" + GameManager.GameData.Money.ToString();
        }

        internal void UnpauseGame()
        {
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);
        }

        public void UnpauseButton()
        {
            LevelManager.UnpauseGame(PlayerRefs.curPlayer.gameObject.activeInHierarchy);
            PlayerControls.menuIsUp = false;
        }
    }
}
