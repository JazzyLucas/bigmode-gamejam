using BigModeGameJam.Core.Manager;
using UnityEngine;

namespace BigModeGameJam.Level.Object
{
    public class MoneyPickUp : MonoBehaviour
    {
        [SerializeField] int value = 1;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                AddMoney();
        }

        void AddMoney()
        {
            GameManager.GameData.Money += value; //This is for testing persistance
            GameManager.GameData.PickedUpCollectableUIDS.Add("sdf"); //This is for testing persistance
        }
    }
}
