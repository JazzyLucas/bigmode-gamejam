using BigModeGameJam.Core;
using BigModeGameJam.Core.Manager;
using System.Linq;
using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    [ExecuteInEditMode]
    public class PersistentInteractable : Interactable, IPersistentOBJ
    {
        [Header("Persistant Pick Up Configs")]
        [SerializeField] string uid;
        [SerializeField] int moneyValue;

        public string UID { get { return uid; } }

        public int MoneyValue { get { return moneyValue; } }

        public void LoadData(GameData data)
        {
            if (data.PickedUpCollectableUIDS.Contains(uid))
                OnLoadCustomCode();
        }

        protected void OnLoadCustomCode() //If this object has already been picked up by the player once and has come back to this level, he can't pick it up again
        {
            Destroy(gameObject);
        }

#if UNITY_EDITOR
        new void Awake() //Make sure every UID is unique
        {
            if (Application.isPlaying)
            {
                mesh = GetComponent<MeshRenderer>();
                return;
            }

            IPersistentOBJ[] persistentPickUps = FindObjectsByType<ObjectiveObject>(FindObjectsSortMode.None).OfType<IPersistentOBJ>().ToArray();
            foreach (ObjectiveObject persistant in persistentPickUps)
            {
                if (persistant != this && persistant.GetComponent<IPersistentOBJ>().UID == uid)
                    uid = System.Guid.NewGuid().ToString();
            }
        }
#endif

        public override void Interact(GameObject interacter)
        {
            base.Interact(interacter);// If you are overriding this function please do not remove this base (Also make sure the base statment is above your custom code)
            GameManager.GameData.PickedUpCollectableUIDS.Add(uid);
            GameManager.GameData.Money += moneyValue;
            Debug.Log("Money: " + moneyValue.ToString());
            Debug.Log("Collectable : " + uid + " has been collected");
            if (!canInteractMultipleTimes) //I can't imagine a Persistent Interactable being able to be used mutiple times, but the check for it is here just in case
                Destroy(gameObject);
        }
    }
}
