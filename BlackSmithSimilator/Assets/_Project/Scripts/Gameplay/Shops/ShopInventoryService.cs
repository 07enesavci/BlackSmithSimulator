using UnityEngine;

namespace BlacksmithSimulator.Gameplay.Shops
{
    public sealed class ShopInventoryService : MonoBehaviour
    {
        [SerializeField] private ShopType shopType;

        public ShopType ShopType => shopType;
    }
}
