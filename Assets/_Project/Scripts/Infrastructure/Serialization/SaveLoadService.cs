using System.IO;
using BlacksmithSimulator.Data.RuntimeModels;
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
            return cachedData;
        }

        public void Save(GameSaveData data)
        {
            var path = GetSavePath();
            var json = serializer.Serialize(data);
            File.WriteAllText(path, json);
            cachedData = data;
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
