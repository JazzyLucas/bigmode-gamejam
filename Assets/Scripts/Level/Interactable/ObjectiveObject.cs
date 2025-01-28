using BigModeGameJam.Level.Manager;
using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    abstract public class ObjectiveObject : MonoBehaviour
    {
        [Header("Objective Object Config: (Only set if set in a Objective Listener)")]
        [SerializeField] protected string uiMessage; //Currently not used

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

            //This might need to go if we have power ups or some kind of pick up that doesn't need to be referenced in a Objective Listener 
            if (GetType() == typeof(PickUp) && GetType() != typeof(PersistentPickUps))
                Debug.LogWarning("Non Persistance Pick Up not set to a Objective Listener. Please assign: " + gameObject.name + " to a Objective Listener");
        }
    }
}
