using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Plugins.KennethDevelops.Events;

// This class is automatically generated.
// Modifications will be overwritten.

namespace Plugins.KennethDevelops.Events
{
	[CustomEditor(typeof(OnGameStartDispatcher))]
	public class OnGameStartDispatcherEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			base.OnInspectorGUI();

			// Enable or disable the button based on whether the game is playing
			GUI.enabled = Application.isPlaying;
			// Draws a button that executes the Dispatch() method when pressed
			if (GUILayout.Button("Dispatch")) {
				//((OnGameStartDispatcher)target).Dispatch();
				Debug.Log("Dispatched");
			}
			// Reset GUI enabled state
			GUI.enabled = true;
		}
	}
}
