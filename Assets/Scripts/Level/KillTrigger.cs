using BigModeGameJam.Level.Controls;
using UnityEngine;

namespace BigModeGameJam.Level
{
    public class KillTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            if(collider.TryGetComponent<PlayerMovement>(out PlayerMovement player))
            {
                player.Die();
            }
        }
    }
}
