using System.IO;
using UnityEngine;

namespace WOS
{
    /// <summary>
    /// 
    /// </summary>
    public static class SaveSystem
    {
        public static void Save<T>(T data, string path)
        {
            var json = JsonUtility.ToJson(data);
            Debug.Log($"Saving data to {path}: {json}");
            var encryptedJson = EncryptionUtility.Encrypt(json);
            path = Application.persistentDataPath + "/" + path;
            File.WriteAllText(path, encryptedJson);
#if UNITY_WEBGL && !UNITY_EDITOR
            NativeCaller.SyncFile();
#endif
        }

        public static T Load<T>(string path)
        {
            if (!Contains(path))
            {
                return default;
            }
            path = Application.persistentDataPath + "/" + path;
            var encryptedJson = File.ReadAllText(path);
            var json = EncryptionUtility.Decrypt(encryptedJson);
            return JsonUtility.FromJson<T>(json);
        }

        public static bool Contains(string path)
        {
            path = Application.persistentDataPath + "/" + path;
            return File.Exists(path);
        }

        public static void Delete(string path)
        {
            path = Application.persistentDataPath + "/" + path;
            File.Delete(path);
        }
    }
}