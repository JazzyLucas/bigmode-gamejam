using UnityEngine;
using UnityEngine.UI;

namespace BigModeGameJam.UI
{
    [RequireComponent(typeof(Image))]
    public class Crosshair : MonoBehaviour
    {
        public static Crosshair instance;
        public Sprite unhoverImg;
        public Sprite hoverImg;

        private Image img;

        public void SetHover(bool h)
        {
            if(h)
            {
                img.sprite = hoverImg;
            }
            else
            {
                img.sprite = unhoverImg;
            }
        }

        private void Awake()
        {
            instance = this;
            img = GetComponent<Image>();
        }
    }
}
