namespace Art.Editor
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(ArtPersonLibrary))]
    public class ArtPersonManagerEditor : Editor
    {
        private readonly string folderPath = "Assets/ScriptableObjects/ArtPersonData"; // Default folder path

        public override void OnInspectorGUI()
        {
            // Draw the default inspector first
            DrawDefaultInspector();

            // Add a small separator
            EditorGUILayout.Space();

            // Field to specify the folder path

            // Add a button to load ScriptableObjects
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Load ArtPersonData from Folder"))
            {
                LoadArtPersonDataFromFolder(folderPath);
            }
        }
       

        private void LoadArtPersonDataFromFolder(string path)
        {
            // Get the target object (ArtPersonManager)
            ArtPersonLibrary manager = (ArtPersonLibrary)target;

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

            // Assign the loaded data to the artPersonData list
            Undo.RecordObject(manager, "Load ArtPersonData"); // Allow Undo
            manager.GetType().GetField("artPersonData", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(manager, loadedData);

            // Mark the object as dirty to save the changes
            EditorUtility.SetDirty(manager);

            Debug.Log($"Loaded {loadedData.Count} ArtPersonData objects from folder: {path}");
        }
    }

}