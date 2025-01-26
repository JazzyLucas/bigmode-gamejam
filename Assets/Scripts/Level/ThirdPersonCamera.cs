using System.Collections;
using UnityEngine;

namespace BigModeGameJam.Level.Controls
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        private const float VERTICAL_MOD = 0.45f;
        private const float TRANSITION_PERIOD = 0.25f;
        /// <summary>
        /// The object that this camera will reference for angle and position.
        /// </summary>
        public Transform follow;
        public float dist = 20;

        public void OnEnable()
        {
            float targetDist = dist;
            dist = 5;
            StartCoroutine(ActivateAnimation(targetDist));
        }

        private IEnumerator ActivateAnimation(float targetDist)
        {
            float rate = (targetDist - dist) / TRANSITION_PERIOD;
            float time = 0;
            while(time < TRANSITION_PERIOD)
            {
                dist += rate * Time.deltaTime;
                yield return new WaitForEndOfFrame();
                time += Time.deltaTime;
            } 
            dist = targetDist;
        }

        private void Update()
        {
            transform.position = follow.position;
            transform.forward = follow.forward;
            // Get final position
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.forward, out hit, dist))
            {
                // "collide" with ground
                transform.position = hit.point;
                transform.Translate(Vector3.up * VERTICAL_MOD);
                return;
            }
            transform.Translate(Vector3.forward * -dist + Vector3.up * VERTICAL_MOD);
        }
    }
}