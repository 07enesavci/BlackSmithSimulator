using UnityEngine;

namespace BlacksmithSimulator.Data.ScriptableObjects.Progression
{
    [CreateAssetMenu(menuName = "Blacksmith Simulator/Progression/Skill Node Definition", fileName = "SkillNodeDefinition")]
    public sealed class SkillNodeDefinitionSO : ScriptableObject
    {
        [field: SerializeField] public string NodeId { get; private set; }
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField] public int RequiredXp { get; private set; }
        [field: SerializeField] public string RequiredRank { get; private set; }
    }
}
