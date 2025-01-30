using BigModeGameJam.Level.Manager;
using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    abstract public class ObjectiveObject : MonoBehaviour
    {
        [Header("Objective Object Config: (Only set if set in a Objective Listener)")]
        [SerializeField] protected string uiMessage; //Currently not used

        internal string UIMessage { get { return uiMessage; } }

        internal bool IsComplete { get; set; } //Mainly is just used for tracking if the UI needs to strike an objective on UI trigger

        /// <summary>
        /// If this Objective has already been completed before it has been displayed in the UI, use this to strike it out
        /// </summary>
        internal void StrikeOutUIMessage()
        {
            uiMessage = "<s>" + uiMessage + "</s>";
        }

        /// <summary>
        /// If this Objective object has been set to a Objective Listener, when this Objective Object does it's interaction/pick up if will update the level manager and that will update the listener
        /// </summary>
        protected void SendToLevelManger()
        {
            if (LevelManager.ObjectiveIsIncluded(this))
            {
                LevelManager.CompleteObjective(this);
                return;
            }
        }
    }
}
