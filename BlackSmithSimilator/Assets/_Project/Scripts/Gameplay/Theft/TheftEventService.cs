using UnityEngine;

namespace BlacksmithSimulator.Gameplay.Theft
{
    public sealed class TheftEventService : MonoBehaviour
    {
        [SerializeField] private int maxAttemptsPerDay = 2;
        [SerializeField] private int attemptsToday;

        public bool CanTriggerTheft => attemptsToday < maxAttemptsPerDay;

        public bool TryConsumeAttempt()
        {
            if (!CanTriggerTheft)
            {
                return false;
            }

            attemptsToday++;
            return true;
        }

        public void ResetForNewDay()
        {
            attemptsToday = 0;
        }
    }
}
