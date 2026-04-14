using System.IO;
using BlacksmithSimulator.Data.RuntimeModels;
using BlacksmithSimulator.Data.Validation;
using UnityEngine;

namespace BlacksmithSimulator.Infrastructure.Serialization
{
    public sealed class SaveLoadService : MonoBehaviour
    {
        private const string SaveFileName = "savegame.json";

        [SerializeField] private bool autoSaveOnQuit = true;

        private readonly IJsonSerializer serializer = new UnityJsonSerializer();
        private GameSaveData cachedData;

        public GameSaveData LoadOrCreate()
        {
            var path = GetSavePath();
            if (!File.Exists(path))
            {
                cachedData = new GameSaveData();
                Save(cachedData);
                return cachedData;
            }

            var json = File.ReadAllText(path);
            cachedData = serializer.Deserialize<GameSaveData>(json) ?? new GameSaveData();
            SaveDataValidator.Sanitize(cachedData);
            LogValidation(SaveDataValidator.Validate(cachedData), "Load");
            return cachedData;
        }

        public void Save(GameSaveData data)
        {
            if (data == null)
            {
                Debug.LogError("[SaveLoadService] Save called with null GameSaveData.");
                return;
            }

            SaveDataValidator.Sanitize(data);
            LogValidation(SaveDataValidator.Validate(data), "Save");

            var path = GetSavePath();
            var json = serializer.Serialize(data);
            File.WriteAllText(path, json);
            cachedData = data;
        }

        private static void LogValidation(ValidationResult result, string context)
        {
            if (result == null)
            {
                return;
            }

            foreach (var issue in result.Issues)
            {
                var text = $"[SaveData:{context}] {issue.Message}";
                if (issue.Severity == ValidationSeverity.Error)
                {
                    Debug.LogError(text);
                }
                else
                {
                    Debug.LogWarning(text);
                }
            }
        }

        private string GetSavePath()
        {
            return Path.Combine(Application.persistentDataPath, SaveFileName);
        }

        private void OnApplicationQuit()
        {
            if (autoSaveOnQuit && cachedData != null)
            {
                Save(cachedData);
            }
        }
    }
}
