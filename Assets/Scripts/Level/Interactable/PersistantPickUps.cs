using BigModeGameJam.Core;
using BigModeGameJam.Core.Manager;
using BigModeGameJam.Level.Controls;
using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    public class PersistantPickUps : Interactable, IPersistentOBJ
    {
        [SerializeField, ReadOnly] string uid;
        [SerializeField] int moneyValue;

        public void LoadData(GameData data)
        {
            if (data.PickedUpCollectableUIDS.Contains(uid) && GameManager.CurrentSceneType == SceneType.Level) //If this object has already been picked up by the player once and has come back to this level, he can't pick it up again
                Destroy(gameObject);
        }

        public override void Interact(GameObject interacter)
        {
            PlayerControls player = interacter.GetComponent<PlayerControls>();

            if (!player)
            {
                Debug.LogError("A non player or Player without PlayerControl script has interacted with " + name + " interactable");
                return;
            }

            GameManager.GameData.PickedUpCollectableUIDS.Add(uid);
            GameManager.GameData.Money += moneyValue;
            Debug.Log("Money: " + moneyValue.ToString());
            Debug.Log("Collectable : " + uid + " has been collected"); 
            //player.CurrentInteractable = null;
            Destroy(gameObject);
        }

        void Reset()
        {
            uid = System.Guid.NewGuid().ToString();
        }
    }
}
