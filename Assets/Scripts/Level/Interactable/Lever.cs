using BigModeGameJam.Level.Interactables;
using UnityEngine;

namespace BigModeGameJam.Level
{
    public class Lever : Interactable
    {
        [SerializeField] public bool isOn = false;
        [SerializeField] private bool breakerSwitch = false;
        [SerializeField] private InteractablePowerBox ipb;
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
            SendToLevelManger();

            if(breakerSwitch)
            {
                ipb.IsComplete = true;
                ipb.GetComponent<BoxCollider>().enabled = false;
            }
        }

        new private void Awake()
        {
            mesh = GetComponent<MeshRenderer>();
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            float thetaZ = 0;
            if(isOn)
            {
                thetaZ = breakerSwitch ? -45 : 180;
            }
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, thetaZ);
        }
    }
}
