using BigModeGameJam.Level.Manager;
using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    abstract public class ObjectiveObject : MonoBehaviour
    {
        [Header("Objective Object Config: (Only set if set in a Objective Listener)")]
        [SerializeField] string uiMessage; //Currently not used

        /// <summary>
        /// If this Objective object has been set to a Objective Listener, when this Objective Object does it's interaction/pick up if will update the level manager and that will update the listener
        /// </summary>
        protected void SendToLevelManger()
        {
            if (LevelManager.AllObjectives.Contains(this))
                LevelManager.CompleteObjective(this);
        }
    }
}
