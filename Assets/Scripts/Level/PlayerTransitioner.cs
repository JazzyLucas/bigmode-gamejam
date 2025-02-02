using UnityEngine;
using BigModeGameJam.Level.Controls;
using BigModeGameJam.UI;
using System.Collections;
using BigModeGameJam.Core;
using FMOD.Studio;
using static BigModeGameJam.Level.Controls.PlayerMovement;
using FMODUnity;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using Unity.VisualScripting.YamlDotNet.Core;
using static UnityEditor.Profiling.RawFrameDataView;
namespace BigModeGameJam.Level
{
    public class PlayerTransitioner : MonoBehaviour
    {
        public const float TRANSITION_PERIOD = 3;
        public static PlayerTransitioner instance;
        /// <summary>
        /// Transition to the specified player
        /// </summary>
        /// <param name="playerType">The player you would like to set to be active</param>
        /// <param name="playerTo">The transform of the position to send the activated player to</param>
        public static void Transition(PlayerMovement.PlayerType playerType, Transform playerTo = null)
        {
            FadeEffect.StartAnimation(FadeEffect.Animation.Transition, Color.white, TRANSITION_PERIOD);
            instance.StartCoroutine(PlayerTransitionCoroutine());
            PlayerRefs.curPlayer.lookToInteract.enabled = false;
            IEnumerator PlayerTransitionCoroutine()
            {
                yield return new WaitForSeconds(TRANSITION_PERIOD / 2);
                switch (playerType)
                {
                    case PlayerMovement.PlayerType.Human:
                        if (playerTo)
                            PlayerRefs.humanPlayer.transform.position = playerTo.position;
                        PlayerRefs.humanPlayer.gameObject.SetActive(true);
                        ElectricHUD.instance.gameObject.SetActive(false);
                        PlayerRefs.curPlayer = PlayerRefs.humanPlayer;
                        PlayerRefs.electricPlayer.gameObject.SetActive(false);
                        EventInstance.setParameterByName("ElectricTrans", 0);
                        PlayerRefs.humanPlayer.lookToInteract.enabled = true;
                        break;
                    case PlayerMovement.PlayerType.Electric:
                        if (playerTo)
                        {
                            PlayerRefs.electricPlayer.transform.position = playerTo.position;
                            PlayerRefs.electricPlayer.transform.forward = playerTo.forward;
                            PlayerRefs.electricPlayer.checkpoint = playerTo;
                        }
                        PlayerRefs.electricPlayer.gameObject.SetActive(true);
                        ElectricHUD.instance.gameObject.SetActive(true);
                        PlayerRefs.curPlayer = PlayerRefs.electricPlayer;
                        PlayerRefs.humanPlayer.gameObject.SetActive(false);
                        EventInstance.setParameterByName("ElectricTrans", 1);
                        PlayerRefs.electricPlayer.lookToInteract.enabled = true;
                        break;
                }
            }
        }
        private void Awake()
        {
            instance = this;
        }

        FMODUnity.StudioEventEmitter emitter;
        void OnEnable()
        {
            var target = GameObject.Find("Music");
            emitter = target.GetComponent<FMODUnity.StudioEventEmitter>();
        }

        void Update()
        {
            float value = 1.0f; // calculate the value every frame
            emitter.SetParameter("ParameterName", value);
        }
    }
}
