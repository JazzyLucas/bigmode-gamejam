using System.Collections.Generic;
using BigModeGameJam.Level.Interactables;

namespace BigModeGameJam.Level.Manager
{
    /// <summary>
    /// Manages Objectives, Ui info updates, and info to be tracked in the levels
    /// </summary>
    public static class LevelManager
    {
        static List<ObjectiveObject> allObjectives = new List<ObjectiveObject>();
        static List<ObjectiveListener> objectiveListeners = new List<ObjectiveListener>();

        /// <summary>
        /// Returns if passed Objective Object was assigned to an Objective Listener
        /// </summary>
        /// <param name="objectiveObjectToCheck"></param> Objective Object to check in list 
        /// <returns></returns>
        public static bool ObjectiveIsIncluded(ObjectiveObject objectiveObjectToCheck) => allObjectives.Contains(objectiveObjectToCheck);

        /// <summary>
        /// Will Initialize all objectives in the level and their Listeners
        /// </summary>
        /// <param name="objectives"></param> The objective objects to be used for the listener
        /// <param name="objectiveListener"></param> the listener that will do something when all objectives are used
        public static void InitializeObjectives(ObjectiveObject[] objectives, ObjectiveListener objectiveListener)
        {
            objectiveListeners.Add(objectiveListener);
            allObjectives.AddRange(objectives);
        }

        /// <summary>
        /// Will Uninitialize Objectives after they have been completed
        /// </summary>
        /// <param name="objectiveListener"></param> The listener to be removed from the listener list
        /// <param name="objectives"></param> The Objective objects to be removed from the objectives list
        public static void UninitializeObjectives(ObjectiveObject[] objectives, ObjectiveListener objectiveListener)
        {
            objectiveListeners.Remove(objectiveListener);

            //In the off chance a multiple listeners share objective objects but have different objective objects included in their objectives list one will remove old objectives first and the second to complete listener will need to make sure not remove null references
            foreach (ObjectiveObject objective in objectives)
            {
                if (allObjectives.Contains(objective)) //This is here to insure no null references are trying to be taken out of the allObjectives list
                    allObjectives.Remove(objective);
            }
        }

        /// <summary>
        /// This will go through all Objective Listeners in the Level and update their Objective Object lists to reflect passed object is interacted/collected
        /// </summary>
        /// <param name="objectiveObjectToCompelete"></param>
        public static void CompleteObjective(ObjectiveObject objectiveObjectToCompelete)
        {
            List<ObjectiveListener> listenersToUpdate = new List<ObjectiveListener>();
            listenersToUpdate.AddRange(objectiveListeners); //Decouple from objectiveListeners

            foreach (ObjectiveListener listener in listenersToUpdate)
            {
                listener.OnObjectiveComplete(objectiveObjectToCompelete);
            }



            //Update UI here
        }
    }
}
