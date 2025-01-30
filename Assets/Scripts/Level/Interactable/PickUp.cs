using BigModeGameJam.Level.Controls;
using BigModeGameJam.Level.Interactables;
using BigModeGameJam.Level.Manager;
using UnityEngine;

namespace BigModeGameJam.Level
{
    public class PickUp : ObjectiveObject
    {
        protected void OnPickUp()
        {
            if (GetType() == typeof(UIObjectiveTrigger) && LevelManager.UIInUse) //Ignore this it's only for the UI stuff (If I had more time i wouldn't be so autistic with this) 
                return;

            OnCustomPickUpCode();
            SendToLevelManger();
            Debug.Log("Objective object: " + gameObject.name + " has been picked up");
            Destroy(gameObject);
        }

        protected virtual void OnCustomPickUpCode() { }


        protected virtual void OnTriggerEnter(Collider collider)
        {
            if (collider.GetComponent<PlayerMovement>())
                OnPickUp();
        }
    }
}
