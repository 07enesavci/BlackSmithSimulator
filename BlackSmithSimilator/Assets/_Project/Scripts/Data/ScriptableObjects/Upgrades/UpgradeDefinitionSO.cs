using UnityEngine;

namespace BlacksmithSimulator.Data.ScriptableObjects.Upgrades
{
    [CreateAssetMenu(menuName = "Blacksmith Simulator/Upgrades/Upgrade Definition", fileName = "UpgradeDefinition")]
    public sealed class UpgradeDefinitionSO : ScriptableObject
    {
        [field: SerializeField] public string UpgradeId { get; private set; }
        [field: SerializeField] public string Category { get; private set; }
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
    }
}
