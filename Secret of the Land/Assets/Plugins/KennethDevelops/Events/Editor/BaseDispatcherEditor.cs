using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Plugins.KennethDevelops.Events {
    
    [CustomEditor(typeof(BaseDispatcher<>))]
    public class BaseDispatcherEditor : Editor {

        public override void OnInspectorGUI() {
            serializedObject.Update();

            base.OnInspectorGUI();

            //Draws a button that executes the Dispatch() method when pressed
            if (Application.isPlaying && GUILayout.Button("Dispatch")) {
                // ((BaseDispatcher<>)target).Dispatch();
                
                Debug.Log("Dispatched");
                
            }
        }
    }
}