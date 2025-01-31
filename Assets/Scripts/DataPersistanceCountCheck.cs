using BigModeGameJam.Core.Manager;
using UnityEngine;

namespace BigModeGameJam.Core
{
    public class DataPersistanceCountCheck : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            DataPersistanceManager[] allDataPersistanceManagers = FindObjectsByType<DataPersistanceManager>(FindObjectsSortMode.None);

            if (allDataPersistanceManagers.Length >= 1)
            {
                int size = allDataPersistanceManagers.Length;

                for (int i = 0; i < size - 1; i++)
                {
                    Destroy(allDataPersistanceManagers[i].gameObject);
                }
            }
            //Do singleton pattern here
        }
    }
}
