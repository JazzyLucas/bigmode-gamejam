using BigModeGameJam.Level.Interactables;
using UnityEngine;

namespace BigModeGameJam.Level
{
    public class DoorKey : Interactable
    {
        public override void Interact(GameObject interacter)
        {
            base.Interact(interacter);

            // Find the ScannerObjective in the scene
            DoorLock lockObjective = GameObject.FindObjectOfType<DoorLock>();

            // If found, set keycard obtained to true
            if (lockObjective != null)
            {
                lockObjective.SetKeyObtained(true);
            }

            Destroy(gameObject);
        }
    }
}
