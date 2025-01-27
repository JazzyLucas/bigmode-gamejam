using UnityEngine;
using System;
using System.IO;

namespace BigModeGameJam.Core.Manager
{
    internal class SaveFileWriter
    {
        string dirPath = "";
        string fileName = "";

        string fullPath
        {
            get
            {
                return Path.Combine(dirPath, fileName);
            }
        }

        internal SaveFileWriter(string path, string name)
        {
            dirPath = path;
            fileName = name;
        }

        /// <summary>
        /// Trying to get data from JSON file and turn it into GameData C# data
        /// </summary>
        /// <returns></returns>
        internal GameData LoadData()
        {
            GameData loadData = null;
            if (File.Exists(fullPath))
            {
                try
                {
                    string dataToLoad;
                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                            dataToLoad = reader.ReadToEnd();
                    }

                    loadData = JsonUtility.FromJson<GameData>(dataToLoad);
                }
                catch(Exception e)
                {
                    Debug.LogError("Error when tring to laod data from game JSON file: " + fullPath + "\n" + e);
                }
            }

            return loadData;
        }

        /// <summary>
        /// Trys to write game data to JSON file in local directory
        /// </summary>
        /// <param name="data"></param> The Game Data to be saved
        internal void SaveData(GameData data)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                string dataToStore = JsonUtility.ToJson(data, true);
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                        writer.Write(dataToStore);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error when trying to save game data to JSON file: " + fullPath + "\n" + e);
            }
        }
    }
}
