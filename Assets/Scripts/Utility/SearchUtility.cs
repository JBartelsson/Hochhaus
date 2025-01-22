using UnityEditor;
using UnityEngine;

namespace Utility
{
    public class SearchUtility
    {
        public static T FindScriptableObjectByName<T>(string name) where T : ScriptableObject
        {
            // Search for all assets of type ScriptableObject
            string[] guids = AssetDatabase.FindAssets($"{name} t:{typeof(T).Name}");

            if (guids.Length > 0)
            {
                // Get the first match
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                return AssetDatabase.LoadAssetAtPath<T>(path);
            }
            else
            {
                Debug.LogWarning($"ScriptableObject of type {typeof(T).Name} with name '{name}' not found.");
                return null;
            }
        }
    }
}