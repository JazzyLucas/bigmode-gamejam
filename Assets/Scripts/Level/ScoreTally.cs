using UnityEngine;
using TMPro;
using BigModeGameJam.Core;
using BigModeGameJam.Core.Manager;
using System.Collections;

namespace BigModeGameJam.Level
{
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreTally : MonoBehaviour
    {
        private const int GOLD = 4440, SILVER = 2500, BRONZE = 1000;
        [SerializeField] private GameObject goldTrophy, silverTrophy, bronzeTrophy;
        private TMP_Text textBox;

        private void Awake()
        {
            textBox = GetComponent<TMP_Text>();
            OnEnable();
        }
        private void OnEnable()
        {
            int money = GameManager.GameData.Money;
            StartCoroutine(TickUpAnimation(money));
            
        }

        private IEnumerator TickUpAnimation(int money)
        {
            int m = 0;
            while(m < money)
            {
                m += 10;
                if(m > money) m = money;
                textBox.text = $"${m}!";
                if(m > GOLD)
                    goldTrophy.SetActive(true);
                if (m > SILVER)
                    silverTrophy.SetActive(true);
                if (m > BRONZE)
                    bronzeTrophy.SetActive(true);
                yield return new WaitForSeconds(1f/30f);
            }
        }
    }
}
