using System;
using System.Collections.Generic;

namespace BigModeGameJam.Core
{
    /// <summary>
    /// Data that will persist throughout the whole game. Money, Collectables, and Level High score
    /// </summary>
    [Serializable] public class GameData
    {

        public int Money;

        public List<string> PickedUpCollectableUIDS = new List<string>();
    }
}
