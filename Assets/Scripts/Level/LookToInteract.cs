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
        public Interactable lookingAt { get; private set; }
        /// <summary>
        /// Allows crosshair to update while preventing lookingAt changing
        /// </summary>
        public bool lockInteractable = false;

        public void Interact()
        {
            if (lookingAt == null) return;
            lookingAt.Interact(gameObject);
        }

        /// <summary>
        /// Assigns object that player is looking at.
        /// </summary>
        /// <returns>False if not looking at anything interactable.</returns>
        private bool LookingAtObject()
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.position, cam.forward, out hit, minDistance))
            {
                if (hit.collider.gameObject.TryGetComponent<Interactable>(out Interactable l))
                {
                    if (lookingAt != null && lookingAt != l)
                    {
                        lookingAt.Unhover();
                    }
                    SetInteractable(l);
                    l.Hover();
                    return true;
                }
            }
            if (lookingAt != null)
            {
                lookingAt.Unhover();
                SetInteractable(null);
            }
            return false;
        }

        // I know I could use C# setters but that shit is so janky so I'm going old-fashioned
        public void SetInteractable(Interactable i)
        {
            if(!lockInteractable) lookingAt = i;
        }

        bool crossHairPresent() => Crosshair.instance ? true : false;

        private void OnDisable()
        {
            if (lookingAt != null) lookingAt.Unhover();

            if (crossHairPresent())
            {
                Crosshair.instance.SetHover(false);
                Crosshair.instance.gameObject.SetActive(false);
            }

        }

        private void OnEnable()
        {
            if (Crosshair.instance)
                Crosshair.instance.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (crossHairPresent())
                Crosshair.instance.SetHover(LookingAtObject());
        }
    }
}
