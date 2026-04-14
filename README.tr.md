# Blacksmith Simulator - Proje Rehberi (TR)

English version: [README.md](README.md)

Bu depo, Unity + C# ile geliştirilen orta cag temali demirci simulator oyununun temel proje yapisini icerir.
Yapi; verdigin mekaniklere gore duzenlenmistir: dovme akisi, gun dongusu, NPC siparisleri, restorasyon, hirsiz olaylari, upgrade, ilerleme, el kitabi, dukkan ekonomisi ve JSON kayit sistemi.

## Teknoloji
- Unity + C#
- Statik tasarim verileri icin ScriptableObjects
- Calisma zamani kayitlari icin JSON serilestirme

## Mimari Katmanlar
- `Core`: oyun baslatma ve zaman/gun akisi
- `Data/ScriptableObjects`: statik tanimlar (item, recipe, skill, upgrade, alloy notlari, diyaloglar)
- `Data/RuntimeModels`: degisebilir kayit verileri
- `Infrastructure/Serialization`: JSON save/load altyapisi
- `Gameplay`: oyun modulleri (forging, orders, restoration, theft, shops, progression, handbook, events, player carry weight)
- `UI`: temel arayuz gorunumleri

## Tum Klasor Yapisi
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

## Gerekli Kurulum Dokumanlari
- Scene ve prefab isimlendirme kurallari: `Docs/SceneAndPrefabNaming.md`
- ScriptableObject olusturma sirasi: `Docs/ScriptableObjectCreationOrder.md`

## Baslangic Notlari
1. Klasoru Unity projesi olarak ac.
2. `Assets/_Project/Scenes` altinda `Bootstrap.unity` olustur.
3. `GameBootstrap` isimli bir `GameObject` ekle.
4. `GameBootstrap`, `DayNightCycleService` ve `SaveLoadService` scriptlerini bagla.
5. ScriptableObject assetlerini `Assets/_Project/ScriptableObjects` altina yerlestir.
