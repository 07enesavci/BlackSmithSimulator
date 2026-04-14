using BlacksmithSimulator.Data.ScriptableObjects.Items;
using BlacksmithSimulator.Data.ScriptableObjects.Recipes;
using BlacksmithSimulator.Data.Validation;
using UnityEngine;

namespace BlacksmithSimulator.Data.ScriptableObjects.Database
{
    [CreateAssetMenu(menuName = "Blacksmith Simulator/Database/Game Database", fileName = "GameDatabase")]
    public sealed class GameDatabaseSO : ScriptableObject
    {
        [field: SerializeField] public ItemDefinitionSO[] Items { get; private set; }
        [field: SerializeField] public RecipeDefinitionSO[] Recipes { get; private set; }

        // Tum item ve tarif verilerine tek merkezden erisilen ScriptableObject veritabani.

#if UNITY_EDITOR
        private void OnValidate()
        {
            var validation = GameDatabaseValidator.Validate(this);
            foreach (var issue in validation.Issues)
            {
                var text = "[GameDatabase] " + issue.Message;
                if (issue.Severity == ValidationSeverity.Error)
                {
                    Debug.LogError(text, this);
                }
                else
                {
                    Debug.LogWarning(text, this);
                }
            }
        }
#endif
    }
}
