using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using NUnit.Framework;

namespace BigModeGameJam
{
    public class AudioManager : MonoBehaviour
    {
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
    
    }
}
