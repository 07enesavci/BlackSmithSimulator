using System;

namespace BlacksmithSimulator.Data.ScriptableObjects.Recipes
{
    [Serializable]
    public sealed class RecipeIngredient
    {
        public string ItemId;
        public float Amount;
    }
}
