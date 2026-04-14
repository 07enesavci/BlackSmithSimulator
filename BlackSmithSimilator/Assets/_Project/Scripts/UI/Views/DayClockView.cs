using BlacksmithSimulator.Core.Time;
using TMPro;
using UnityEngine;

namespace BlacksmithSimulator.UI.Views
{
    public sealed class DayClockView : MonoBehaviour
    {
        [SerializeField] private DayNightCycleService cycleService;
        [SerializeField] private TextMeshProUGUI clockText;

        private void Update()
        {
            if (cycleService == null || clockText == null)
            {
                return;
            }

            clockText.text = $"Time: {cycleService.CurrentHour:00.00}";
        }
    }
}
