using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    public class DestroyOnInteractable : Interactable
    {
        public override void Interact(GameObject interacter)
        {
            base.Interact(interacter);
            
            Destroy(gameObject);
        }
    }
}
