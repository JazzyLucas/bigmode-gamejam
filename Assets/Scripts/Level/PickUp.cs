using BigModeGameJam.Level.Controls;
using BigModeGameJam.Level.Manager;
using UnityEngine;

namespace BigModeGameJam.Level
{
    public class PickUp : MonoBehaviour
    {
        [SerializeField] Objective objective;

        protected virtual void OnPickUp()
        {
            LevelManager.CompleteedObjectives.Add(objective);
            Debug.Log("Objective object: " + name + " has been picked up");
        }


        protected virtual void OnTriggerEnter(Collider collider)
        {
            if (collider.GetComponent<PlayerMovement>())
                OnPickUp();
        }
    }
}
