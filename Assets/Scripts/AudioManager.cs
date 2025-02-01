using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using NUnit.Framework;
using BigModeGameJam.Core.Manager;
using BigModeGameJam.Core;

namespace BigModeGameJam
{
    public class AudioManager : MonoBehaviour, IPersistentOBJ
    {
        [Header("Volume")]
        [UnityEngine.Range(0, 1)]
        public float masterVolume = 1;
        [UnityEngine.Range(0, 1)]
        public float musicVolume = 1;
        [UnityEngine.Range(0, 1)]
        public float SFXVolume = 1;
        [UnityEngine.Range(0, 1)]

        private Bus masterBus;
        private Bus musicBus;
        private Bus sfxBus;

        private System.Collections.Generic.List<EventInstance> eventInstances;
        private System.Collections.Generic.List<StudioEventEmitter> eventEmitters;

        public static AudioManager instance { get; private set; }

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Found more than one Audio Manager in the scene.");
            }
            instance = this;

            eventInstances = new System.Collections.Generic.List<EventInstance>();
            eventEmitters = new System.Collections.Generic.List<StudioEventEmitter>();

            masterBus = RuntimeManager.GetBus("bus:/");
            musicBus = RuntimeManager.GetBus("bus:/Music");
            sfxBus = RuntimeManager.GetBus("bus:/SFX");
        }

        private void Update()
        {
            masterBus.setVolume(masterVolume);
            GameManager.GameData.masterVolume = masterVolume;
            musicBus.setVolume(musicVolume);
            GameManager.GameData.musicVolume = musicVolume;
            sfxBus.setVolume(SFXVolume);
            GameManager.GameData.SFXVolume = SFXVolume;
        }

        public void PlayOneShot(EventReference sound, Vector3 position)
        {
            RuntimeManager.PlayOneShot(sound);
        }
        public EventInstance CreateEventInstance(EventReference eventReference)
        {
            EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
            return eventInstance;
        }

        public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
        {
            StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
            emitter.EventReference = eventReference;
            eventEmitters.Add(emitter);
            return emitter;
        }

        private void CleanUp()
        {
            //stop and release any created instances
            foreach (EventInstance eventInstance in eventInstances)
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                eventInstance.release();
            }
            //stop all of the event emitters, because if we don't they may hang around in other scenes
            foreach (StudioEventEmitter emitter in eventEmitters)
            {
                emitter.Stop();
            }
        }

        // PERSISTENCE STUFF
        public void LoadData(GameData data)
        {
            masterVolume = data.masterVolume;
            musicVolume = data.musicVolume;
            SFXVolume = data.SFXVolume;
        }

        // ignore these. they're just in the interface
        public string UID { get; }
        public int MoneyValue { get; }
        
    }
}
    