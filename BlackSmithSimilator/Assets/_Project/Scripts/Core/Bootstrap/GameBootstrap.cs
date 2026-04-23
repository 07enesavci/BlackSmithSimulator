using BlacksmithSimulator.Core.Time;
using BlacksmithSimulator.Infrastructure.Serialization;
using UnityEngine;

namespace BlacksmithSimulator.Core.Bootstrap
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        [Header("Display")]
        [SerializeField] private int _vSyncCount = 1; // 0=off, 1=every vblank, 2=every 2nd vblank
        [Header("Cursor (Gameplay)")]
        [SerializeField] private bool _lockCursorOnStart = true;
        [SerializeField] private bool _hideCursorOnStart = true;
        private void Awake()
        {
            QualitySettings.vSyncCount = _vSyncCount;
            if (_lockCursorOnStart)
                Cursor.lockState = CursorLockMode.Locked;
            if (_hideCursorOnStart)
                Cursor.visible = false;
        }
    }
}
