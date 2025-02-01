using UnityEngine;

namespace BigModeGameJam.Level
{
    [RequireComponent(typeof(Collider)), RequireComponent(typeof(MeshRenderer))]
    public class CheckpointTrigger : MonoBehaviour
    {
        [SerializeField] private Transform checkpointPosition;
        private void Awake()
        {
            // Be visible in editor, not during gameplay
            GetComponent<MeshRenderer>().enabled = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<PlayerRefs>(out PlayerRefs p))
            {
                // here if unassigned
                p.checkpoint = checkpointPosition ? checkpointPosition : transform;
            }
        }
    }
}
