using NUnit.Framework;
using BigModeGameJam.UI;
using System.Collections.Generic;
using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    public class FinalDoorOpener : Interactable
    {
        public List<Animator> animator;
        public List<GameObject> objectsToTurnOff;
        public List<GameObject> objectsToTurnOn;
        public InteractablePowerBox powerBox;

        //LIGHTING TRANSITION SETTINGS
        [SerializeField] private Light directionalLight;
        public float INTENSITY = 1.2f;
        [SerializeField] private float transitionDuration = 1f;

        private float initialAmbientIntensity;
        private float targetIntensity;
        private float currentLerpTime;
        private bool isTransitioning;

        public override void Interact(GameObject interacter)
        {
            if (!powerBox.IsComplete)
            {
                // Show "locked" message using UI system
                if (Crosshair.instance)
                {
                    //Crosshair.instance.ShowMessage("Locked");
                }
                return;
            }

            if (!canInteractMultipleTimes && timesInteracted > 0)
            {
                Unhover();
                return;
            }

            // Enable animation
            foreach (Animator anim in animator)
            {
                anim.enabled = true;
            }

            foreach (GameObject obj in objectsToTurnOff)
            {
                obj.SetActive(false);
            }

            foreach (GameObject obj in objectsToTurnOn)
            {
                obj.SetActive(true);
            }

            if (Mathf.Approximately(RenderSettings.ambientIntensity, INTENSITY))
                return;

            directionalLight.enabled = true;
            initialAmbientIntensity = RenderSettings.ambientIntensity;
            targetIntensity = INTENSITY;
            currentLerpTime = 0f;
            isTransitioning = true;

            timesInteracted++;
            SendToLevelManger();
            gameObject.GetComponent<FinalDoorOpener>().canInteractMultipleTimes = false;
        }

        void Start()
        {
            if (directionalLight == null)
            {
                Light[] lights = GameObject.FindObjectsOfType<Light>();
                foreach (Light light in lights)
                {
                    if (light.type == LightType.Directional)
                    {
                        directionalLight = light;
                        break;
                    }
                }

                if (directionalLight != null)
                {
                    Debug.LogWarning("Directional light reference was not set - found directional light automatically", this);
                }
                else
                {
                    Debug.LogError("No directional light found in the scene!", this);
                }
            }
            initialAmbientIntensity = RenderSettings.ambientIntensity;
        }

        private void Update()
        {
            if (isTransitioning)
            {
                currentLerpTime += Time.deltaTime;
                float percentageComplete = currentLerpTime / transitionDuration;

                RenderSettings.ambientIntensity = Mathf.Lerp(initialAmbientIntensity, targetIntensity, percentageComplete);

                if (percentageComplete >= 1.0f)
                {
                    isTransitioning = false;
                }
            }
        }
    }
}
