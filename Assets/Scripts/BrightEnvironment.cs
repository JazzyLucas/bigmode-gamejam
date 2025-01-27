using UnityEngine;

namespace BigModeGameJam
{
    public class BrightEnvironment : MonoBehaviour
    {
        [SerializeField] private Light directionalLight;
        private const float NORMAL_INTENSITY = 1f;

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
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                directionalLight.enabled = true;
                RenderSettings.ambientIntensity = NORMAL_INTENSITY;
            }
        }
    }
}
