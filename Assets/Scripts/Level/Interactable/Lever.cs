using BigModeGameJam.Level.Interactables;
using UnityEngine;

namespace BigModeGameJam.Level
{
    public class Lever : Interactable
    {
        [SerializeField] public bool isOn { get ; private set;} = false;
        public override void Interact(GameObject interacter)
        {
            if(!canInteractMultipleTimes && timesInteracted > 0)
            {
                Unhover();
                return;
            }
            // Toggle switch
            isOn = !isOn;
            UpdateVisual();
            timesInteracted++;
        }

        private void Awake()
        {
            mesh = GetComponent<MeshRenderer>();
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            if(isOn)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 180);
            }
            else
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            }
        }
    }
}
