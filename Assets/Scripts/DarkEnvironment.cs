using UnityEngine;

namespace BigModeGameJam
{
    public class DarkEnvironment : MonoBehaviour
    {
        [SerializeField] private Light directionalLight;
        private const float DARK_INTENSITY = 0.3f;

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                directionalLight.enabled = false;
                RenderSettings.ambientIntensity = DARK_INTENSITY;
            }
        }
    }
}
