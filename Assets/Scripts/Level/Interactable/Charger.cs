using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    /// <summary>
    /// Script for "charger" object that replenishes player health
    /// </summary>
    public class Charger : Interactable
    {
        public override void Interact(GameObject interacter)
        {
            if(interacter.TryGetComponent<PlayerHealth>(out PlayerHealth p))
            {
                p.RegenHealth(100);
            }
        }
    }
}
