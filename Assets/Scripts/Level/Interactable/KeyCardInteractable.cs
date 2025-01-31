using BigModeGameJam.Level.Interactables;
using UnityEngine;

namespace BigModeGameJam.Level
{
    public class KeyCardInteractable : Interactable
    {
        public override void Interact(GameObject interacter)
        {
            base.Interact(interacter);

            // Find the ScannerObjective in the scene
            ScannerObjective scannerObjective = GameObject.FindObjectOfType<ScannerObjective>();

            // If found, set keycard obtained to true
            if (scannerObjective != null)
            {
                scannerObjective.SetKeycardObtained(true);
            }

            Destroy(gameObject);
        }
    }
}
