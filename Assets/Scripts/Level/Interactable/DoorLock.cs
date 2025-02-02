using UnityEngine;
using BigModeGameJam.Level.Interactables;
using BigModeGameJam.UI;

namespace BigModeGameJam.Level.Interactables
{
    public class DoorLock : Interactable
    {
        [SerializeField] private bool keyObtained = false;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject unlockedLock;

        public override void Interact(GameObject interacter)
        {
            if (!keyObtained)
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

            unlockedLock.SetActive(true);
            gameObject.GetComponent<MeshRenderer>().enabled = false;

            // Enable animation
            if (animator != null)
            {
                animator.enabled = true;
            }

            timesInteracted++;
            SendToLevelManger();
        }

        public void SetKeyObtained(bool obtained)
        {
            keyObtained = obtained;
        }
    }
}
