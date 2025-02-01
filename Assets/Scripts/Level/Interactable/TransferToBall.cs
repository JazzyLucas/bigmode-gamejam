using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    public class TransferToBall : Interactable
    {
        [SerializeField] private GameObject electricBall;
        private Vector3 startPos;

        public override void Interact(GameObject interacter)
        {
            electricBall.transform.position = startPos;
            PlayerRefs.curPlayer.gameObject.SetActive(false);
            electricBall.SetActive(true);
        }

        private void Start()
        {
            startPos = electricBall.transform.position;
        }
    }
}
