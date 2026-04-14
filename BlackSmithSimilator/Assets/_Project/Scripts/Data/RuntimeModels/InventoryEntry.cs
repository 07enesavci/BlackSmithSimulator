using System;

namespace BlacksmithSimulator.Data.RuntimeModels
{
    [Serializable]
    public sealed class InventoryEntry
    {
        public string ItemId;
        public float Amount;
        public InventoryUnitType UnitType;
    }
}
