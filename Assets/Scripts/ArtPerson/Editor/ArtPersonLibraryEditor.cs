namespace Art.Editor
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(ArtPersonLibrary))]
    public class ArtPersonManagerEditor : Editor
    {
        public static string folderPath = "Assets/ScriptableObjects/ArtPersonData"; // Default folder path

        public override void OnInspectorGUI()
        {
            ArtPersonLibrary manager = (ArtPersonLibrary)target;

            // Draw the default inspector first
            DrawDefaultInspector();

            // Add a small separator
            EditorGUILayout.Space();

            // Field to specify the folder path

            // Add a button to load ScriptableObjects
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Load ArtPersonData from Folder"))
            {
               LoadArtPersonDataIntoManager(manager);

            }
        }

        public static void LoadArtPersonDataIntoManager(ArtPersonLibrary manager)
        {
            List<ArtPersonData> loadedData = LoadArtPersonDataFromFolder();

            Undo.RecordObject(manager, "Load ArtPersonData"); // Allow Undo
            manager.GetType().GetField("artPersonData",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(manager, loadedData);

            // Mark the object as dirty to save the changes
            EditorUtility.SetDirty(manager);
        }


        public static List<ArtPersonData> LoadArtPersonDataFromFolder()
        {
            // Get the target object (ArtPersonManager)
            string path = folderPath;
            // Find all ScriptableObjects of type ArtPersonData in the specified folder
            string[] guids = AssetDatabase.FindAssets("t:ArtPersonData", new[] { path });
            List<ArtPersonData> loadedData = new List<ArtPersonData>();

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                ArtPersonData data = AssetDatabase.LoadAssetAtPath<ArtPersonData>(assetPath);

                if (data != null)
                {
                    loadedData.Add(data);
                }
            }


            return loadedData;
        }
    }
}