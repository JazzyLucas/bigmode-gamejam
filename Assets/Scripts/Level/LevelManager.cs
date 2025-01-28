using System.Collections.Generic;
using UnityEngine;

namespace BigModeGameJam.Level.Manager
{
    public static class LevelManager
    {
        static List<Objective> allObjectives = new List<Objective>();
        public static List<Objective> CompleteedObjectives = new List<Objective>();
        public static List<IObjectiveListener> ObjectiveListeners = new List<IObjectiveListener>();

        public static void InitializeObjectives(IObjectiveListener objectiveListener)
        {
            allObjectives.AddRange(objectiveListener.Objectives);
        }
    }

    public class Objective
    {
        public string name;
    }
}
