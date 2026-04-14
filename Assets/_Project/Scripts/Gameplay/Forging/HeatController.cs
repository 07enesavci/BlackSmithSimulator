using UnityEngine;

namespace BlacksmithSimulator.Gameplay.Forging
{
    public sealed class HeatController : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float heatNormalized = 0.25f;

        public float HeatNormalized => heatNormalized;
        public bool IsOptimal => heatNormalized >= 0.45f && heatNormalized <= 0.65f;

        public void AddHeat(float amount)
        {
            heatNormalized = Mathf.Clamp01(heatNormalized + amount);
        }

        public void CoolDown(float amount)
        {
            heatNormalized = Mathf.Clamp01(heatNormalized - amount);
        }
    }
}
