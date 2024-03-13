using UnityEditor;
using UnityEngine;

namespace Plugins.KennethDevelops.Events {
    public static class EventManagerDocumentation {

        [MenuItem("Open Documentation", menuItem = "Tools/KennethDevelops/Open Documentation")]
        public static void OpenDocumentation() {
            Application.OpenURL("https://github.com/kennethdevelops/EventManagerDocs/wiki");
        }

    }
}