using BigModeGameJam.Level.Controls;
using BigModeGameJam.Level.Interactables;
using UnityEngine;

namespace BigModeGameJam.Level
{
    public class PickUp : ObjectiveObject
    {
        protected virtual void OnPickUp()
        {
            SendToLevelManger();
            Debug.Log("Objective object: " + gameObject.name + " has been picked up");
            Destroy(gameObject);
        }


        protected virtual void OnTriggerEnter(Collider collider)
        {
            if (collider.GetComponent<PlayerMovement>())
                OnPickUp();
        }
    }
}
