using UnityEngine;
using BigModeGameJam.Level.Manager;
using System.Linq;
using BigModeGameJam.Core;

namespace BigModeGameJam.Level.Interactables
{
    /// <summary>
    /// Parent class for Interactables and Pick ups. Allows said classes to become Objectives tied to Objective Listeners
    /// </summary>
    public class ObjectiveListener : MonoBehaviour
    {
        [Header("Objective Config")]
        [SerializeField, ReadOnly] int completedObjectiveCount;

        [SerializeField] ObjectiveObject[] objectiveObjects;

        protected virtual void Start()
        {
            LevelManager.InitializeObjectives(objectiveObjects);
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

        protected virtual void FinishAllObjectives()
        {
            LevelManager.ObjectiveListeners.Remove(this);
            Debug.Log("All objectives in: " + gameObject.name + " have been completed");
        }
    }
}
