using BigModeGameJam.Level.Interactables;
using UnityEngine;

namespace BigModeGameJam.Level
{
    public class Lever : Interactable
    {
        [SerializeField] public bool isOn { get ; private set;} = false;
        public override void Interact(GameObject interacter)
        {
            if(!canInteractMultipleTimes)
            {
                Unhover();
                return;
            }
            // Toggle switch
            isOn = !isOn;
            UpdateVisual();
        }

        private void Awake()
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            if(isOn)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180);
            }
            else
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
            }
        }
    }
}
