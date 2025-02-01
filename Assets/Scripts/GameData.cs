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
        public float masterVolume = 0.8f, musicVolume = 0.8f, SFXVolume = 0.8f;
        public float lookSensitivity;
    }
}
