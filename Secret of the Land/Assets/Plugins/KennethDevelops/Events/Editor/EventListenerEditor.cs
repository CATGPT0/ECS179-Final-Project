using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Plugins.KennethDevelops.Events {

    [CustomEditor(typeof(EventListener))]
    public class EventListenerEditor : Editor {
        private string[] eventTypes = new string[0];
        private int      selectedEventIndex;
        
        // Full names of all types, parallel to the eventTypes array.
        string[] eventTypeFullNames;

        public override void OnInspectorGUI() {
            serializedObject.Update();
            
            RenderDefaultInspector();
            
            RenderEventTypeDropdown();
        }

        private void OnEnable() {
            RefreshEventTypes();
            DetermineSelectedEventIndex();
        }

        private void RefreshEventTypes() {
            var baseEventDerivedTypes = EventUtilities.GetDerivedFromBaseEvent().ToArray(); // Make sure to only enumerate the sequence once
            eventTypes = baseEventDerivedTypes
                        .Select(t => GetUniqueName(t, baseEventDerivedTypes))
                        .ToArray();
            eventTypeFullNames = baseEventDerivedTypes
                                .Select(t => t.AssemblyQualifiedName)
                                .ToArray();
        }

        private void DetermineSelectedEventIndex() {
            var currentEventType = ((EventListener)target).eventType;
            if (currentEventType == null) {
                Debug.LogWarning("Current event type is null. Defaulting to first type in the list.");
                selectedEventIndex = 0;
                return;
            }

            string currentEventTypeFullName = currentEventType.AssemblyQualifiedName;
            selectedEventIndex = Array.IndexOf(eventTypeFullNames, currentEventTypeFullName);
            if (selectedEventIndex < 0) {
                Debug.LogWarning($"Current event type '{currentEventTypeFullName}' not found in the list of event types. Defaulting to first type in the list.");
                selectedEventIndex = 0;
            }
        }


        private void RenderEventTypeDropdown() {
            // Draw the popup menu for event types
            int newSelectedEventIndex = EditorGUILayout.Popup("Event Type", selectedEventIndex, eventTypes);
            if (newSelectedEventIndex != selectedEventIndex)
            {
                selectedEventIndex = newSelectedEventIndex;
                Type selectedEventType = Type.GetType(eventTypeFullNames[selectedEventIndex]);
                ((EventListener)target).eventType = selectedEventType;
                EditorUtility.SetDirty(target);
            }

            
            // Apply changes to the serializedProperty - always do this at the end of OnInspectorGUI
            serializedObject.ApplyModifiedProperties();
        }

        private void RenderDefaultInspector() {
            DrawPropertiesExcluding(serializedObject, "_eventType");
        }

        private string GetUniqueName(Type type, Type[] allTypes)
        {
            string uniqueName     = type.Name;
            string namespacePart  = "";
            int    namespaceIndex = type.Namespace.LastIndexOf('.');
            if (namespaceIndex >= 0)
            {
                namespacePart = type.Namespace.Substring(namespaceIndex + 1) + ".";
            }
    
            // If there's another type with the same name
            while (allTypes.Count(t => t.Name == uniqueName && t != type) > 0)
            {
                uniqueName = namespacePart + uniqueName;
            }
    
            return uniqueName;
        }

    }
}