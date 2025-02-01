using BigModeGameJam.Level;
using System.Collections.Generic;
using UnityEngine;

namespace BigModeGameJam
{
    public class ElectricZoneObjective : MonoBehaviour
    {
        public GameObject player;
        public GameObject electricBallPlayer;
        public GameObject powerBox;
        public float chargeValue = 0;
        public List<GameObject> lasersToEnable;

        private void OnTriggerStay(Collider other)
        {
            if (chargeValue != 100)
            {
                chargeValue++;
            }
            else
            {
                player.SetActive(true);
                electricBallPlayer.SetActive(false);

                InteractablePowerBox ipb = powerBox.GetComponent<InteractablePowerBox>();
                ipb.IsComplete = true;
                powerBox.GetComponent<BoxCollider>().enabled = false;
                if (lasersToEnable.Count > 0)
                {
                    foreach (GameObject l in lasersToEnable)
                    {
                        l.GetComponent<BoxCollider>().enabled = true;
                        l.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
            }
        }
    }
}
