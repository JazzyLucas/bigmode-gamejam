using BigModeGameJam.Level.Controls;
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
        
        [SerializeField] PlayerMovement.PlayerType playerType = PlayerMovement.PlayerType.Human;

        [Header("For both player types")]
        [SerializeField] new readonly CapsuleCollider collider;
        [SerializeField] new readonly Rigidbody rigidbody;
        [SerializeField] readonly Camera firstPersonCam;
        [SerializeField] readonly ThirdPersonCamera thirdPersonCamera;
        [SerializeField] readonly PlayerMovement playerMovement;
        [SerializeField] readonly LookToInteract lookToInteract;

        [Header("For electric character only")]
        [SerializeField] readonly ElectricMode electricMode;
        [SerializeField] readonly Dash dash;

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
    }
}
