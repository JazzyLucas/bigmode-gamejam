using BigModeGameJam.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigModeGameJam
{
    public class ElectricZoneObjective : MonoBehaviour
    {
        public GameObject electricBallPlayer;
        public GameObject particleEffects;
        public GameObject powerBox;
        public float scaleUp = 10;
        public float chargeValue = 0;
        public List<GameObject> lasersToEnable;
        public GameObject lightToEnable;
        public MeshRenderer scannerLight;
        private Coroutine coroutine = null;

        private void OnTriggerEnter(Collider other)
        {
            if(coroutine != null) return;
            coroutine = StartCoroutine(EndAnimation());
            IEnumerator EndAnimation()
            {
                PlayerTransitioner.Transition(Level.Controls.PlayerMovement.PlayerType.Human);
                float time = 0;
                while(time < PlayerTransitioner.TRANSITION_PERIOD / 2)
                {
                    yield return new WaitForEndOfFrame();
                    particleEffects.transform.localScale += new Vector3(scaleUp, scaleUp, scaleUp) * Time.deltaTime;
                    PlayerRefs.electricPlayer.playerHealth.RegenHealth(50 * Time.deltaTime);
                    time += Time.deltaTime;
                }
                electricBallPlayer.SetActive(false);

                InteractablePowerBox ipb = powerBox.GetComponent<InteractablePowerBox>();
                ipb.IsComplete = true;
                powerBox.GetComponent<BoxCollider>().enabled = false;
                lightToEnable.gameObject.SetActive(true);
                Material material = scannerLight.material;
                material.color = Color.red;  // Base color
                material.SetColor("_SpecColor", Color.red);  // Specular color
                material.SetColor("_EmissionColor", Color.red);  // Emission color
                material.EnableKeyword("_EMISSION");  // Make sure emission is enabled
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
