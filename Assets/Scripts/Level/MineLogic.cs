using UnityEngine;

namespace BigModeGameJam.Level
{
    public class MineLogic : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            if(collider.TryGetComponent<PlayerHealth>(out PlayerHealth player))
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                gameObject.transform.GetChild(2).gameObject.SetActive(true);
                player.Die();
            }
        }
    }
}
