using BigModeGameJam.Core;
using BigModeGameJam.Level.Manager;
using System.Linq;
using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    /// <summary>
    /// Parent class for Interactables and Pick ups. Allows said classes to become Objectives tied to Objective Listeners
    /// </summary>
    abstract public class ObjectiveListener : MonoBehaviour
    {
        [Header("Objective Object Config: (Only set if set in UI Objective Manager)")]
        [SerializeField] protected string uiMessage; //Currently not used

        internal string UIMessage { get { return uiMessage; } }

        [Header("Objective Config")]
        [SerializeField] int completedObjectiveCount;

        [SerializeField] ObjectiveObject[] objectiveObjects;

        internal ObjectiveObject[] ObjectiveObjects { get { return objectiveObjects; } }

        void Awake()
        {
            //These are check for the game devs not to screw up

            ObjectiveListener[] objectiveListeners = GetComponents<ObjectiveListener>();

            if (objectiveListeners.Count() > 1)
                Debug.LogError(objectiveListeners.Count().ToString() + " ObjectiveListener scripts found on " + gameObject.name + " please make sure only 1 is present on the object");

            int checkForDuplicates = 0;

            foreach (ObjectiveObject objectiveObject in objectiveObjects)
            {
                foreach (ObjectiveObject objectiveObjectToCheck in objectiveObjects)
                {
                    if (objectiveObjectToCheck == objectiveObject)
                        checkForDuplicates++;
                }

                if (checkForDuplicates > 1)
                    Debug.LogError(checkForDuplicates.ToString() + " duplicate Objective Objects found in " + gameObject.name + " Objective Config please fix");

                checkForDuplicates = 0;
            }
        }

        protected virtual void Start()
        {
            LevelManager.InitializeObjectives(objectiveObjects, this);
        }

        internal void OnObjectiveComplete(ObjectiveObject objectiveFromManager)
        {
            if (objectiveObjects.Contains(objectiveFromManager))
                completedObjectiveCount++;

            if (completedObjectiveCount == objectiveObjects.Length)
            {
                FinishAllObjectives();
                return;
            }

            Debug.Log("Number of objectives to complete for " + gameObject.name + " to complete all objectives: " + (objectiveObjects.Length - completedObjectiveCount).ToString());
        }

        void FinishAllObjectives()
        {
            OnFinishAllCustomCode();
            LevelManager.UninitializeObjectives(objectiveObjects, this);
            Debug.Log("All objectives in: " + gameObject.name + " have been completed");
        }

        protected virtual void OnFinishAllCustomCode() { }
    }
}
