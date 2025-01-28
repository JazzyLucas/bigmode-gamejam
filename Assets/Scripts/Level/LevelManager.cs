using System.Collections.Generic;
using BigModeGameJam.Level.Interactables;

namespace BigModeGameJam.Level.Manager
{
    public static class LevelManager
    {
        static List<ObjectiveObject> allObjectives = new List<ObjectiveObject>();
        public static List<ObjectiveListener> ObjectiveListeners = new List<ObjectiveListener>();

        public static List<ObjectiveObject> AllObjectives { get { return allObjectives; } }

        public static void InitializeObjectives(ObjectiveObject[] objectives)
        {
            allObjectives.AddRange(objectives);
        }
        /// <summary>
        /// This will go through all Objective Listeners in the Level and update their Objective Object lists to reflect passed object is interacted/collected
        /// </summary>
        /// <param name="objectiveObjectToCompelete"></param>
        public static void CompleteObjective(ObjectiveObject objectiveObjectToCompelete)
        {
            foreach (ObjectiveListener listener in ObjectiveListeners)
            {
                listener.OnObjectiveComplete(objectiveObjectToCompelete);
            }

            //Update UI here
        }
    }
}
