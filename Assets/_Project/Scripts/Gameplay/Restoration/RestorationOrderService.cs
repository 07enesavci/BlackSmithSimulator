using UnityEngine;

namespace BlacksmithSimulator.Gameplay.Restoration
{
    public sealed class RestorationOrderService : MonoBehaviour
    {
        [Range(0f, 100f)]
        [SerializeField] private float durabilityPercent = 25f;

        public float DurabilityPercent => durabilityPercent;

        public void ImproveDurability(float amount)
        {
            durabilityPercent = Mathf.Clamp(durabilityPercent + amount, 0f, 100f);
        }
    }
}
