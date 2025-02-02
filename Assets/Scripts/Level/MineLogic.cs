using System.Collections;
using BigModeGameJam.Core;
using UnityEngine;

namespace BigModeGameJam.Level
{
    public class MineLogic : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Boom, this.transform.position);
            if(collider.CompareTag("Player"))
            {
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
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.Death, this.transform.position);
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
