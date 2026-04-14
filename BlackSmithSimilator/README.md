# Blacksmith Simulator - Project Guide (EN)

Turkish version: [README.tr.md](README.tr.md)

This repository contains the base project structure for a medieval blacksmith simulator made with Unity + C#.
The structure is aligned with your requested systems: forging flow, day cycle, NPC orders, restoration, theft events, upgrades, progression, handbook, shop economy, and JSON save pipeline.

## Technology
- Unity + C#
- ScriptableObjects for static design data
- JSON serialization for runtime save data

## Architecture Layers
- `Core`: game bootstrap and time/day flow
- `Data/ScriptableObjects`: static definitions (items, recipes, skills, upgrades, alloy notes, dialogues)
- `Data/RuntimeModels`: runtime mutable save models
- `Infrastructure/Serialization`: JSON save/load implementation
- `Gameplay`: gameplay domain modules (forging, orders, restoration, theft, shops, progression, handbook, events, player carry weight)
- `UI`: basic runtime UI views

## Full Folder Structure
```text
BlacksmithSimulator/
├─ Assets/
│  └─ _Project/
│     ├─ Animations/
│     ├─ Art/
│     ├─ Audio/
│     │  ├─ Ambience/
│     │  ├─ NPC/
│     │  └─ Shops/
│     ├─ Materials/
│     ├─ Prefabs/
│     ├─ Scenes/
│     ├─ ScriptableObjects/
│     │  ├─ Alloys/
│     │  ├─ Dialogue/
│     │  ├─ Items/
│     │  ├─ Progression/
│     │  ├─ Recipes/
│     │  └─ Upgrades/
│     ├─ Scripts/
│     │  ├─ Core/
│     │  │  ├─ Bootstrap/
│     │  │  └─ Time/
│     │  ├─ Data/
│     │  │  ├─ RuntimeModels/
│     │  │  └─ ScriptableObjects/
│     │  │     ├─ Alloys/
│     │  │     ├─ Dialogue/
│     │  │     ├─ Items/
│     │  │     ├─ Progression/
│     │  │     ├─ Recipes/
│     │  │     └─ Upgrades/
│     │  ├─ Gameplay/
│     │  │  ├─ Economy/
│     │  │  ├─ Events/
│     │  │  ├─ Forging/
│     │  │  ├─ Handbook/
│     │  │  ├─ NPC/
│     │  │  ├─ Orders/
│     │  │  ├─ Player/
│     │  │  ├─ Progression/
│     │  │  ├─ Restoration/
│     │  │  ├─ Shops/
│     │  │  ├─ Theft/
│     │  │  └─ Upgrades/
│     │  ├─ Infrastructure/
│     │  │  └─ Serialization/
│     │  ├─ UI/
│     │  │  └─ Views/
│     │  ├─ Editor/
│     │  └─ BlacksmithSimulator.Runtime.asmdef
│     ├─ StreamingAssets/
│     │  └─ Database/
│     │     ├─ dialogue.seed.json
│     │     ├─ items.seed.json
│     │     ├─ progression.seed.json
│     │     ├─ recipes.seed.json
│     │     ├─ recipes.tiers.seed.json
│     │     └─ upgrades.seed.json
│     ├─ Tests/
│     └─ UI/
├─ Docs/
│  ├─ SceneAndPrefabNaming.md
│  └─ ScriptableObjectCreationOrder.md
├─ Packages/
│  └─ manifest.json
├─ .gitignore
├─ README.md
└─ README.tr.md
```

## Required Setup Documents
- Scene and prefab naming rules: `Docs/SceneAndPrefabNaming.md`
- ScriptableObject creation order: `Docs/ScriptableObjectCreationOrder.md`

## Startup Notes
1. Open this folder as a Unity project.
2. Create `Bootstrap.unity` in `Assets/_Project/Scenes`.
3. Add a `GameObject` named `GameBootstrap`.
4. Attach `GameBootstrap`, `DayNightCycleService`, and `SaveLoadService`.
5. Place ScriptableObject assets under `Assets/_Project/ScriptableObjects`.
