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

        [Header("For electric character only")]
        [SerializeField] public ElectricMode electricMode;
        [SerializeField] public Dash dash;


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

        private void Awake()
        {
            switch(playerType)
            {
                case PlayerMovement.PlayerType.Human:
                    humanPlayer = this;
                    return;
                case PlayerMovement.PlayerType.Electric:
                    electricPlayer = this;
                    return;
            }
        }

        private void OnEnabled()
        {
            curPlayer = this;
            curCam = firstPersonCam.gameObject;
        }
    }
}
