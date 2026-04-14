using System;
using System.Collections.Generic;

namespace BlacksmithSimulator.Data.RuntimeModels
{
    [Serializable]
    public sealed class GameSaveData
    {
        public int DayIndex = 1;
        public float CurrentHour = 8f;
        public int Money = 100;
        public int Xp = 0;
        public string Rank = "Apprentice";
        public List<InventoryEntry> Inventory = new();
        public List<string> UnlockedSkillNodeIds = new();
    }
}
