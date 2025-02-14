using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Items.Editor
{
    using UnityEngine;
    using UnityEditor;

    public class ItemDataCreatorWindow : EditorWindow
    {
        private ItemType selectedOption = ItemType.None; // Default selection
        private const string TEMPLATE_PATH = "Assets/Scripts/Items/Editor/ItemTemplate.txt";
        private const string ARTIST_SCRIPT_FOLDER = "Assets/Scripts/Items/ItemFunctions";
        [MenuItem("Tools/Create Item Tool")]
        public static void ShowWindow()
        {
            GetWindow<ItemDataCreatorWindow>("Enum Selector");
        }

        private void OnGUI()
        {
            GUILayout.Label("Choose an option:", EditorStyles.boldLabel);
            GUIContent label = new GUIContent("Select Option:");
            List<ItemType> existingArtPersons = ItemLibraryEditor.LoadArtPersonDataFromFolder()
                .Select((x) => x.ItemType).ToList();
            // Dropdown to select enum
            selectedOption = (ItemType)EditorGUILayout.EnumPopup(label, selectedOption,
                (x) => { return !existingArtPersons.Contains((ItemType)x); }, false);

            GUILayout.Space(10);

            // Button to trigger action
            if (GUILayout.Button("Execute Action"))
            {
                ExecuteAction(selectedOption);
            }
        }

        private void ExecuteAction(ItemType option)
        {
           CreateScriptFromTemplate(option);
        }


        

        private void CreateScriptFromTemplate(ItemType itemType)
        {
            string className = itemType.ToString();
            // className = EditorUtility.SaveFilePanel("Create Script", ARTIST_SCRIPT_FOLDER, className, "cs");

            if (string.IsNullOrEmpty(className)) return;

            className = Path.GetFileNameWithoutExtension(className);
            string scriptPath = Path.Combine(ARTIST_SCRIPT_FOLDER, className + ".cs");

            if (!Directory.Exists(ARTIST_SCRIPT_FOLDER))
                Directory.CreateDirectory(ARTIST_SCRIPT_FOLDER);

            if (File.Exists(scriptPath))
            {
                Debug.LogWarning($"Script {className}.cs already exists!");
                return;
            }

            if (!File.Exists(TEMPLATE_PATH))
            {
                Debug.LogError("Template file not found!");
                return;
            }

            // Read template and replace placeholder
            string templateContent = File.ReadAllText(TEMPLATE_PATH);
            string finalContent = templateContent.Replace("#ARTIST_NAME#", className);

            // Write new script
            File.WriteAllText(scriptPath, finalContent);
            Debug.Log($"Created script: {scriptPath}");
            ItemData newItem = ScriptableObject.CreateInstance<ItemData>();
            newItem.ItemTypeEditor = itemType;
            newItem.DisplayNameEditor = className;
            AssetDatabase.CreateAsset(newItem, ItemLibraryEditor.folderPath + "/" + className + ".asset");
            selectedOption = ItemType.None;
            
            //Refresh ArtPersonLibraries
            ItemLibrary[] artPerson = FindObjectsOfType<ItemLibrary>();
            foreach (var artPersonLibrary in artPerson)
            {
                ItemLibraryEditor.LoadArtPersonDataIntoManager(artPersonLibrary);
            }
            
            // Refresh the AssetDatabase
            
            
            AssetDatabase.Refresh();
        }
    }
}