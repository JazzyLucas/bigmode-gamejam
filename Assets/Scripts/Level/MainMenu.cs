using BigModeGameJam.Level;
using UnityEngine;

namespace BigModeGameJam.Core
{
    /// <summary>
    /// Logic for in world main-menu. All main menu objects should be children
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            PlayerTransitioner.Transition(Level.Controls.PlayerMovement.PlayerType.Human);
        }
    }
}
