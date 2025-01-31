using UnityEngine;

namespace BigModeGameJam.Core.Manager
{
    public interface IPersistentOBJ
    {
        public void LoadData(GameData data);
        public string UID { get; }
        public int MoneyValue { get; }
    }
}
