using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Art.Editor
{
    using UnityEngine;
    using UnityEditor;

    public class ArtPersonCreatorWindow : EditorWindow
    {
        private ArtPersonType selectedOption = ArtPersonType.None; // Default selection
        private const string TEMPLATE_PATH = "Assets/Scripts/ArtPerson/Editor/ArtistTemplate.txt";
        private const string ARTIST_SCRIPT_FOLDER = "Assets/Scripts/ArtPerson/ArtPersons";
        [MenuItem("Tools/Create Artist Tool")]
        public static void ShowWindow()
        {
            GetWindow<ArtPersonCreatorWindow>("Enum Selector");
        }

        private void OnGUI()
        {
            GUILayout.Label("Choose an option:", EditorStyles.boldLabel);
            GUIContent label = new GUIContent("Select Option:");
            List<ArtPersonType> existingArtPersons = ArtPersonManagerEditor.LoadArtPersonDataFromFolder()
                .Select((x) => x.ArtPersonType).ToList();
            // Dropdown to select enum
            selectedOption = (ArtPersonType)EditorGUILayout.EnumPopup(label, selectedOption,
                (x) => { return !existingArtPersons.Contains((ArtPersonType)x); }, false);

            GUILayout.Space(10);

            // Button to trigger action
            if (GUILayout.Button("Execute Action"))
            {
                ExecuteAction(selectedOption);
            }
        }

        private void ExecuteAction(ArtPersonType option)
        {
           CreateScriptFromTemplate(option);
        }


        

        private void CreateScriptFromTemplate(ArtPersonType artPersonType)
        {
            string className = artPersonType.ToString();
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
            ArtPersonData newArtPerson = ScriptableObject.CreateInstance<ArtPersonData>();
            newArtPerson.ArtPersonTypeEditor = artPersonType;
            newArtPerson.DisplayNameEditor = className;
            AssetDatabase.CreateAsset(newArtPerson, ArtPersonManagerEditor.folderPath + "/" + className + ".asset");
            selectedOption = ArtPersonType.None;
            
            //Refresh ArtPersonLibraries
            ArtPersonLibrary[] artPerson = FindObjectsOfType<ArtPersonLibrary>();
            foreach (var artPersonLibrary in artPerson)
            {
                ArtPersonManagerEditor.LoadArtPersonDataIntoManager(artPersonLibrary);
            }
            
            // Refresh the AssetDatabase
            
            
            AssetDatabase.Refresh();
        }
    }
}