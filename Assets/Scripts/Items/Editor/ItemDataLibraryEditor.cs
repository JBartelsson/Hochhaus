namespace Items.Editor
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(ItemLibrary))]
    public class ItemLibraryEditor : Editor
    {
        public static string folderPath = "Assets/ScriptableObjects/ItemData"; // Default folder path

        public override void OnInspectorGUI()
        {
            ItemLibrary manager = (ItemLibrary)target;

            // Draw the default inspector first
            DrawDefaultInspector();

            // Add a small separator
            EditorGUILayout.Space();

            // Field to specify the folder path

            // Add a button to load ScriptableObjects
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Load ItemData from Folder"))
            {
               LoadArtPersonDataIntoManager(manager);

            }
        }

        public static void LoadArtPersonDataIntoManager(ItemLibrary manager)
        {
            List<ItemData> loadedData = LoadArtPersonDataFromFolder();

            Undo.RecordObject(manager, "Load ItemData"); // Allow Undo
            manager.GetType().GetField("itemData",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(manager, loadedData);

            // Mark the object as dirty to save the changes
            EditorUtility.SetDirty(manager);
        }


        public static List<ItemData> LoadArtPersonDataFromFolder()
        {
            // Get the target object (ArtPersonManager)
            string path = folderPath;
            // Find all ScriptableObjects of type ArtPersonData in the specified folder
            string[] guids = AssetDatabase.FindAssets("t:ItemData", new[] { path });
            List<ItemData> loadedData = new List<ItemData>();

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                ItemData data = AssetDatabase.LoadAssetAtPath<ItemData>(assetPath);

                if (data != null)
                {
                    loadedData.Add(data);
                }
            }


            return loadedData;
        }
    }
}