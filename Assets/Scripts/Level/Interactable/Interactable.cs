using BigModeGameJam.Core;
using BigModeGameJam.Level.Controls;
using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    /// <summary>
    /// Base class for interactable objects.
    /// To interact with an Interactable, look at it and press the interact key.
    /// </summary>
    [RequireComponent(typeof(MeshRenderer))]
    public class Interactable : ObjectiveObject
    {
        protected MeshRenderer mesh;
        [Header("Interactable Configs")]
        public Material highlightMaterial;
        [SerializeField] protected bool canInteractMultipleTimes;
        [SerializeField] protected int timesInteracted;
        public void Hover()
        {
            mesh.materials = new Material[] {mesh.material, highlightMaterial};
        }

        public void Unhover()
        {
            // Remove added materials
            mesh.materials = new Material[] {mesh.material};
        }
        /// <summary>
        /// Unimplemented interaction logic
        /// </summary>
        /// <param name="interacter">The player object that is interacting with this object</param>
        public virtual void Interact(GameObject interacter)
        {
            if (!canInteractMultipleTimes && timesInteracted > 0)
                return;

            timesInteracted++;
            SendToLevelManger();
            Debug.Log($"{gameObject.name} interaction has happened");
        }

        protected void Awake()
        {
            mesh = GetComponent<MeshRenderer>();
        }
    }
}
