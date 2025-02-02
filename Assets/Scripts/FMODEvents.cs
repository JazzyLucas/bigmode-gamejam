using UnityEngine;
using FMODUnity;

namespace BigModeGameJam.Core
{
    public class FMODEvents : MonoBehaviour
    {

        [field: Header("FootstepNormal")]
        [field: SerializeField] public EventReference FootstepNormal {  get; private set; }

        [field: Header("JumpSound")]
        [field: SerializeField] public EventReference JumpSound { get; private set; }

        [field: Header("FootstepElectric")]
        [field: SerializeField] public EventReference FootstepElectric { get; private set; }

        [field: Header("Item")]
        [field: SerializeField] public EventReference Item { get; private set; }

        [field: Header("SpecialItem")]
        [field: SerializeField] public EventReference SpecialItem { get; private set; }

        [field: Header("Boom")]
        [field: SerializeField] public EventReference Boom { get; private set; }

        [field: Header("LevelTwo")]
        [field: SerializeField] public EventReference LevelTwo { get; private set; }

        [field: Header("Title")]
        [field: SerializeField] public EventReference Title { get; private set; }

        [field: Header("LevelComplete")]
        [field: SerializeField] public EventReference LevelComplete { get; private set; }

        [field: Header("Test")]
        [field: SerializeField] public EventReference Test { get; private set; }

        [field: Header("Death")]
        [field: SerializeField] public EventReference Death { get; private set; }

        [field: Header("Food")]
        [field: SerializeField] public EventReference Food { get; private set; }

        [field: Header("Money")]
        [field: SerializeField] public EventReference Money { get; private set; }

        [field: Header("ElectricMove")]
        [field: SerializeField] public EventReference ElectricMove { get; private set; }

        [field: Header("ElectricDeath")]
        [field: SerializeField] public EventReference ElectricDeath { get; private set; }
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
