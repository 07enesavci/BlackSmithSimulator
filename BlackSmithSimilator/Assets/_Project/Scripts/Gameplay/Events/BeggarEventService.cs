using UnityEngine;

namespace BlacksmithSimulator.Gameplay.Events
{
    public sealed class BeggarEventService : MonoBehaviour
    {
        [SerializeField] private bool isBeggarAtDoor;

        public bool IsBeggarAtDoor => isBeggarAtDoor;

        public void SpawnBeggar()
        {
            isBeggarAtDoor = true;
        }

        public void ResolveBeggar()
        {
            isBeggarAtDoor = false;
        }
    }
}
