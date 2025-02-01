using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace BigModeGameJam.Core.Manager
{
    internal class DataPersistanceManager : MonoBehaviour
    {
        [Header("File Configs")]
        [SerializeField] string fileName = "GameSave.json"; //Change this to the name of the game 

        static SaveFileWriter fileWriter;
        static bool firstStartUp = true; //Tracks if this was the first time the game was opened
        List<IPersistentOBJ> dataPersistanceOBJs;

        void Start()
        {
            DontDestroyOnLoad(gameObject);
            dataPersistanceOBJs = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IPersistentOBJ>().ToList();
            LoadGame();
        }

        private void OnApplicationQuit()
        {
            fileWriter.SaveData(GameManager.GameData);
            Debug.Log("Data has been saved");
        }

        void LoadGame()
        {
            Debug.Log("loaded game");
            if (firstStartUp)
            {
                fileWriter = new SaveFileWriter(Application.persistentDataPath, fileName);
                GameManager.PersistGame(fileWriter.LoadData());
                firstStartUp = false;
                Debug.Log("Data has been loaded");
            }

            foreach (IPersistentOBJ persistentOBJ in dataPersistanceOBJs)
            {
                persistentOBJ.LoadData(GameManager.GameData);
            }
        }
    }
}
