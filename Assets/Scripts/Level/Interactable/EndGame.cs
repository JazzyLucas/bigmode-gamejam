using System.Collections;
using BigModeGameJam.Core;
using BigModeGameJam.UI;
using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    public class EndGame : Interactable
    {
        public override void Interact(GameObject interacter)
        {
            if(!canInteractMultipleTimes) return;
            StartCoroutine(EndAnimation());
            timesInteracted++;
            IEnumerator EndAnimation()
            {
                FadeEffect.StartAnimation(FadeEffect.Animation.FadeOut, Color.black, 6);
                yield return new WaitForSeconds(6);
                SceneChange.LoadNewScene("Credits");
            }
        }
    }
}
