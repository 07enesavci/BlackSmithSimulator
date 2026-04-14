using UnityEngine;

namespace BlacksmithSimulator.Data.ScriptableObjects.Recipes
{
    [CreateAssetMenu(menuName = "Blacksmith Simulator/Recipes/Recipe Definition", fileName = "RecipeDefinition")]
    public sealed class RecipeDefinitionSO : ScriptableObject
    {
        [field: SerializeField] public string RecipeId { get; private set; }
        [field: SerializeField] public string OutputItemId { get; private set; }
        [field: SerializeField] public string Tier { get; private set; }
        [field: SerializeField] public float ForgeDurationSeconds { get; private set; }
        [field: SerializeField] public float MeltDurationSeconds { get; private set; }
    }
}
