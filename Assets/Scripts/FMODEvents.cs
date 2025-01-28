using UnityEngine;
using FMODUnity;

namespace BigModeGameJam.Core
{
    public class FMODEvents : MonoBehaviour
    {

        [field: Header("FootstepNormal")]

        [field: SerializeField] public EventReference FootstepNormal {  get; private set; }
        public static FMODEvents instance { get; private set; }

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Found more than one FMOD Events instance in the scene.");
            }
            instance = this;
        }
    }
}
