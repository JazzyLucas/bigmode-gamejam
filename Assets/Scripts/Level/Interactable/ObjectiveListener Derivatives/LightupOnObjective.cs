using BigModeGameJam.Level.Interactables;
using UnityEngine;

namespace BigModeGameJam.Level
{
    public class LightupOnObjective : ObjectiveListener
    {
        [Header("References")]
        [SerializeField] private MeshRenderer mesh;
        [SerializeField] new private Light light;
        [Header("Visuals")]
        [SerializeField] private Material litMaterial;
        [SerializeField] private Color lightColor = Color.green;
        protected override void OnFinishAllCustomCode()
        {
            mesh.material = litMaterial;
            light.color = lightColor;
        }
    }
}
