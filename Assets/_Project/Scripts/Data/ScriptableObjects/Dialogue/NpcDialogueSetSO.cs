using UnityEngine;

namespace BlacksmithSimulator.Data.ScriptableObjects.Dialogue
{
    [CreateAssetMenu(menuName = "Blacksmith Simulator/Dialogue/NPC Dialogue Set", fileName = "NpcDialogueSet")]
    public sealed class NpcDialogueSetSO : ScriptableObject
    {
        [field: SerializeField] public string NpcId { get; private set; }
        [field: SerializeField] public string[] RandomTalkLines { get; private set; }
        [field: SerializeField] public string[] RequestLines { get; private set; }
    }
}
