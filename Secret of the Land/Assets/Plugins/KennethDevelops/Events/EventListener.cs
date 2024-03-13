using System;
using UnityEngine;
using UnityEngine.Events;

namespace Plugins.KennethDevelops.Events {
    public class EventListener : MonoBehaviour {
        
        [HideInInspector]
        public string eventTypeName = "";

        public int    priority = 100;
        
        public UnityEvent onEventReceived;
        
        private void OnEnable(){
            EventManager.Subscribe(eventType, EventHandler, this, priority);
        }

        private void EventHandler() {
            onEventReceived?.Invoke();
        }

        private void OnDisable() {
            EventManager.Unsubscribe(this);
        }

        public Type eventType {
            get => Type.GetType(String.IsNullOrEmpty(eventTypeName) ? "" : eventTypeName);
            set => eventTypeName = value.AssemblyQualifiedName;
        }
    }
}