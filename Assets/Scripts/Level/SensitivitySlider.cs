using UnityEngine;
using UnityEngine.UI;
using BigModeGameJam.Level.Controls;
using TMPro;

namespace BigModeGameJam.UI
{
    [RequireComponent(typeof(Slider))]
    public class SensitivitySlider : MonoBehaviour
    {
        private Slider slider;
        [SerializeField] private TMP_Text textBox;

        public void UpdateValue( )
        {
            PlayerControls.lookSensitivity = slider.value;
            textBox.text = $"SENSITIVITY: {slider.value:0.00}";
        }

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }
    }
}
