using System.Collections;
using BigModeGameJam.Core.Manager;
using BigModeGameJam.Level;
using BigModeGameJam.Level.Manager;
using BigModeGameJam.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                Crosshair.instance.gameObject.SetActive(true);
                FadeEffect.StartAnimation(FadeEffect.Animation.FadeIn, Color.black, 1);
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

        public void BackToMain()
        {
            FMODUnity.RuntimeManager.CreateInstance("event:/LevelTwo").stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void Start()
        {
            OnEnable();
        }

        private void OnEnable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if(PlayerRefs.humanPlayer) PlayerRefs.humanPlayer.gameObject.SetActive(false);
            if(PlayerRefs.electricPlayer) PlayerRefs.electricPlayer.gameObject.SetActive(false);
            if(Crosshair.instance) Crosshair.instance.gameObject.SetActive(false);
        }
    }
}
