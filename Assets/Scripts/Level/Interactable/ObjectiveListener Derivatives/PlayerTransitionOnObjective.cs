using BigModeGameJam.Level.Controls;
using BigModeGameJam.Level.Interactables;
using UnityEngine;

namespace BigModeGameJam.Level
{
    public class PlayerTransitionOnObjective : ObjectiveListener
    {
        [SerializeField] private PlayerMovement.PlayerType transitionTo = PlayerMovement.PlayerType.Human;
        protected override void OnFinishAllCustomCode()
        {
            PlayerTransitioner.Transition(transitionTo);
        }
    }
}
