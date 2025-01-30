using System.Collections.Generic;
using System.Diagnostics;
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

        public static UIObjectiveManager UIObjectiveManager { get; set; }

        public static bool UIInUse { get; private set; }

        /// <summary>
        /// Returns if passed Objective Object was assigned to an Objective Listener
        /// </summary>
        /// <param name="objectiveObjectToCheck"> Objective Object to check in list  </param> 
        /// <returns></returns>
        public static bool ObjectiveIsIncluded(ObjectiveObject objectiveObjectToCheck) => allObjectives.Contains(objectiveObjectToCheck);

        /// <summary>
        /// Give Level manager reference to the Objectives Object and Listener Text on the UI
        /// </summary>
        /// <param name="uiObject"> The UI objectives object</param> 
        public static void CreateNewUIObjective(ObjectiveListener objectiveListener)
        {
            List<string> stringsToPass = new List<string>();

            foreach (ObjectiveObject objective in objectiveListener.ObjectiveObjects)
            {
                if (objective.IsComplete)
                    objective.StrikeOutUIMessage();

                stringsToPass.Add(objective.UIMessage);
            }


            UIObjectiveManager.CreateNewObjectiveDisplay(objectiveListener.UIMessage, stringsToPass.ToArray());
            UIInUse = true;
        }

        /// <summary>
        /// Will Initialize all objectives in the level and their Listeners
        /// </summary>
        /// <param name="objectives">The objective objects to be used for the listener</param> 
        /// <param name="objectiveListener">the listener that will do something when all objectives are used</param> 
        public static void InitializeObjectives(ObjectiveObject[] objectives, ObjectiveListener objectiveListener)
        {
            objectiveListeners.Add(objectiveListener);
            allObjectives.AddRange(objectives);
        }

        /// <summary>
        /// Will Uninitialize Objectives after they have been completed
        /// </summary>
        /// <param name="objectiveListener">The listener to be removed from the listener list</param> 
        /// <param name="objectives">The Objective objects to be removed from the objectives list</param> 
        public static void UninitializeObjectives(ObjectiveObject[] objectives, ObjectiveListener objectiveListener)
        {
            objectiveListeners.Remove(objectiveListener);

            //In the off chance a multiple listeners share objective objects but have different objective objects included in their objectives list one will remove old objectives first and the second to complete listener will need to make sure not remove null references
            foreach (ObjectiveObject objective in objectives)
            {
                if (allObjectives.Contains(objective)) //This is here to insure no null references are trying to be taken out of the allObjectives list
                    allObjectives.Remove(objective);
            }

            UIObjectiveManager.OnAllObjectiveFinish();
            UIInUse = false;
        }

        /// <summary>
        /// This will go through all Objective Listeners in the Level and update their Objective Object lists to reflect passed object is interacted/collected
        /// </summary>
        /// <param name="objectiveObjectToCompelete">The Objective that completed</param>
        public static void CompleteObjective(ObjectiveObject objectiveObjectToCompelete)
        {
            objectiveObjectToCompelete.IsComplete = true;
            List<ObjectiveListener> listenersToUpdate = new List<ObjectiveListener>();//fuck me i'll lazy cuz I don't feel like fucking with LinkedLists rn
            listenersToUpdate.AddRange(objectiveListeners); //Decouple from objectiveListeners 

            foreach (ObjectiveListener listener in listenersToUpdate)
            {
                listener.OnObjectiveComplete(objectiveObjectToCompelete);
            }

            UIObjectiveManager.OnDisplayUpdate(objectiveObjectToCompelete.UIMessage);
        }
    }
}
