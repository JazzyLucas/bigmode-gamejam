using BigModeGameJam.Level.Interactables;
using BigModeGameJam.UI;
using UnityEngine;

namespace BigModeGameJam.Level
{
    public class InteractablePowerBox :Interactable
    {
        public Transform electricPlayerStart;

        public override void Interact(GameObject interacter)
        {
            if (!canInteractMultipleTimes && timesInteracted > 0 || IsComplete)
            {
                Unhover();
                return;
            }

            PlayerRefs.PlayerTransition(Controls.PlayerMovement.PlayerType.Electric, electricPlayerStart);

            timesInteracted++;
            SendToLevelManger();
        }
    }
}
