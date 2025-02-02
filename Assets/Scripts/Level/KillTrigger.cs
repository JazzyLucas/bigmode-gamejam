using BigModeGameJam.Core;
using BigModeGameJam.Level.Controls;
using UnityEngine;
using static BigModeGameJam.Level.Controls.PlayerMovement;

namespace BigModeGameJam.Level
{
    public class KillTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            if(collider.TryGetComponent<PlayerHealth>(out PlayerHealth player))
            {
                player.Die();

                if (collider.GetComponent<PlayerMovement>().playerType == PlayerType.Human)
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.Death, this.transform.position);
                else AudioManager.instance.PlayOneShot(FMODEvents.instance.ElectricDeath, this.transform.position);
            }
        }
    }
}
