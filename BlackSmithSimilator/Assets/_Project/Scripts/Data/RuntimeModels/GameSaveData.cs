using System;
using System.Collections.Generic;

namespace BlacksmithSimulator.Data.RuntimeModels
{
    [Serializable]
    public sealed class GameSaveData
    {
        public int SaveVersion = 1;
        public int DayIndex = 1;
        public float CurrentHour = 8f;
        public int Money = 100;
        public int Xp = 0;
        public string Rank = "Apprentice";
        public float CarriedWeightKg = 0f;
        public List<InventoryEntry> Inventory = new();
        public List<string> UnlockedSkillNodeIds = new();

        // Kayit semasi degisince migration icin surum takibi.
    }
}
