using Newtonsoft.Json;
using UnityEngine;

namespace SI
{
    public static class Saving
    {
        public static void Save(string key, object value) => PlayerPrefs.SetString(key, JsonConvert.SerializeObject(value));

        public static bool Load<T>(string key, out T value)
        {
            value = default;

            if (!PlayerPrefs.HasKey(key))
                return false;

            value = JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(key));
            return true;
        }
    }
}
