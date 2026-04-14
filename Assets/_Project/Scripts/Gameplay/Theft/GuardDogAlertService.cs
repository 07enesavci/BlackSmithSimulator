using UnityEngine;

namespace BlacksmithSimulator.Gameplay.Theft
{
    public sealed class GuardDogAlertService : MonoBehaviour
    {
        [SerializeField] private bool isUnlocked;

        public bool IsUnlocked => isUnlocked;

        public void Unlock()
        {
            isUnlocked = true;
        }

        public bool CanWarnPlayer()
        {
            return isUnlocked;
        }
    }
}
