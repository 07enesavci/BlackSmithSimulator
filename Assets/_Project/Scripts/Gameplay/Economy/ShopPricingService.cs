using UnityEngine;

namespace BlacksmithSimulator.Gameplay.Economy
{
    public sealed class ShopPricingService : MonoBehaviour
    {
        [SerializeField] private float qualityPriceMultiplier = 1f;

        public int CalculateBuyPrice(int basePrice, float materialQuality)
        {
            var calculated = basePrice * Mathf.Max(1f, materialQuality) * qualityPriceMultiplier;
            return Mathf.RoundToInt(calculated);
        }
    }
}
