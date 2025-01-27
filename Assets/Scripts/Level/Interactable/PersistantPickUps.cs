using BigModeGameJam.Core;
using BigModeGameJam.Core.Manager;
using BigModeGameJam.Level.Controls;
using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    public class PersistantPickUps : MonoBehaviour, IPersistentOBJ
    {
        [SerializeField, ReadOnly] string uid;
        [SerializeField] int moneyValue;

        public void LoadData(GameData data)
        {
            if (data.PickedUpCollectableUIDS.Contains(uid) && GameManager.CurrentSceneType == SceneType.Level) //If this object has already been picked up by the player once and has come back to this level, he can't pick it up again
                Destroy(gameObject);
        }

        public void Pickup()
        {
            GameManager.GameData.PickedUpCollectableUIDS.Add(uid);
            GameManager.GameData.Money += moneyValue;
            Debug.Log("Money: " + moneyValue.ToString());
            Debug.Log("Collectable : " + uid + " has been collected"); 
            //player.CurrentInteractable = null;
            Destroy(gameObject);
        }

        public void OnTriggerEnter(Collider collider)
        {
            if(collider.GetComponent<PlayerMovement>() != null)
            {
                Pickup();
            }
        }

        void Reset()
        {
            uid = System.Guid.NewGuid().ToString();
        }
    }
}
