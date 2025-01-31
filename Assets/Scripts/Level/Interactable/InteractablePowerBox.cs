using BigModeGameJam.Level.Interactables;
using BigModeGameJam.UI;
using UnityEngine;
using static BigModeGameJam.UI.FadeEffect;

namespace BigModeGameJam.Level
{
    public class InteractablePowerBox :Interactable
    {
        public GameObject player;
        public GameObject electricBallPlayer;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        public override void Interact(GameObject interacter)
        {
            if (!canInteractMultipleTimes && timesInteracted > 0)
            {
                Unhover();
                return;
            }

            player.SetActive(false);
            electricBallPlayer.SetActive(true);

            timesInteracted++;
            SendToLevelManger();
        }
    }
}
