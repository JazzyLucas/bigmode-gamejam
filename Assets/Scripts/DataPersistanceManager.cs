using UnityEngine;

namespace BigModeGameJam.Core.Manager
{
    public class DataPersistanceManager : MonoBehaviour
    {
        void Start()
        {
            //Do singleton pattern here

            GameManager.GameData.DataPersistanceManager = this;
            LoadGame();
        }


        void NewGame()
        {

        }

        void LoadGame()
        {

        }


        void SaveGame()
        {

        }
    }
}
