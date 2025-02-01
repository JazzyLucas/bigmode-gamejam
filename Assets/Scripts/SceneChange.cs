using UnityEngine;
using UnityEngine.SceneManagement;

namespace BigModeGameJam.Core
{
    public class SceneChange : MonoBehaviour
    {
        public static void LoadNewScene(string sceneToLoad)
        {
            if (sceneToLoad == "Exit")
            {
                Application.Quit();
                Debug.Log("Game Has ended");
                return;
            }

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
