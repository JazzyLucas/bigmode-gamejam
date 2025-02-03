using UnityEngine;
using UnityEngine.UI;
using BigModeGameJam.Level.Controls;
using TMPro;
using BigModeGameJam.Core.Manager;
using BigModeGameJam.Core;

namespace BigModeGameJam.UI
{
    [RequireComponent(typeof(Slider))]
    public class SensitivitySlider : MonoBehaviour, IPersistentOBJ
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TMP_Text textBox;

        public void UpdateValue()
        {
            PlayerControls.lookSensitivity = slider.value;
            textBox.text = $"SENSITIVITY: {slider.value:0.00}";
            GameManager.GameData.lookSensitivity = slider.value;
        }

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        // Persistence implementation
        
        public void LoadData(GameData data)
        {
            slider.value = data.lookSensitivity;
            UpdateValue();
        }

        // ignore these. they're just in the interface
        public string UID { get; }
        public int MoneyValue { get; }
    }
}
