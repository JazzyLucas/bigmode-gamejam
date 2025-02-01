using System.Collections;
using BigModeGameJam.Level.Controls;
using BigModeGameJam.UI;
using UnityEngine;

namespace BigModeGameJam.Level
{
    /// <summary>
    /// Container for references for important components pertaining to the attached player
    /// to reduce redundancy. All attributes should be assigned in the editor before runtime.
    /// </summary>
    public class PlayerRefs : MonoBehaviour
    {
        public static PlayerRefs humanPlayer, electricPlayer;
        // Should be assigned whenever player switches
        public static PlayerRefs curPlayer;
        // Assigned whenever player or view switches
        public static GameObject curCam;
        
        [SerializeField] public PlayerMovement.PlayerType playerType = PlayerMovement.PlayerType.Human;
        [Header("For both player types")]
        
        [SerializeField] new public CapsuleCollider collider;
        [SerializeField] new public Rigidbody rigidbody;
        [SerializeField] public Camera firstPersonCam;
        [SerializeField] public ThirdPersonCamera thirdPersonCam;
        [SerializeField] public PlayerMovement playerMovement;
        [SerializeField] public LookToInteract lookToInteract;
        [SerializeField] public PlayerHealth playerHealth;
        [SerializeField] public GameObject playerModel; // We can change the type of this when we start integrating animation
        [SerializeField] public Transform checkpoint;

        [Header("For electric character only")]
        [SerializeField] public ElectricMode electricMode;
        [SerializeField] public Dash dash;
        [SerializeField] public GameObject orb;


        /// <summary>
        /// Transition from first person to third person
        /// </summary>
        public void ThirdPerson()
        {
            firstPersonCam.gameObject.SetActive(false);
            thirdPersonCam.gameObject.SetActive(true);
            Crosshair.instance.gameObject.SetActive(false);
            curCam = thirdPersonCam.gameObject;
        }

        /// <summary>
        /// Transition from third person to first person
        /// </summary>
        public void FirstPerson()
        {
            firstPersonCam.gameObject.SetActive(true);
            thirdPersonCam.gameObject.SetActive(false);
            Crosshair.instance.gameObject.SetActive(true);
            curCam = firstPersonCam.gameObject;
        }

        /// <summary>
        /// Transition to the specified player
        /// </summary>
        /// <param name="playerType">The player you would like to set to be active</param>
        /// <param name="playerTo">The transform of the position to send the activated player to</param>
        public static void PlayerTransition(PlayerMovement.PlayerType playerType, Transform playerTo = null)
        {
            FadeEffect.StartAnimation(FadeEffect.Animation.Transition, Color.white, 3);
            curPlayer.StartCoroutine(PlayerTransitionCoroutine());
            IEnumerator PlayerTransitionCoroutine()
            {
                yield return new WaitForSeconds(1.5f);
                switch(playerType)
                {
                    case PlayerMovement.PlayerType.Human:
                        if(playerTo)
                            humanPlayer.transform.position = playerTo.position;
                        humanPlayer.gameObject.SetActive(true);
                        ElectricHUD.instance.gameObject.SetActive(false);
                        curPlayer = humanPlayer;
                        electricPlayer.gameObject.SetActive(false);
                        break;
                    case PlayerMovement.PlayerType.Electric:
                        if(playerTo)
                        {
                            electricPlayer.transform.position = playerTo.position;
                            electricPlayer.checkpoint = playerTo;
                        }
                        electricPlayer.gameObject.SetActive(true);
                        ElectricHUD.instance.gameObject.SetActive(true);
                        curPlayer = electricPlayer;
                        humanPlayer.gameObject.SetActive(false);
                        break;
                }
            }
            
        }

        private void Awake()
        {
            switch(playerType)
            {
                case PlayerMovement.PlayerType.Human:
                    humanPlayer = this;
                    break;
                case PlayerMovement.PlayerType.Electric:
                    electricPlayer = this;
                    break;
            }
            CheckMultipleInstances();
        }

        /// <summary>
        /// Disables non-primary player (electric) if both players are active
        /// </summary>
        private void CheckMultipleInstances()
        {
            if(humanPlayer && humanPlayer.gameObject.activeInHierarchy && 
                electricPlayer && electricPlayer.gameObject.activeInHierarchy)
            {
                electricPlayer.gameObject.SetActive(false);
                curPlayer = humanPlayer;
                return;
            }
            curPlayer = this;
        }

        private void OnEnabled()
        {
            curPlayer = this;
            curCam = firstPersonCam.gameObject;
        }
    }
}
