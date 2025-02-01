using BigModeGameJam.Level.Interactables;
using BigModeGameJam.UI;
using UnityEngine;

namespace BigModeGameJam.Level
{
    public class InteractablePowerBox :Interactable
    {
        public GameObject electricBallPlayer;
        public Transform electricPlayerStart;

        public override void Interact(GameObject interacter)
        {
            if (!canInteractMultipleTimes && timesInteracted > 0)
            {
                Unhover();
                return;
            }

            PlayerRefs.PlayerTransition(Controls.PlayerMovement.PlayerType.Electric, electricPlayerStart);
            if(electricBallPlayer) electricBallPlayer.SetActive(true);

            timesInteracted++;
            SendToLevelManger();
        }
    }
}
