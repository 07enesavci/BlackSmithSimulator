using System;
using UnityEngine;

namespace BlacksmithSimulator.Core.Time
{
    public class DayNightCycleService : MonoBehaviour
    {
        [Header("Zaman Ayarları")]
        [Tooltip("Gerçek hayatta kaç saniye = oyun içi 1 saat")]
        [SerializeField] private float _realSecondsPerHour = 10f;
        [Tooltip("Günün başladığı saat (0-23)")]
        [SerializeField] private int _startHour = 8;
        [Tooltip("Dükkan kapanış saati. 24=00:00 (ertesi gün), 26=02:00 (ertesi gün) gibi değerler verilebilir.")]
        [SerializeField] private int _shopCloseHour = 24;
        [Tooltip("Oyuncu uyumazsa bayılacağı saat. 26 gibi 24'ten büyük değerler verilebilir.")]
        [SerializeField] private int _faintHour = 26;
        // EVENTS
        public event Action<int, int> OnTimeChanged;
        public event Action OnShopClosed;
        public event Action OnPlayerPassedOut;
        public event Action OnDayStarted;
        // Timeline: gün, _startHour'dan başlar ve 24'ü aşabilir (örn 26).
        // UI saatini göstermek için %24 uygulanır.
        public int CurrentHour => Mod24(Mathf.FloorToInt(_timelineHour));
        public int CurrentMinute
        {
            get
            {
                float hourFloor = Mathf.Floor(_timelineHour);
                return Mathf.FloorToInt((_timelineHour - hourFloor) * 60f);
            }
        }
        private float _timelineHour;
        private float _shopCloseTimelineHour;
        private float _faintTimelineHour;
        private bool _dayActive;
        private bool _shopClosedTriggered;
        private bool _passOutTriggered;
        private int _lastBroadcastedMinute = -1;
        private void Start() => StartNewDay();
        private void Update()
        {
            if (!_dayActive) return;
            _timelineHour += UnityEngine.Time.deltaTime / _realSecondsPerHour;
            BroadcastTimeIfMinuteChanged();
            if (!_shopClosedTriggered && _timelineHour >= _shopCloseTimelineHour)
                TriggerShopClosure();
            if (!_passOutTriggered && _timelineHour >= _faintTimelineHour)
                TriggerPassOut();
        }
        public void StartNewDay()
        {
            BuildTimelineThresholds();
            _timelineHour = _startHour;
            _dayActive = true;
            _shopClosedTriggered = false;
            _passOutTriggered = false;
            _lastBroadcastedMinute = -1;
            OnDayStarted?.Invoke();
            BroadcastTimeIfMinuteChanged(force: true);
            Debug.Log($"=== YENİ GÜN BAŞLADI: {CurrentHour:D2}:{CurrentMinute:D2} ===");
        }
        public void Sleep()
        {
            if (!_dayActive) return;
            _dayActive = false;
            Debug.Log("Oyuncu uyudu. Gün başarıyla tamamlandı.");
            StartNewDay(); //Yeni gün ilerleyen zamanlarda gün özeti eklenebilir, şimdilik hemen yeni gün başlatılıyor.
        }
        private void BroadcastTimeIfMinuteChanged(bool force = false)
        {
            int hour = CurrentHour;
            int minute = CurrentMinute;
            if (!force && minute == _lastBroadcastedMinute) return;
            _lastBroadcastedMinute = minute;
            OnTimeChanged?.Invoke(hour, minute);
            //Debug.Log($"[ZAMAN] {hour:D2}:{minute:D2}");
        }
        private void TriggerShopClosure()
        {
            _shopClosedTriggered = true;
            OnShopClosed?.Invoke();
            Debug.Log("=== DÜKKAN KAPANDI! EVE GİTME ZAMANI ===");
        }
        private void TriggerPassOut()
        {
            _passOutTriggered = true;
            _dayActive = false;
            OnPlayerPassedOut?.Invoke();
            Debug.LogError("!!! OYUNCU YORGUNLUKTAN BAYILDI !!!");
            StartNewDay();
        }
        private void BuildTimelineThresholds()
        {
            // 0-23 girilirse startHour'a göre "aynı gün mü ertesi gün mü" karar verir.
            _shopCloseTimelineHour = ToTimelineHour(_shopCloseHour);
            _faintTimelineHour = ToTimelineHour(_faintHour);
            // Mantıksal güvenlik: faint, shop close'dan önce olmasın (tasarım gereği).
            if (_faintTimelineHour <= _shopCloseTimelineHour)
                _faintTimelineHour = _shopCloseTimelineHour + 2f; // varsayılan 2 saat sonra bayılma
        }
        private float ToTimelineHour(int hour)
        {
            if (hour >= 24) return hour;
            // Örn start=8, close=2 verilirse => ertesi gün 26 gibi düşün.
            if (hour < _startHour) return hour + 24;
            return hour;
        }
        private static int Mod24(int hour)
        {
            int r = hour % 24;
            return r < 0 ? r + 24 : r;
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            _realSecondsPerHour = Mathf.Max(0.01f, _realSecondsPerHour);
            _startHour = Mathf.Clamp(_startHour, 0, 23);
            _shopCloseHour = Mathf.Clamp(_shopCloseHour, 0, 48);
            _faintHour = Mathf.Clamp(_faintHour, 0, 48);
        }
#endif
    }
}