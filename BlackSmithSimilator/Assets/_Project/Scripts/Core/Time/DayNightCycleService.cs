using System;
using UnityEngine;

namespace BlacksmithSimulator.Core.Time
{
    public class DayNightCycleService : MonoBehaviour
    {
        [Header("Zaman Ayarları")]
        [SerializeField] private float _realSecondsPerHour = 10f; 
        [SerializeField] private int _startHour = 8;     
        [SerializeField] private int _shopCloseHour = 24; 
        [SerializeField] private int _faintHour = 26;     

        // EVENTS (Olaylar)
        public event Action<int, int> OnTimeChanged;
        public event Action OnShopClosed;
        public event Action OnPlayerPassedOut;
        public event Action OnDayStarted;

        public int CurrentHour => Mathf.FloorToInt(_currentTime) % 24;
        public int CurrentMinute => Mathf.FloorToInt((_currentTime - Mathf.FloorToInt(_currentTime)) * 60f);

        private float _currentTime;
        private bool _isDayActive;
        private int _lastBroadcastedMinute = -1;
        private bool _isShopClosedTriggered;

        private void Start() => StartNewDay();

        private void Update()
        {
            if (!_isDayActive) return;

            _currentTime += UnityEngine.Time.deltaTime / _realSecondsPerHour;

            // Mevcut zamanı Property'lerden (Yukarıdan) okuyoruz
            int currentHourInt = CurrentHour;
            int currentMinuteInt = CurrentMinute;

            // Dakika değişti mi?
            if (currentMinuteInt != _lastBroadcastedMinute)
            {
                _lastBroadcastedMinute = currentMinuteInt;
                OnTimeChanged?.Invoke(currentHourInt, currentMinuteInt);
                Debug.Log($"[ZAMAN] {currentHourInt % 24:D2}:{currentMinuteInt:D2}");
            }

            // Dükkan Kapanış Kontrolü
            if (_currentTime >= _shopCloseHour && !_isShopClosedTriggered)
            {
                TriggerShopClosure();
            }

            // Bayılma Kontrolü
            if (_currentTime >= _faintHour)
            {
                TriggerPassOut();
            }
        }

        public void StartNewDay()
        {
            _currentTime = _startHour;
            _isDayActive = true;
            _isShopClosedTriggered = false;
            
            _lastBroadcastedMinute = 0; 
            OnTimeChanged?.Invoke(CurrentHour, CurrentMinute);
            
            OnDayStarted?.Invoke();
            Debug.Log("=== YENİ GÜN BAŞLADI: 08:00 ===");
        }

        private void TriggerShopClosure()
        {
            _isShopClosedTriggered = true;
            OnShopClosed?.Invoke();
            Debug.Log("=== DÜKKAN KAPANDI! EVE GİTME ZAMANI ===");
        }

        private void TriggerPassOut()
        {
            _isDayActive = false;
            _currentTime = _faintHour;
            
            OnPlayerPassedOut?.Invoke();
            Debug.LogError("!!! OYUNCU YORGUNLUKTAN BAYILDI !!!");
        }

        public void Sleep()
        {
            if (!_isDayActive) return;
            
            _isDayActive = false;
            Debug.Log("Oyuncu uyudu. Gün başarıyla tamamlandı.");
        }
    }
}