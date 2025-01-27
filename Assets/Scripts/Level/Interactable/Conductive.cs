using BigModeGameJam.Level.Controls;
using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    public class Conductive : Interactable
    {
        public override void Interact(GameObject interacter)
        {
            if(interacter.TryGetComponent<ElectricMode>(out ElectricMode p))
            {
                if(p.enabled)
                {
                    p.Enter(this);
                }
                else
                {
                    p.Exit();
                }
            }
            else
            {
                Unhover();
            }
        }
    }
}