using UnityEngine;

namespace BlacksmithSimulator.Gameplay.Progression
{
    public sealed class ProgressionService : MonoBehaviour
    {
        [SerializeField] private int xp;

        public int Xp => xp;

        public string Rank
        {
            get
            {
                if (xp < 5000) return "Apprentice";
                if (xp <= 10000) return "Journeyman";
                return "Master";
            }
        }

        public void AddXp(int amount)
        {
            xp = Mathf.Max(0, xp + amount);
        }
    }
}
