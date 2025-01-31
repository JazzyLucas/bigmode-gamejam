using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BigModeGameJam.UI
{
    [RequireComponent(typeof(Image))]
    public class FadeEffect : MonoBehaviour
    {
        private static FadeEffect instance;
        public enum Animation : int
        {
            FadeOut = 0, // Gameplay fades out, not image
            FadeIn = 1,
            Transition = 2 // Fade out then fade in
        }

        public float period = 2;
        private Image img;

        /// <summary>
        /// Plays fading animation
        /// </summary>
        /// <param name="a">Animation to play</param>
        /// <param name="c">Color to set the image to</param>
        /// <param name="p">Optionally specify length of animation. If < 0, use field</param>
        public static void StartAnimation(Animation a, Color c, float p = -1)
        {
            if(instance == null)
            {
                Debug.LogError("Fade Effect is missing, but has been called");
                return;
            }

            if(p < 0)
            {
                p = instance.period;
            }

            switch(a)
            {
                case Animation.FadeOut:
                    instance.StartCoroutine(FadeOut());
                    return;
                case Animation.FadeIn:
                    instance.StartCoroutine(FadeIn());
                    return;
                case Animation.Transition:
                    instance.StartCoroutine(Transition());
                    return;
            }

            IEnumerator FadeOut()
            {
                instance.img.color = new Color(c.r, c.g, c.b, 0);
                while(instance.img.color.a < 1)
                {
                    c = instance.img.color;
                    instance.img.color = new Color(c.r, c.g, c.b, c.a + Time.deltaTime / p);
                    yield return new WaitForEndOfFrame();
                }
            }

            IEnumerator FadeIn()
            {
                instance.img.color = c;
                while(instance.img.color.a > 0)
                {
                    c = instance.img.color;
                    instance.img.color = new Color(c.r, c.g, c.b, c.a - Time.deltaTime / p);
                    yield return new WaitForEndOfFrame();
                }
            }

            IEnumerator Transition()
            {
                p /= 2; // Half time dedicated to fade in and fade out
                yield return instance.StartCoroutine(FadeOut());
                instance.StartCoroutine(FadeIn());
            }
        }

        private void Awake()
        {
            instance = this;
            img = GetComponent<Image>();
        }
    }
}
