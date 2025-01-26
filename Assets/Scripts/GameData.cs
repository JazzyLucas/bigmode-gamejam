using UnityEngine;
using BigModeGameJam.Core.Manager;

namespace BigModeGameJam.Core
{
    /// <summary>
    /// Data that will persist throughout the whole game. Money, Collectables, and Level High score
    /// </summary>
    public class GameData
    {

        public DataPersistanceManager DataPersistanceManager { get; set; }

        public int Money { get; private set; }


        public void ModifyMoney(int moneyChange)
        {
            Money += moneyChange;
        }
    }
}
