using UnityEngine;

namespace BlacksmithSimulator.Data.ScriptableObjects.Items
{
    [CreateAssetMenu(menuName = "Blacksmith Simulator/Items/Item Definition", fileName = "ItemDefinition")]
    public sealed class ItemDefinitionSO : ScriptableObject
    {
        [field: SerializeField] public string ItemId { get; private set; }
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField] public float WeightKg { get; private set; }
        [field: SerializeField] public int BaseBuyPrice { get; private set; }
        [field: SerializeField] public int BaseSellPrice { get; private set; }
    }
}
