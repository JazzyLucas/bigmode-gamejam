using System;
using System.Linq;
using UnityEngine;


namespace BigModeGameJam.Level.Interactables
{
    /// <summary>
    /// Base class for interactable objects.
    /// </summary>
    [RequireComponent(typeof(MeshRenderer))]
    public class Interactable : MonoBehaviour
    {
        private MeshRenderer mesh;
        public Material highlightMaterial;
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
            Debug.Log($"{gameObject.name} is using an unimplemented interaction script!!!");
        }

        private void Awake()
        {
            mesh = GetComponent<MeshRenderer>();
        }
    }
}
