using System.Collections;
using BigModeGameJam.Core;
using BigModeGameJam.UI;
using UnityEngine;

namespace BigModeGameJam.Level.Interactables
{
    public class EndGame : Interactable
    {
        [SerializeField] private GameObject endScreen;
        public override void Interact(GameObject interacter)
        {
            if(!canInteractMultipleTimes && timesInteracted > 0) return;
            StartCoroutine(EndAnimation());
            timesInteracted++;
            IEnumerator EndAnimation()
            {
                PlayerTransitioner.instance.music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.LevelComplete, this.transform.position);
                FadeEffect.StartAnimation(FadeEffect.Animation.FadeOut, Color.black, 6);
                yield return new WaitForSeconds(6);
                FadeEffect.StartAnimation(FadeEffect.Animation.FadeIn, Color.black, 2);
                endScreen.SetActive(true);
            }
            
        }
    }
}
