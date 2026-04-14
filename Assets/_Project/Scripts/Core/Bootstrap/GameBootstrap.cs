using BlacksmithSimulator.Core.Time;
using BlacksmithSimulator.Infrastructure.Serialization;
using UnityEngine;

namespace BlacksmithSimulator.Core.Bootstrap
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private DayNightCycleService dayNightCycleService;
        [SerializeField] private SaveLoadService saveLoadService;

        private void Awake()
        {
            if (saveLoadService != null)
            {
                saveLoadService.LoadOrCreate();
            }
        }

        private void Start()
        {
            if (dayNightCycleService != null)
            {
                dayNightCycleService.StartDay(8f, 21f, 24f);
            }
        }
    }
}
