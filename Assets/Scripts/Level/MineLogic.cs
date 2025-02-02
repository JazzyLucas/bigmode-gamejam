using System.Collections;
using BigModeGameJam.Core;
using BigModeGameJam.Level.Controls;
using UnityEngine;
using static BigModeGameJam.Level.Controls.PlayerMovement;

namespace BigModeGameJam.Level
{
    public class MineLogic : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Boom, this.transform.position);
            if(collider.CompareTag("Player"))
            {
                StartCoroutine(WaitAndDestroy());
                PlayerHealth player = collider.GetComponent<PlayerHealth>();
                gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                GameObject dynamite = gameObject.transform.GetChild(1).gameObject;
                dynamite.gameObject.GetComponent<MeshRenderer>().enabled = false;
                dynamite.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                dynamite.transform.GetChild(1).GetComponent<Light>().enabled = false;
                gameObject.transform.GetChild(2).gameObject.SetActive(true);
                if (player != null && !player.isDead)
                {
                    player.Die();
                    if (collider.GetComponent<PlayerMovement>().playerType == PlayerType.Human)
                        AudioManager.instance.PlayOneShot(FMODEvents.instance.Death, this.transform.position);
                    else AudioManager.instance.PlayOneShot(FMODEvents.instance.ElectricDeath, this.transform.position);
                }
            }

            IEnumerator WaitAndDestroy()
            {
                yield return new WaitForSeconds(1);
                Destroy(this.gameObject);
            }
        }
    }
}
