using UnityEngine;

namespace BlacksmithSimulator.Gameplay.Player
{
    public sealed class CarryWeightMovementService : MonoBehaviour
    {
        [SerializeField] private float baseSpeed = 5f;
        [SerializeField] private float maxCarryWeightKg = 60f;
        [SerializeField] private float currentWeightKg;

        public float CurrentSpeed
        {
            get
            {
                var ratio = Mathf.Clamp01(currentWeightKg / maxCarryWeightKg);
                return Mathf.Lerp(baseSpeed, baseSpeed * 0.4f, ratio);
            }
        }

        public void SetCurrentWeight(float weightKg)
        {
            currentWeightKg = Mathf.Max(0f, weightKg);
        }
    }
}
