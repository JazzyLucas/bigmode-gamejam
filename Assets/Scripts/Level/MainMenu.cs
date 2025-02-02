using System.Collections;
using BigModeGameJam.Core.Manager;
using BigModeGameJam.Level;
using BigModeGameJam.Level.Manager;
using UnityEngine;

namespace BigModeGameJam.Core
{
    /// <summary>
    /// Logic for in world main-menu. All main menu objects should be children
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        private Coroutine coroutine;
        public void StartGame()
        {
            if(coroutine != null) return;
            PlayerTransitioner.Transition(Level.Controls.PlayerMovement.PlayerType.Human);
            coroutine = StartCoroutine(WaitAndDisable());
            IEnumerator WaitAndDisable()
            {
                yield return new WaitForSeconds(PlayerTransitioner.TRANSITION_PERIOD / 2);
                gameObject.SetActive(false);
            }
        }

        public void Settings()
        {
            LevelManager.PauseGame();
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void ResetGame()
        {
            GameManager.ResetData();
            Debug.Log("Game data reset!");
        }

        private void Start()
        {
            OnEnable();
        }

        private void OnEnable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PlayerRefs.humanPlayer.gameObject.SetActive(false);
            PlayerRefs.electricPlayer.gameObject.SetActive(false);
        }
    }
}
