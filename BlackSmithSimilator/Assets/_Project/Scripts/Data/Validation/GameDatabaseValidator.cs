using System.Collections.Generic;
using BlacksmithSimulator.Data.ScriptableObjects.Database;
using BlacksmithSimulator.Data.ScriptableObjects.Items;
using BlacksmithSimulator.Data.ScriptableObjects.Recipes;

namespace BlacksmithSimulator.Data.Validation
{
    public static class GameDatabaseValidator
    {
        public static ValidationResult Validate(GameDatabaseSO database)
        {
            var result = new ValidationResult();

            if (database == null)
            {
                result.AddError("GameDatabaseSO reference is null.");
                return result;
            }

            var itemIds = ValidateItems(database.Items, result);
            ValidateRecipes(database.Recipes, itemIds, result);

            return result;
        }

        private static HashSet<string> ValidateItems(ItemDefinitionSO[] items, ValidationResult result)
        {
            var ids = new HashSet<string>();

            if (items == null || items.Length == 0)
            {
                result.AddWarning("Items array is null or empty.");
                return ids;
            }

            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                if (item == null)
                {
                    result.AddError($"Items[{i}] is null.");
                    continue;
                }

                var id = item.ItemId;
                if (string.IsNullOrWhiteSpace(id))
                {
                    result.AddError($"Items[{i}] has empty ItemId.");
                    continue;
                }

                if (!ids.Add(id))
                {
                    result.AddError($"Duplicate ItemId: \"{id}\".");
                }

                if (string.IsNullOrWhiteSpace(item.DisplayName))
                {
                    result.AddWarning($"Item \"{id}\" has empty DisplayName.");
                }

                if (item.WeightPerUnitKg < 0f)
                {
                    result.AddWarning($"Item \"{id}\" has negative WeightPerUnitKg.");
                }
            }

            return ids;
        }

        private static void ValidateRecipes(
            RecipeDefinitionSO[] recipes,
            HashSet<string> itemIds,
            ValidationResult result)
        {
            var recipeIds = new HashSet<string>();

            if (recipes == null || recipes.Length == 0)
            {
                result.AddWarning("Recipes array is null or empty.");
                return;
            }

            for (var i = 0; i < recipes.Length; i++)
            {
                var recipe = recipes[i];
                if (recipe == null)
                {
                    result.AddError($"Recipes[{i}] is null.");
                    continue;
                }

                var rid = recipe.RecipeId;
                if (string.IsNullOrWhiteSpace(rid))
                {
                    result.AddError($"Recipes[{i}] has empty RecipeId.");
                    continue;
                }

                if (!recipeIds.Add(rid))
                {
                    result.AddError($"Duplicate RecipeId: \"{rid}\".");
                }

                var outputId = recipe.OutputItemId;
                if (string.IsNullOrWhiteSpace(outputId))
                {
                    result.AddError($"Recipe \"{rid}\" has empty OutputItemId.");
                }
                else if (itemIds.Count > 0 && !itemIds.Contains(outputId))
                {
                    result.AddError($"Recipe \"{rid}\" references unknown OutputItemId: \"{outputId}\".");
                }

                if (recipe.OutputAmount <= 0f)
                {
                    result.AddError($"Recipe \"{rid}\" OutputAmount must be greater than zero.");
                }

                if (recipe.ForgeDurationSeconds < 0f || recipe.MeltDurationSeconds < 0f)
                {
                    result.AddWarning($"Recipe \"{rid}\" has negative duration (forge/melt).");
                }

                var ingredients = recipe.Ingredients;
                if (ingredients == null || ingredients.Length == 0)
                {
                    result.AddWarning($"Recipe \"{rid}\" has no ingredients.");
                    continue;
                }

                for (var g = 0; g < ingredients.Length; g++)
                {
                    var ing = ingredients[g];
                    if (ing == null)
                    {
                        result.AddError($"Recipe \"{rid}\" Ingredients[{g}] is null.");
                        continue;
                    }

                    var ingId = ing.ItemId;
                    if (string.IsNullOrWhiteSpace(ingId))
                    {
                        result.AddError($"Recipe \"{rid}\" has empty ingredient ItemId at index {g}.");
                        continue;
                    }

                    if (itemIds.Count > 0 && !itemIds.Contains(ingId))
                    {
                        result.AddError($"Recipe \"{rid}\" references unknown ingredient ItemId: \"{ingId}\".");
                    }

                    if (ing.Amount <= 0f)
                    {
                        result.AddError($"Recipe \"{rid}\" ingredient \"{ingId}\" Amount must be greater than zero.");
                    }
                }
            }
        }
    }
}
