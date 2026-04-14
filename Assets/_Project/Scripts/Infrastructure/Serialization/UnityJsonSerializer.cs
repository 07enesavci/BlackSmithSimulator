using UnityEngine;

namespace BlacksmithSimulator.Infrastructure.Serialization
{
    public sealed class UnityJsonSerializer : IJsonSerializer
    {
        public string Serialize<T>(T data)
        {
            return JsonUtility.ToJson(data, true);
        }

        public T Deserialize<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
    }
}
