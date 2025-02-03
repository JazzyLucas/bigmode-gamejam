using BigModeGameJam.Core.Manager;
using UnityEngine;

namespace BigModeGameJam.Core
{
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    public class Fatten : MonoBehaviour
    {
        private const int MAX_FOOD = 7;
        private SkinnedMeshRenderer smr;

        private void Awake()
        {
            int foodsEaten = GameManager.GameData.foodsEaten;
            float ratio = (float) foodsEaten / MAX_FOOD;
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, ratio * 100);
        }
    }
}
