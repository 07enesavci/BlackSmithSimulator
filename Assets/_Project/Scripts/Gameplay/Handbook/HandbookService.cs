using UnityEngine;

namespace BlacksmithSimulator.Gameplay.Handbook
{
    public sealed class HandbookService : MonoBehaviour
    {
        [SerializeField] private HandbookPageType currentPage = HandbookPageType.Blueprints;

        public HandbookPageType CurrentPage => currentPage;

        public void OpenPage(HandbookPageType pageType)
        {
            currentPage = pageType;
        }
    }
}
