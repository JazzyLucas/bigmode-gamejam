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
            if (chargeValue < 100)
            {
                // Made framerate independent. - Jay
                chargeValue += Time.deltaTime * 60;
            }
            else
            {
                electricBallPlayer.SetActive(false);
                PlayerRefs.PlayerTransition(Level.Controls.PlayerMovement.PlayerType.Human);

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
