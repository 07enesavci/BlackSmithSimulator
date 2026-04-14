using UnityEngine;

namespace BlacksmithSimulator.Data.ScriptableObjects.Alloys
{
    [CreateAssetMenu(menuName = "Blacksmith Simulator/Alloys/Alloy Note Definition", fileName = "AlloyNoteDefinition")]
    public sealed class AlloyNoteDefinitionSO : ScriptableObject
    {
        [field: SerializeField] public string NoteId { get; private set; }
        [field: SerializeField] public int UnlockXp { get; private set; }
        [field: SerializeField] public string FormulaText { get; private set; }
    }
}
