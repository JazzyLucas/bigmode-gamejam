using BigModeGameJam.Level.Manager;
using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    public class UIObjectiveTrigger : PickUp
    {
        [Header("Listener to assign")]
        [SerializeField] ObjectiveListener currentObjectiveListener;

        protected override void OnCustomPickUpCode()
        {
            if (uiMessage != "Ignore this (Also don't put this in a listener)")
            {
                Debug.LogError("Fuck u, It says not to change the UI Message why did you change it now I'm going to purposely break shit in the UI");
                return;
            }

            if (!LevelManager.UIObjectiveManager)
            {
                Debug.LogWarning("Warrning: No UICanvas in scene. Cannot use Objective Triggers without a UIManager present in the scene");
                return;
            }

            LevelManager.CreateNewUIObjective(currentObjectiveListener);
        }

    }
}
