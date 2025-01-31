using UnityEngine;
using UnityEngine.SceneManagement;

namespace BigModeGameJam.Core
{
    public class SceneChange : MonoBehaviour
    {
        public void LoadNewScene(string sceneToLaod)
        {
            SceneManager.LoadScene(sceneToLaod);
        }
    }
}
