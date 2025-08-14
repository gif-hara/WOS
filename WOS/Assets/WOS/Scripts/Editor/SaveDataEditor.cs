using UnityEditor;
using UnityEngine;

namespace WOS.Editor
{
    public class SaveDataEditor
    {
        [MenuItem("WOS/SaveData/Open")]
        public static void Open()
        {
            EditorUtility.RevealInFinder($"{Application.persistentDataPath}/{SaveData.Path}");
        }

        [MenuItem("WOS/SaveData/Delete")]
        public static void Delete()
        {
            if (EditorUtility.DisplayDialog("確認", "セーブデータを削除します。よろしいですか？", "Yes", "No"))
            {
                if (SaveSystem.Contains(SaveData.Path))
                {
                    SaveSystem.Delete(SaveData.Path);
                }
            }
        }
    }
}
