using UnityEngine;

namespace BigModeGameJam.Level
{
    public class MineLogic : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
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
                }
            }
        }
    }
}
