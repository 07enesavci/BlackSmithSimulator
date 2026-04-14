using UnityEngine;

namespace BlacksmithSimulator.Core.Time
{
    public sealed class DayNightCycleService : MonoBehaviour
    {
        [SerializeField] private float currentHour = 8f;
        [SerializeField] private float hourSpeed = 0.25f;

        private float customerCutoffHour = 21f;
        private float forgingCutoffHour = 24f;
        private bool running;

        public float CurrentHour => currentHour;
        public bool CanReceiveCustomers => currentHour < customerCutoffHour;
        public bool CanForge => currentHour < forgingCutoffHour;

        public void StartDay(float startHour, float customerCutoff, float forgeCutoff)
        {
            currentHour = startHour;
            customerCutoffHour = customerCutoff;
            forgingCutoffHour = forgeCutoff;
            running = true;
        }

        private void Update()
        {
            if (!running)
            {
                return;
            }

            currentHour += Time.deltaTime * hourSpeed;
            if (currentHour >= 24f)
            {
                currentHour = 24f;
            }
        }
    }
}
