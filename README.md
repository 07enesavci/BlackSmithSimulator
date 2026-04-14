# Blacksmith Simulator

## English

This repository contains a Unity + C# medieval blacksmith simulator project skeleton.

Active Unity project root:
- `BlackSmithSimilator/`

Open from Unity Hub:
1. Add project
2. Select `BlackSmithSimilator/`
3. Open project

### Stack
- Unity + C#
- ScriptableObjects for design-time data
- JSON serialization for runtime save/load

### Architecture
- `Core`: bootstrap and day-time cycle
- `Data/ScriptableObjects`: items, recipes, progression, upgrades, dialogue, alloy notes
- `Data/RuntimeModels`: save/inventory models
- `Data/Validation`: `GameDatabaseValidator`, `SaveDataValidator`
- `Infrastructure/Serialization`: save-load and JSON abstraction
- `Gameplay`: modular service stubs
- `UI`: basic UI views

### Folder Structure (Current)
```text
BlacksmithSimulator/
├─ .git/
├─ README.md
└─ BlackSmithSimilator/
   ├─ Assets/
   │  ├─ _Project/
   │  │  ├─ Animations/
   │  │  ├─ Art/
   │  │  ├─ Audio/
   │  │  │  ├─ Ambience/
   │  │  │  ├─ NPC/
   │  │  │  └─ Shops/
   │  │  ├─ Materials/
   │  │  ├─ Prefabs/
   │  │  ├─ Scenes/
   │  │  ├─ ScriptableObjects/
   │  │  ├─ Scripts/
   │  │  ├─ StreamingAssets/Database/
   │  │  ├─ Tests/
   │  │  └─ UI/
   │  └─ (Unity default sample files)
   ├─ Docs/
   │  ├─ SceneAndPrefabNaming.md
   │  ├─ ScriptableObjectCreationOrder.md
   │  ├─ InGameDataArchitecture.md
   │  └─ InGameDataArchitecture.tr.md
   ├─ Packages/
   │  ├─ manifest.json
   │  └─ packages-lock.json
   ├─ ProjectSettings/
   ├─ .gitignore
   └─ BlackSmithSimilator.sln
```

### Important Paths
- Scripts: `BlackSmithSimilator/Assets/_Project/Scripts/`
- Seed JSON: `BlackSmithSimilator/Assets/_Project/StreamingAssets/Database/`
- Docs: `BlackSmithSimilator/Docs/`

---

## Turkce

Bu depo, Unity + C# ile gelistirilen orta cag temali demirci simulator oyununun temel proje iskeletini icerir.

Aktif Unity proje kok dizini:
- `BlackSmithSimilator/`

Unity Hub ile acilis:
1. Proje ekle
2. `BlackSmithSimilator/` klasorunu sec
3. Projeyi ac

### Teknoloji
- Unity + C#
- Tasarim zamani verileri icin ScriptableObject
- Calisma zamani save/load icin JSON serilestirme

### Mimari
- `Core`: baslatma ve gun/zaman dongusu
- `Data/ScriptableObjects`: item, tarif, ilerleme, upgrade, diyalog, alasim notlari
- `Data/RuntimeModels`: save ve envanter modelleri
- `Data/Validation`: `GameDatabaseValidator`, `SaveDataValidator`
- `Infrastructure/Serialization`: save-load ve JSON katmani
- `Gameplay`: moduler servis iskeletleri
- `UI`: temel UI gorunumleri

### Onemli Dizinler
- Scriptler: `BlackSmithSimilator/Assets/_Project/Scripts/`
- Seed JSON: `BlackSmithSimilator/Assets/_Project/StreamingAssets/Database/`
- Dokumanlar: `BlackSmithSimilator/Docs/`
