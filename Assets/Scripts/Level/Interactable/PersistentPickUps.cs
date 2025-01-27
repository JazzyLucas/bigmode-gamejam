using BigModeGameJam.Core;
using BigModeGameJam.Core.Manager;
using BigModeGameJam.Level.Controls;
using System.Collections.Generic;
using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    [ExecuteInEditMode] public class PersistentPickUps : MonoBehaviour, IPersistentOBJ
    {
        [SerializeField, ReadOnly] string uid;
        [SerializeField] int moneyValue;

        internal string UID { get { return uid; } }

        public void LoadData(GameData data)
        {
            if (data.PickedUpCollectableUIDS.Contains(uid) && GameManager.CurrentSceneType == SceneType.Level) //If this object has already been picked up by the player once and has come back to this level, he can't pick it up again
                Destroy(gameObject);
        }

#if UNITY_EDITOR
        private void Awake() //Make sure every UID is unique
        {
            if (!Application.isEditor)
                return;

            PersistentPickUps[] persistentPickUps = FindObjectsByType<PersistentPickUps>(FindObjectsSortMode.None);
            foreach (PersistentPickUps pickUps in persistentPickUps)
            {
                if (pickUps != this && pickUps.UID == uid)
                    uid = System.Guid.NewGuid().ToString();
            }
        }
#endif

        void Pickup()
        {
            GameManager.GameData.PickedUpCollectableUIDS.Add(uid);
            GameManager.GameData.Money += moneyValue;
            Debug.Log("Money: " + moneyValue.ToString());
            Debug.Log("Collectable : " + uid + " has been collected"); ;
            Destroy(gameObject);
        }
        

        public void OnTriggerEnter(Collider collider)
        {
            if(collider.GetComponent<PlayerMovement>() && !Application.isEditor)
                Pickup();
        }
    }
}
