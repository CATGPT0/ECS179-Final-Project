using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Plugins.KennethDevelops.Events.Generators {
    public static class EventDispatcherGenerator {

        [MenuItem("Generate Dispatchers", menuItem = "Tools/KennethDevelops/Generate Dispatchers")]
        public static void Generate() {

            //first we remove all previously generated Dispatchers
            RemoveFilesInFolder($"{Application.dataPath}/Plugins/KennethDevelops/Events/Generated");

            var types = EventUtilities.GetDerivedFromBaseEvent();

            GenerateDispatchers(types);
            GenerateDispatcherEditors(types);

            AssetDatabase.Refresh();
        }

        private static void RemoveFilesInFolder(string path) {
            // If the directory does not exist, create it
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            // Get all the files in the directory
            var files = Directory.GetFiles(path);

            // Loop through each file and delete
            foreach (var file in files) {
                File.Delete(file);
            }
        }

        private static void GenerateDispatchers(List<Type> derivedTypes) {
            foreach (var type in derivedTypes) {
                var sb = new StringBuilder();

                // Add using directives
                sb.AppendLine("using Plugins.KennethDevelops.Events;");
                sb.AppendLine("using Plugins.KennethDevelops.Events.Impl;");
                sb.AppendLine();

                // Add generated class comment
                sb.AppendLine("// This class is automatically generated.");
                sb.AppendLine("// Modifications will be overwritten.");
                sb.AppendLine();

                // Add namespace
                sb.AppendLine("namespace Plugins.KennethDevelops.Events");
                sb.AppendLine("{");

                // Class declaration
                sb.AppendLine($"\tpublic class {type.Name}Dispatcher : BaseDispatcher<{type.Name}>");
                sb.AppendLine("\t{");

                // Field
                sb.AppendLine($"\t\tpublic {type.Name} eventData;");

                // GetEventData Method
                sb.AppendLine("\t\tprotected override BaseEvent<" + type.Name + "> GetEventData()");
                sb.AppendLine("\t\t{");
                sb.AppendLine($"\t\t\treturn eventData;");
                sb.AppendLine("\t\t}");

                // End of class
                sb.AppendLine("\t}");

                // End of namespace
                sb.AppendLine("}");

                var directory = $"{Application.dataPath}/Plugins/KennethDevelops/Events/Generated/";
                var fileName  = $"{type.Name}Dispatcher.cs";
                // Write to file
                File.WriteAllText($"{directory}{fileName}", sb.ToString());
                Debug.Log($"Generated {fileName}");
            }
        }

        private static void GenerateDispatcherEditors(List<Type> derivedTypes) {
            foreach (var type in derivedTypes) {
                var sb = new StringBuilder();

                // Add using directives
                sb.AppendLine("using System;");
                sb.AppendLine("using System.Linq;");
                sb.AppendLine("using UnityEditor;");
                sb.AppendLine("using UnityEngine;");
                sb.AppendLine("using Plugins.KennethDevelops.Events;");
                sb.AppendLine();

                // Add generated class comment
                sb.AppendLine("// This class is automatically generated.");
                sb.AppendLine("// Modifications will be overwritten.");
                sb.AppendLine();

                // Add namespace
                sb.AppendLine("namespace Plugins.KennethDevelops.Events");
                sb.AppendLine("{");

                // Class declaration
                sb.AppendLine($"\t[CustomEditor(typeof({type.Name}Dispatcher))]");
                sb.AppendLine($"\tpublic class {type.Name}DispatcherEditor : Editor");
                sb.AppendLine("\t{");

                // OnInspectorGUI Method
                sb.AppendLine("\t\tpublic override void OnInspectorGUI()");
                sb.AppendLine("\t\t{");
                sb.AppendLine("\t\t\tserializedObject.Update();");
                sb.AppendLine();
                sb.AppendLine("\t\t\tbase.OnInspectorGUI();");
                sb.AppendLine();
                sb.AppendLine("\t\t\t// Enable or disable the button based on whether the game is playing");
                sb.AppendLine("\t\t\tGUI.enabled = Application.isPlaying;");
                sb.AppendLine("\t\t\t// Draws a button that executes the Dispatch() method when pressed");
                sb.AppendLine("\t\t\tif (GUILayout.Button(\"Dispatch\")) {");
                sb.AppendLine($"\t\t\t\t//(({type.Name}Dispatcher)target).Dispatch();");
                sb.AppendLine("\t\t\t\tDebug.Log(\"Dispatched\");");
                sb.AppendLine("\t\t\t}");
                sb.AppendLine("\t\t\t// Reset GUI enabled state");
                sb.AppendLine("\t\t\tGUI.enabled = true;");
                sb.AppendLine("\t\t}");

                // End of class
                sb.AppendLine("\t}");

                // End of namespace
                sb.AppendLine("}");

                var directory = $"{Application.dataPath}/Plugins/KennethDevelops/Events/Generated/Editor/";
                var fileName  = $"{type.Name}DispatcherEditor.cs";
                // Write to file
                File.WriteAllText($"{directory}{fileName}", sb.ToString());
                Debug.Log($"Generated {fileName}");
            }
        }


    }
}