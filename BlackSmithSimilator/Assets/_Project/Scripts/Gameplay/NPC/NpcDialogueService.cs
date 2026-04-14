using UnityEngine;

namespace BlacksmithSimulator.Gameplay.NPC
{
    public sealed class NpcDialogueService : MonoBehaviour
    {
        [TextArea]
        [SerializeField] private string[] randomLines;

        public string GetRandomLine()
        {
            if (randomLines == null || randomLines.Length == 0)
            {
                return "...";
            }

            return randomLines[Random.Range(0, randomLines.Length)];
        }
    }
}
