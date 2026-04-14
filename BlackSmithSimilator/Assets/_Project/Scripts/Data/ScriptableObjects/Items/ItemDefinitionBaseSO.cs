using UnityEngine;

namespace BlacksmithSimulator.Data.ScriptableObjects.Items
{
    public abstract class ItemDefinitionBaseSO : ScriptableObject
    {
        [field: Header("Identity")]
        [field: SerializeField] public string ItemId { get; private set; }
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: TextArea]
        [field: SerializeField] public string Description { get; private set; }

        [field: Header("Classification")]
        [field: SerializeField] public ItemCategory Category { get; private set; }
        [field: SerializeField] public CraftingTier RequiredTier { get; private set; }
        [field: SerializeField] public MerchantType MerchantSource { get; private set; }
        [field: SerializeField] public ItemUnitType UnitType { get; private set; }

        [field: Header("Economy")]
        [field: SerializeField] public int BaseBuyPrice { get; private set; }
        [field: SerializeField] public int BaseSellPrice { get; private set; }

        [field: Header("Physical")]
        [field: SerializeField] public float WeightPerUnitKg { get; private set; }
        [field: SerializeField] public float MeltDurationMultiplier { get; private set; } = 1f;
        [field: SerializeField] public float ForgeDurationMultiplier { get; private set; } = 1f;
        [field: SerializeField] public float DurabilityBase { get; private set; } = 100f;

        // Tum item turleri icin ortak ana taslak; turetilen SO'lar bu alanlari kullanir.
    }
}
