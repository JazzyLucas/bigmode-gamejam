using UnityEngine;
using BigModeGameJam.Level.Interactables;
using BigModeGameJam.UI;

namespace BigModeGameJam.Level.Interactables
{
    public class ScannerObjective : Interactable
    {
        [SerializeField] private bool keycardObtained = false;
        [SerializeField] private GameObject objectToAnimate;
        [SerializeField] private Animator animator;
        [SerializeField] private MeshRenderer scannerLight;
        public InteractablePowerBox powerBox;

        public override void Interact(GameObject interacter)
        {
            if (!keycardObtained)
            {
                // Show "locked" message using UI system
                if (Crosshair.instance)
                {
                    //Crosshair.instance.ShowMessage("Locked");
                }
                return;
            }

            if (!canInteractMultipleTimes && timesInteracted > 0 || !powerBox.IsComplete)
            {
                Unhover();
                return;
            }

            // Change material colors to green
            if (scannerLight != null)
            {
                MeshRenderer childRenderer = scannerLight.GetComponent<MeshRenderer>();
                Material material = childRenderer.material;
                material.color = Color.green;  // Base color
                material.SetColor("_SpecColor", Color.green);  // Specular color
                material.SetColor("_EmissionColor", Color.green);  // Emission color
                material.EnableKeyword("_EMISSION");  // Make sure emission is enabled
            }

            // Enable animation
            if (objectToAnimate != null && animator != null)
            {
                objectToAnimate.SetActive(true);
                animator.enabled = true;
            }

            timesInteracted++;
            SendToLevelManger();
        }

        public void SetKeycardObtained(bool obtained)
        {
            keycardObtained = obtained;
        }
    }
}