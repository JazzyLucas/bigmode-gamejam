using UnityEngine;
using BigModeGameJam.Level.Interactables;
using BigModeGameJam.UI;

namespace BigModeGameJam.Level.Controls
{
    public class LookToInteract : MonoBehaviour
    {
        /// <summary>
        /// Minimum distance the camera and interactable must be to allow interaction.
        /// </summary>
        public float minDistance = 15;
        public Transform cam;
        public Interactable lookingAt;

        public void Interact()
        {
            if(lookingAt == null) return;
            lookingAt.Interact(gameObject);
        }

        /// <summary>
        /// Assigns object that player is looking at.
        /// </summary>
        /// <returns>False if not looking at anything interactable.</returns>
        private bool LookingAtObject()
        {
            RaycastHit hit;
            if(Physics.Raycast(cam.position, cam.forward, out hit, minDistance))
            {
                if(hit.collider.gameObject.TryGetComponent<Interactable>(out Interactable l))
                {
                    lookingAt = l;
                    l.Hover();
                    return true;
                }
            }
            if(lookingAt != null)
            {
                lookingAt.Unhover();
                lookingAt = null;
            }
            return false;
        }

        private void Update()
        {
            Crosshair.instance.SetHover(LookingAtObject());
        }
    }
}
