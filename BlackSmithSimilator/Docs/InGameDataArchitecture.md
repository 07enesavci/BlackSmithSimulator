# In-Game Data Architecture and Database

This document describes the **foundation data architecture** and how **database content** (ores, weapons such as swords, crafting recipes) is stored so other systems can access it in a **modular** way.

## Goals

- Single source of truth for design-time data (items, recipes, related definitions).
- Clear split between **authoring data** (ScriptableObjects and optional JSON seeds) and **runtime player state** (save file JSON).
- **Validation** to catch bad data early (duplicate IDs, broken recipe links, corrupted saves).

## Design-Time Database (ScriptableObjects)

Authoring uses Unity **ScriptableObject** assets.

| Role | Type | Location (assets) |
|------|------|-------------------|
| Base template for all items | `ItemDefinitionBaseSO` | Implemented in code; concrete assets use `ItemDefinitionSO` |
| Item row (ores, fuels, weapons, components) | `ItemDefinitionSO` | `Assets/_Project/ScriptableObjects/Items` |
| Crafting recipe row | `RecipeDefinitionSO` | `Assets/_Project/ScriptableObjects/Recipes` |
| Aggregated database | `GameDatabaseSO` | `Assets/_Project/ScriptableObjects` (one asset referencing all items and recipes) |

**Modular access:** gameplay code should resolve data by stable string IDs (`ItemId`, `RecipeId`) and load rows from `GameDatabaseSO` (or a future lookup service that wraps it). Other features (forging, shops, UI) depend on these IDs, not on scene references to individual assets.

Creation order for assets is documented in `Docs/ScriptableObjectCreationOrder.md`.

## JSON Database (Seeds and Templates)

JSON files under `Assets/_Project/StreamingAssets/Database` hold **seed** or **template** data for design and tooling. Examples:

- `items.seed.json` — item rows aligned with `ItemDefinitionSO` fields.
- `recipes.seed.json` — recipe rows aligned with `RecipeDefinitionSO` fields.
- `recipes.tiers.seed.json` — recipe IDs grouped by crafting tier (apprentice / journeyman / master).
- Additional seeds (`upgrades.seed.json`, `progression.seed.json`, `dialogue.seed.json`) support related systems.

These files are **not** the player save; they help keep data structured and version-controlled alongside code.

## Runtime Save Data (Player State)

Player progress uses **JSON serialization** via `SaveLoadService` and `GameSaveData` (`Assets/_Project/Scripts/Data/RuntimeModels`). This is separate from the design database above.

## Security and Validation

| Layer | Class | What it checks |
|-------|--------|----------------|
| Design database | `GameDatabaseValidator` | Duplicate or empty IDs, recipe output/ingredient IDs that do not exist in the item set, invalid amounts, etc. Triggered in the Editor on `GameDatabaseSO` validate. |
| Save data | `SaveDataValidator` | Negative money/XP, invalid time fields, inventory inconsistencies (e.g. same `ItemId` with conflicting `UnitType`), duplicate stacks, corrupted lists. `Sanitize` repairs safe cases before persistence. |

Both return a shared `ValidationResult` with errors and warnings.

## Summary

- **Ores, swords, and recipes** live as **ScriptableObject** rows (with optional **JSON seeds** mirroring the same structure).
- **One aggregated `GameDatabaseSO`** gives a stable entry point for the rest of the game.
- **Validators** keep design data and save data **consistent and safe** as content grows.
