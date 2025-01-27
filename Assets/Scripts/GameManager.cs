using UnityEngine;

namespace BigModeGameJam.Core.Manager
{
    public static class GameManager
    {
        public static GameData GameData { get; private set; }

        internal static void PersistGame(GameData gameData)
        {
            if (gameData == null)
            {
                GameData = new GameData();
                return;
            }

            GameData = gameData;
        }
    }
}
