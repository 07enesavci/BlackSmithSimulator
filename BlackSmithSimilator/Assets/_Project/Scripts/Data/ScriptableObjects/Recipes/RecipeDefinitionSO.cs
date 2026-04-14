using BlacksmithSimulator.Data.ScriptableObjects.Items;
using UnityEngine;

namespace BlacksmithSimulator.Data.ScriptableObjects.Recipes
{
    [CreateAssetMenu(menuName = "Blacksmith Simulator/Recipes/Recipe Definition", fileName = "RecipeDefinition")]
    public sealed class RecipeDefinitionSO : ScriptableObject
    {
        [field: SerializeField] public string RecipeId { get; private set; }
        [field: SerializeField] public CraftingTier RequiredTier { get; private set; }
        [field: SerializeField] public RecipeIngredient[] Ingredients { get; private set; }
        [field: SerializeField] public string OutputItemId { get; private set; }
        [field: SerializeField] public float OutputAmount { get; private set; } = 1f;
        [field: SerializeField] public float ForgeDurationSeconds { get; private set; }
        [field: SerializeField] public float MeltDurationSeconds { get; private set; }
        [field: SerializeField] public bool IsRestoration { get; private set; }

        // Malzeme girdileri ItemId ile item veritabanindaki kayitlara baglanir.
    }
}
