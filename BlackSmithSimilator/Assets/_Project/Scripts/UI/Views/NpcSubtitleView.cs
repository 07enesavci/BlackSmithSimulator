using TMPro;
using UnityEngine;

namespace BlacksmithSimulator.UI.Views
{
    public sealed class NpcSubtitleView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI subtitleText;

        public void ShowSubtitle(string line)
        {
            if (subtitleText != null)
            {
                subtitleText.text = line;
            }
        }
    }
}
