using UnityEngine;
using UnityEngine.SceneManagement;

namespace BigModeGameJam.Core
{
    public class SceneChange : MonoBehaviour
    {
        public void LoadNewScene(string sceneToLaod)
        {
            if (sceneToLaod == "Exit")
            {
                Application.Quit();
                Debug.Log("Game Has ended");
                return;
            }

            SceneManager.LoadScene(sceneToLaod);
        }
    }
}
