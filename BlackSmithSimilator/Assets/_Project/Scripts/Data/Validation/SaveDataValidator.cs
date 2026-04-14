using System;
using System.Collections.Generic;
using System.Linq;
using BlacksmithSimulator.Data.RuntimeModels;
using UnityEngine;

namespace BlacksmithSimulator.Data.Validation
{
    public static class SaveDataValidator
    {
        private const int MinSaveVersion = 1;
        private const int MaxInventoryEntries = 10000;
        private const float MaxDayHour = 24f;

        private static readonly HashSet<string> ValidRanks = new(StringComparer.OrdinalIgnoreCase)
        {
            "Apprentice",
            "Journeyman",
            "Master"
        };

        public static ValidationResult Validate(GameSaveData data)
        {
            var result = new ValidationResult();

            if (data == null)
            {
                result.AddError("GameSaveData reference is null.");
                return result;
            }

            if (data.SaveVersion < MinSaveVersion)
            {
                result.AddError($"SaveVersion must be >= {MinSaveVersion} (got {data.SaveVersion}).");
            }

            if (data.DayIndex < 1)
            {
                result.AddError($"DayIndex must be >= 1 (got {data.DayIndex}).");
            }

            if (float.IsNaN(data.CurrentHour) || float.IsInfinity(data.CurrentHour))
            {
                result.AddError("CurrentHour is NaN or Infinity.");
            }
            else if (data.CurrentHour < 0f || data.CurrentHour > MaxDayHour)
            {
                result.AddWarning($"CurrentHour should be in [0, {MaxDayHour}] (got {data.CurrentHour}).");
            }

            if (data.Money < 0)
            {
                result.AddError($"Money cannot be negative (got {data.Money}).");
            }

            if (data.Xp < 0)
            {
                result.AddError($"Xp cannot be negative (got {data.Xp}).");
            }

            if (string.IsNullOrWhiteSpace(data.Rank))
            {
                result.AddWarning("Rank is empty.");
            }
            else if (!ValidRanks.Contains(data.Rank))
            {
                result.AddWarning($"Rank has unknown value: \"{data.Rank}\".");
            }

            if (float.IsNaN(data.CarriedWeightKg) || float.IsInfinity(data.CarriedWeightKg))
            {
                result.AddError("CarriedWeightKg is NaN or Infinity.");
            }
            else if (data.CarriedWeightKg < 0f)
            {
                result.AddError($"CarriedWeightKg cannot be negative (got {data.CarriedWeightKg}).");
            }

            if (data.Inventory == null)
            {
                result.AddError("Inventory list is null.");
            }
            else
            {
                if (data.Inventory.Count > MaxInventoryEntries)
                {
                    result.AddError($"Inventory exceeds max entries ({MaxInventoryEntries}).");
                }

                ValidateInventory(data.Inventory, result);
            }

            if (data.UnlockedSkillNodeIds == null)
            {
                result.AddWarning("UnlockedSkillNodeIds list is null.");
            }
            else
            {
                ValidateSkillUnlocks(data.UnlockedSkillNodeIds, result);
            }

            return result;
        }

        private static void ValidateInventory(List<InventoryEntry> inventory, ValidationResult result)
        {
            var seenPairs = new Dictionary<string, int>();
            var itemIdToUnit = new Dictionary<string, InventoryUnitType>();

            for (var i = 0; i < inventory.Count; i++)
            {
                var entry = inventory[i];
                if (entry == null)
                {
                    result.AddError($"Inventory[{i}] is null.");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(entry.ItemId))
                {
                    result.AddError($"Inventory[{i}] has empty ItemId.");
                }

                if (float.IsNaN(entry.Amount) || float.IsInfinity(entry.Amount))
                {
                    result.AddError($"Inventory[{i}] Amount is NaN or Infinity.");
                }
                else if (entry.Amount < 0f)
                {
                    result.AddError($"Inventory[{i}] Amount cannot be negative (got {entry.Amount}).");
                }

                if (!string.IsNullOrWhiteSpace(entry.ItemId))
                {
                    var key = MakeStackKey(entry.ItemId, entry.UnitType);
                    if (seenPairs.ContainsKey(key))
                    {
                        result.AddWarning($"Duplicate inventory stack for ItemId \"{entry.ItemId}\" and UnitType {entry.UnitType}.");
                    }
                    else
                    {
                        seenPairs[key] = i;
                    }

                    if (itemIdToUnit.TryGetValue(entry.ItemId, out var existingUnit))
                    {
                        if (existingUnit != entry.UnitType)
                        {
                            result.AddError(
                                $"Inventory inconsistency: ItemId \"{entry.ItemId}\" uses both {existingUnit} and {entry.UnitType}.");
                        }
                    }
                    else
                    {
                        itemIdToUnit[entry.ItemId] = entry.UnitType;
                    }
                }
            }
        }

        private static void ValidateSkillUnlocks(List<string> skillIds, ValidationResult result)
        {
            var seen = new HashSet<string>(StringComparer.Ordinal);
            for (var i = 0; i < skillIds.Count; i++)
            {
                var id = skillIds[i];
                if (string.IsNullOrWhiteSpace(id))
                {
                    result.AddWarning($"UnlockedSkillNodeIds[{i}] is empty.");
                    continue;
                }

                if (!seen.Add(id))
                {
                    result.AddWarning($"Duplicate UnlockedSkillNodeId: \"{id}\".");
                }
            }
        }

        public static void Sanitize(GameSaveData data)
        {
            if (data == null)
            {
                return;
            }

            if (data.SaveVersion < MinSaveVersion)
            {
                Debug.LogWarning($"[SaveData] SaveVersion corrected from {data.SaveVersion} to {MinSaveVersion}.");
                data.SaveVersion = MinSaveVersion;
            }

            if (data.DayIndex < 1)
            {
                Debug.LogWarning($"[SaveData] DayIndex corrected from {data.DayIndex} to 1.");
                data.DayIndex = 1;
            }

            if (float.IsNaN(data.CurrentHour) || float.IsInfinity(data.CurrentHour))
            {
                Debug.LogWarning("[SaveData] CurrentHour was invalid; set to 8.");
                data.CurrentHour = 8f;
            }
            else
            {
                data.CurrentHour = Mathf.Clamp(data.CurrentHour, 0f, MaxDayHour);
            }

            if (data.Money < 0)
            {
                Debug.LogWarning($"[SaveData] Money corrected from {data.Money} to 0.");
                data.Money = 0;
            }

            if (data.Xp < 0)
            {
                Debug.LogWarning($"[SaveData] Xp corrected from {data.Xp} to 0.");
                data.Xp = 0;
            }

            if (string.IsNullOrWhiteSpace(data.Rank) || !ValidRanks.Contains(data.Rank))
            {
                var previous = data.Rank;
                data.Rank = "Apprentice";
                Debug.LogWarning($"[SaveData] Rank corrected from \"{previous}\" to Apprentice.");
            }

            if (float.IsNaN(data.CarriedWeightKg) || float.IsInfinity(data.CarriedWeightKg))
            {
                Debug.LogWarning("[SaveData] CarriedWeightKg was invalid; set to 0.");
                data.CarriedWeightKg = 0f;
            }
            else if (data.CarriedWeightKg < 0f)
            {
                Debug.LogWarning($"[SaveData] CarriedWeightKg corrected from {data.CarriedWeightKg} to 0.");
                data.CarriedWeightKg = 0f;
            }

            if (data.Inventory == null)
            {
                data.Inventory = new List<InventoryEntry>();
            }
            else
            {
                if (data.Inventory.Count > MaxInventoryEntries)
                {
                    var excess = data.Inventory.Count - MaxInventoryEntries;
                    Debug.LogWarning($"[SaveData] Inventory truncated by {excess} entries (max {MaxInventoryEntries}).");
                    data.Inventory.RemoveRange(MaxInventoryEntries, excess);
                }

                SanitizeInventory(data.Inventory);
            }

            if (data.UnlockedSkillNodeIds == null)
            {
                data.UnlockedSkillNodeIds = new List<string>();
            }
            else
            {
                SanitizeSkillUnlocks(data.UnlockedSkillNodeIds);
            }
        }

        private static void SanitizeInventory(List<InventoryEntry> inventory)
        {
            for (var i = inventory.Count - 1; i >= 0; i--)
            {
                if (inventory[i] == null)
                {
                    Debug.LogWarning($"[SaveData] Removed null Inventory[{i}].");
                    inventory.RemoveAt(i);
                    continue;
                }

                if (string.IsNullOrWhiteSpace(inventory[i].ItemId))
                {
                    Debug.LogWarning($"[SaveData] Removed inventory row with empty ItemId at index {i}.");
                    inventory.RemoveAt(i);
                    continue;
                }

                if (float.IsNaN(inventory[i].Amount) || float.IsInfinity(inventory[i].Amount))
                {
                    Debug.LogWarning($"[SaveData] Inventory amount at ItemId \"{inventory[i].ItemId}\" was invalid; set to 0.");
                    inventory[i].Amount = 0f;
                }
                else if (inventory[i].Amount < 0f)
                {
                    Debug.LogWarning($"[SaveData] Inventory amount for \"{inventory[i].ItemId}\" corrected to 0.");
                    inventory[i].Amount = 0f;
                }
            }

            ResolveItemIdUnitConflicts(inventory);
            MergeDuplicateStacks(inventory);
            inventory.RemoveAll(e => e != null && e.Amount <= 0f);
        }

        private static void ResolveItemIdUnitConflicts(List<InventoryEntry> inventory)
        {
            var firstUnit = new Dictionary<string, InventoryUnitType>(StringComparer.Ordinal);
            var removeIndices = new List<int>();
            for (var i = 0; i < inventory.Count; i++)
            {
                var entry = inventory[i];
                if (entry == null || string.IsNullOrWhiteSpace(entry.ItemId))
                {
                    continue;
                }

                if (!firstUnit.TryGetValue(entry.ItemId, out var unit))
                {
                    firstUnit[entry.ItemId] = entry.UnitType;
                }
                else if (unit != entry.UnitType)
                {
                    Debug.LogWarning(
                        $"[SaveData] Dropped conflicting inventory row for \"{entry.ItemId}\" (UnitType {entry.UnitType} vs {unit}).");
                    removeIndices.Add(i);
                }
            }

            for (var j = removeIndices.Count - 1; j >= 0; j--)
            {
                inventory.RemoveAt(removeIndices[j]);
            }
        }

        private static void MergeDuplicateStacks(List<InventoryEntry> inventory)
        {
            var merged = new Dictionary<string, InventoryEntry>(StringComparer.Ordinal);
            var order = new List<string>();

            foreach (var entry in inventory)
            {
                if (entry == null || string.IsNullOrWhiteSpace(entry.ItemId))
                {
                    continue;
                }

                var key = MakeStackKey(entry.ItemId, entry.UnitType);
                if (merged.TryGetValue(key, out var existing))
                {
                    existing.Amount += entry.Amount;
                    Debug.LogWarning($"[SaveData] Merged duplicate stack for \"{entry.ItemId}\" ({entry.UnitType}).");
                }
                else
                {
                    merged[key] = entry;
                    order.Add(key);
                }
            }

            inventory.Clear();
            foreach (var key in order)
            {
                inventory.Add(merged[key]);
            }
        }

        private static void SanitizeSkillUnlocks(List<string> skillIds)
        {
            for (var i = skillIds.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrWhiteSpace(skillIds[i]))
                {
                    Debug.LogWarning($"[SaveData] Removed empty UnlockedSkillNodeIds[{i}].");
                    skillIds.RemoveAt(i);
                }
            }

            var seen = new HashSet<string>(StringComparer.Ordinal);
            for (var i = skillIds.Count - 1; i >= 0; i--)
            {
                var id = skillIds[i];
                if (!seen.Add(id))
                {
                    Debug.LogWarning($"[SaveData] Removed duplicate skill id \"{id}\".");
                    skillIds.RemoveAt(i);
                }
            }
        }

        private static string MakeStackKey(string itemId, InventoryUnitType unitType)
        {
            return itemId + "\u001f" + unitType;
        }
    }
}
