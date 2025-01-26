using UnityEngine;

namespace BigModeGameJam.Core.Manager
{
    public interface IPersistentOBJ
    {
        void LoadData(GameData data);
        void SaveData(ref GameData data);
    }
}
