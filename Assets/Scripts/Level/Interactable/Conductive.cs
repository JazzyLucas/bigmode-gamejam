using BigModeGameJam.Level.Controls;
using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    public class Conductive : Interactable
    {
        /// <summary>
        /// Array of conductive materials that the player should be able to jump to from this one
        /// </summary>
        [SerializeField] private Conductive[] connected;

        /// <summary>
        /// Returns true if 
        /// </summary>
        /// <param name="conductive"></param>
        /// <returns></returns>
        public bool IsConnected(Conductive target)
        {
            foreach(Conductive con in connected)
            {
                if(con == target) return true;
            }
            return false;

        }
        public override void Interact(GameObject interacter)
        {
            if(interacter.TryGetComponent<ElectricMode>(out ElectricMode p))
            {
                if(p.enabled)
                {
                    p.Exit();
                }
                else
                {
                    p.Enter(this);
                }
            }
            else
            {
                Unhover();
            }
        }
    }
}