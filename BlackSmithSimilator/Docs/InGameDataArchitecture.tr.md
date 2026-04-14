# Oyun Ici Veri Mimarisi ve Veritabani

Bu dokuman **temel veri mimarisini** ve kiliclar, madenler, uretim tarifleri gibi **veritabani iceriginin** nasil tutuldugunu anlatir; diger sistemlerin veriye **moduler** sekilde erismesini hedefler.

## Hedefler

- Tasarim verileri icin tek dogruluk kaynagi (item, recipe ve ilgili tanimlar).
- **Icerik uretimi** (ScriptableObject ve istege bagli JSON tohumlari) ile **oyuncu durumu** (kayit dosyasi JSON) net sekilde ayrilir.
- **Dogrulama** ile hatali veriyi erken yakalamak (cift ID, kirik tarif baglantisi, bozuk kayit).

## Tasarim Veritabani (ScriptableObjects)

Icerik Unity **ScriptableObject** assetleri ile tutulur.

| Rol | Tip | Konum (asset) |
|-----|-----|----------------|
| Tum itemlar icin temel sablon | `ItemDefinitionBaseSO` | Kodda; somut assetler `ItemDefinitionSO` kullanir |
| Item satiri (maden, yakit, silah, parca) | `ItemDefinitionSO` | `Assets/_Project/ScriptableObjects/Items` |
| Uretim tarifi satiri | `RecipeDefinitionSO` | `Assets/_Project/ScriptableObjects/Recipes` |
| Toplu veritabani | `GameDatabaseSO` | `Assets/_Project/ScriptableObjects` (tum item ve tariflere referans) |

**Moduler erisim:** oyun kodu veriyi sabit string ID (`ItemId`, `RecipeId`) ile cozer; `GameDatabaseSO` uzerinden (veya bunu saracak bir lookup servisi ile) okur. Dovme, dukkan, UI gibi ozellikler bu IDlere baglanir, tek tek sahne referanslarina degil.

Asset olusturma sirasi: `Docs/ScriptableObjectCreationOrder.md`.

## JSON Veritabani (Tohum ve sablonlar)

`Assets/_Project/StreamingAssets/Database` altindaki JSON dosyalari tasarim ve araclar icin **tohum/sablon** veridir. Ornekler:

- `items.seed.json` — `ItemDefinitionSO` alanlariyla uyumlu item satirlari.
- `recipes.seed.json` — `RecipeDefinitionSO` alanlariyla uyumlu tarif satirlari.
- `recipes.tiers.seed.json` — tarif IDlerinin seviyeye gore gruplanmasi.
- Diger tohumlar (`upgrades.seed.json`, `progression.seed.json`, `dialogue.seed.json`) ilgili sistemlere destek verir.

Bunlar **oyuncu kaydi degildir**; kodla birlikte surum kontrolu ve duzenli veri tutmak icin kullanilir.

## Calisma Zamani Kayit Verisi (Oyuncu durumu)

Oyuncu ilerlemesi `SaveLoadService` ile `GameSaveData` uzerinden **JSON** ile saklanir; tasarim veritabanindan ayri katmandir.

## Guvenlik ve Dogrulama

| Katman | Sinif | Ne kontrol eder |
|--------|-------|-----------------|
| Tasarim veritabani | `GameDatabaseValidator` | Bos veya cift ID, tarif ciktisi/malzeme IDsinin item setinde olmamasi, gecersiz miktar vb. Editor’da `GameDatabaseSO` validate ile calisir. |
| Kayit verisi | `SaveDataValidator` | Negatif para/XP, gecersiz zaman, envanter tutarsizligi (ayni `ItemId` icin cakisan `UnitType`), cift yigin, bozuk listeler. `Sanitize` guvenli duzeltmeleri kayittan once uygular. |

Ikisi de ortak `ValidationResult` ile hata ve uyari dondurur.

## Ozet

- **Maden, kilic, tarif** verileri **ScriptableObject** satirlari olarak tutulur; bu yapiyi **JSON tohumlari** destekler.
- **`GameDatabaseSO`** oyunun geri kalanina stabil bir giris noktasi verir.
- **Validatorlar** icerik buyudukce tasarim ve kayit verisinin **tutarli ve guvenli** kalmasini saglar.
