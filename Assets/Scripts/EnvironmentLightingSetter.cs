using UnityEngine;

namespace BigModeGameJam
{
    public class EnvironmentLightingSetter : MonoBehaviour
    {
        [SerializeField] private Light directionalLight;
        public float INTENSITY = 0.3f;
        [SerializeField] private float transitionDuration = 2f;

        private float initialAmbientIntensity;
        private float targetIntensity;
        private float currentLerpTime;
        private bool isTransitioning;

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (Mathf.Approximately(RenderSettings.ambientIntensity, INTENSITY))
                    return;

                directionalLight.enabled = false;
                initialAmbientIntensity = RenderSettings.ambientIntensity;
                targetIntensity = INTENSITY;
                currentLerpTime = 0f;
                isTransitioning = true;
            }
        }
    }
}
