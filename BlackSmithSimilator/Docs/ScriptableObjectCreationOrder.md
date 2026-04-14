# ScriptableObject Creation Order

This order keeps data dependencies clean for the existing project scope.

## 1) Item Definitions
Create item assets first.
- Path: `Assets/_Project/ScriptableObjects/Items`
- Type: `ItemDefinitionSO`
- Includes ore, coal, ingot, weapon outputs, handles, leather parts.

## 2) Recipe Definitions
Create craft recipe assets after items.
- Path: `Assets/_Project/ScriptableObjects/Recipes`
- Type: `RecipeDefinitionSO`
- Each recipe points to output item IDs and timings.

## 3) Skill Node Definitions
Create progression skill nodes.
- Path: `Assets/_Project/ScriptableObjects/Progression`
- Type: `SkillNodeDefinitionSO`
- Include apprentice, journeyman, master ranges and unlock nodes.

## 4) Upgrade Definitions
Create workstation/tool upgrade assets.
- Path: `Assets/_Project/ScriptableObjects/Upgrades`
- Type: `UpgradeDefinitionSO`
- Include furnace, bellows, hammer, anvil, sharpening levels.

## 5) NPC Dialogue Sets
Create dialogue pools with subtitle lines.
- Path: `Assets/_Project/ScriptableObjects/Dialogue`
- Type: `NpcDialogueSetSO`
- Include random talk lines and request lines.

## 6) Alloy Notes
Create alchemy/alloy note entries.
- Path: `Assets/_Project/ScriptableObjects/Alloys`
- Type: `AlloyNoteDefinitionSO`
- Use XP milestones for unlock points.

## 7) Validate With Seed JSON
Check JSON templates after SO setup.
- Path: `Assets/_Project/StreamingAssets/Database`
- Files:
  - `items.seed.json`
  - `recipes.seed.json`
  - `recipes.tiers.seed.json`
  - `upgrades.seed.json`
  - `progression.seed.json`
  - `dialogue.seed.json`
