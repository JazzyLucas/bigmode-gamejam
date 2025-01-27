using UnityEngine;

namespace BigModeGameJam.Core.Manager
{
    public static class GameManager
    {
        public static GameData GameData { get; private set; } = new GameData();
        public static SceneType CurrentSceneType { get; set; }

        internal static void PersistGame(GameData gameData)
        {
            if (gameData != null)
                GameData = gameData;
        }
    }
}
