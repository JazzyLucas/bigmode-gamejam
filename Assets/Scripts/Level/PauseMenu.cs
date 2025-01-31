using BigModeGameJam.Level.Manager;
using UnityEngine;
using TMPro;
using BigModeGameJam.Core.Manager;

namespace BigModeGameJam.Level
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] GameObject allObjects;
        [SerializeField] GameObject moneyText;

        void Awake()
        {
            gameObject.SetActive(false);
        }

        void Start()
        {
            LevelManager.PauseMenu = this;
        }

        internal void PauseGame()
        {
            Time.timeScale = 0.0f;
            gameObject.SetActive(true);
            moneyText.GetComponent<TextMeshProUGUI>().text = GameManager.GameData.Money.ToString();
        }

        internal void UnpauseGame()
        {
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);
        } 
    }
}
