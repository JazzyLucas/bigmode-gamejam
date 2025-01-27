using BigModeGameJam.Level.Controls;
using System;
using System.Linq;
using UnityEngine;


namespace BigModeGameJam.Level.Interactables
{
    /// <summary>
    /// Base class for interactable objects.
    /// </summary>
    [RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(BoxCollider))]
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

        void OnTriggerEnter(Collider other) //Sets to the player what interactable is being interacted with
        {
            if (!other.GetComponent<PlayerControls>())
                return;

            other.GetComponent<PlayerControls>().CurrentInteractable = this;
        }

        void OnTriggerExit(Collider other)//Unsets to the player what this interactable
        {
            if (!other.GetComponent<PlayerControls>())
                return;

            other.GetComponent<PlayerControls>().CurrentInteractable = null;
        }

        private void Awake()
        {
            mesh = GetComponent<MeshRenderer>();
        }
    }
}
