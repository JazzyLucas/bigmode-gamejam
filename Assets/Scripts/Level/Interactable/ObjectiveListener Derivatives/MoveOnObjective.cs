using BigModeGameJam.Level.Interactables;
using UnityEngine;

namespace BigModeGameJam.Level
{
    public class MoveOnObjective : ObjectiveListener
    {
        [SerializeField] float speed = 2;
        [SerializeField] Transform transformToMoveTo;
        bool objectIsMoving;

        protected override void OnFinishAllCustomCode()
        {
            objectIsMoving = true;
        }

        void Update()
        {
            if (Vector3.Distance(transform.position, transformToMoveTo.position) < 0.001f)
                objectIsMoving = false;

            if (objectIsMoving)
                transform.position = Vector3.MoveTowards(transform.position, transformToMoveTo.position, speed * Time.deltaTime);
        }
    }
}
