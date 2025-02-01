using BigModeGameJam.Core;
using BigModeGameJam.Level.Controls;
using UnityEngine;

namespace BigModeGameJam.Level
{
    public class KillTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            if(collider.TryGetComponent<PlayerHealth>(out PlayerHealth player))
            {
                player.Die();
                AudioManager.instance.PlayOneShot(FMODEvents.instance.Death, this.transform.position);
            }
        }
    }
}
