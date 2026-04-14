using UnityEngine;

namespace BlacksmithSimulator.Gameplay.Upgrades
{
    public sealed class UpgradeLevelService : MonoBehaviour
    {
        [SerializeField] private int furnaceLevel = 1;
        [SerializeField] private int bellowsLevel = 1;
        [SerializeField] private int hammerLevel = 1;
        [SerializeField] private int anvilLevel = 1;
        [SerializeField] private int sharpeningLevel = 1;

        public int GetLevel(UpgradeCategory category)
        {
            return category switch
            {
                UpgradeCategory.Furnace => furnaceLevel,
                UpgradeCategory.Bellows => bellowsLevel,
                UpgradeCategory.Hammer => hammerLevel,
                UpgradeCategory.Anvil => anvilLevel,
                UpgradeCategory.Sharpening => sharpeningLevel,
                _ => 1
            };
        }
    }
}
