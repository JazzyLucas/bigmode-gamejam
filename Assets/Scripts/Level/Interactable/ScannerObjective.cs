using UnityEngine;
using BigModeGameJam.Level.Interactables;
using BigModeGameJam.UI;
using System.Collections.Generic;
using BigModeGameJam.Core;

namespace BigModeGameJam.Level.Interactables
{
    public class ScannerObjective : Interactable
    {
        [SerializeField] private bool keycardObtained = false;
        public List<Animator> animator;
        [SerializeField] private MeshRenderer scannerLight;
        public InteractablePowerBox powerBox;

        public override void Interact(GameObject interacter)
        {
            if (!canInteractMultipleTimes && timesInteracted > 0)
            {
                Unhover();
                return;
            }

            if (!powerBox.IsComplete)
            {
                Unhover();
                AudioManager.instance.PlayOneShot(FMODEvents.instance.KeypadOff, this.transform.position);
                return;
            }

            if (!keycardObtained)
            {
                Unhover();
                AudioManager.instance.PlayOneShot(FMODEvents.instance.KeypadFail, this.transform.position);
                return;
            }

            // Change material colors to green
            if (scannerLight != null)
            {
                Material material = scannerLight.material;
                material.color = Color.green;  // Base color
                material.SetColor("_SpecColor", Color.green);  // Specular color
                material.SetColor("_EmissionColor", Color.green);  // Emission color
                material.EnableKeyword("_EMISSION");  // Make sure emission is enabled
            }

            // Enable animation
            if (animator != null)
            {
                foreach (Animator animator in animator)
                {
                    animator.enabled = true;
                }
            }

            timesInteracted++;
            SendToLevelManger();
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Test2, this.transform.position);
            gameObject.GetComponent<ScannerObjective>().canInteractMultipleTimes = false;
        }

        public void SetKeycardObtained(bool obtained)
        {
            keycardObtained = obtained;
        }
    }
}