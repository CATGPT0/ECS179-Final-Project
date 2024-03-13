using System;
using System.Collections.Generic;
using Plugins.KennethDevelops.Extensions;
using UnityEngine;
using UnityEngine.Events;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif


namespace KennethDevelops.DevUtils.Model {
    
    public class ExecuteOn : MonoBehaviour {

        public Type type = Type.Awake;
        
        public UnityEvent execute;
        
        [SerializeField] private float _stayInterval = 0.1f;

        [SerializeField] private List<string> tagFilters = new();
        [SerializeField] private LayerMask    _layerMask = -1; //-1 = all layers
        
        private float _lastStayExecutionTime;
        

        private void Awake() {
            if (type != Type.Awake) return;
            Execute();
        }

        private void Start() {
            if (type != Type.Start) return;
            Execute();
        }
        

        private void OnEnable() {
            if (type != Type.Enable) return;
            Execute();
        }

        private void OnDisable() {
            if (type != Type.Disable) return;
            Execute();
        }
        
        private void OnTriggerEnter(Collider other) {
            if (type != Type.OnTriggerEnter) return;
            
            if (tagFilters.Count > 0   && !tagFilters.Contains(other.gameObject.tag)) return;
            if (_layerMask       != -1 && _layerMask.IncludesLayer(other.gameObject.layer)) return;
            
            Execute();
        }
        
        private void OnTriggerExit(Collider other) {
            if (type != Type.OnTriggerExit) return;
            
            if (tagFilters.Count > 0   && !tagFilters.Contains(other.gameObject.tag)) return;
            if (_layerMask       != -1 && _layerMask.IncludesLayer(other.gameObject.layer)) return;
            
            Execute();
        }
        
        private void OnTriggerStay(Collider other) {
            if (type != Type.OnTriggerStay) return;
            //calls Execute() every _stayInterval seconds
            if (Time.time - _lastStayExecutionTime < _stayInterval) return;
            
            if (tagFilters.Count > 0   && !tagFilters.Contains(other.gameObject.tag)) return;
            if (_layerMask       != -1 && _layerMask.IncludesLayer(other.gameObject.layer)) return;
            
            _lastStayExecutionTime = Time.time;
            Execute();
        }
        
        private void OnCollisionEnter(Collision other) {
            if (type != Type.OnCollisionEnter) return;
            
            if (tagFilters.Count > 0   && !tagFilters.Contains(other.gameObject.tag)) return;
            if (_layerMask       != -1 && _layerMask.IncludesLayer(other.gameObject.layer)) return;
            
            Execute();
        }
        
        private void OnCollisionExit(Collision other) {
            if (type != Type.OnCollisionExit) return;
            
            if (tagFilters.Count > 0   && !tagFilters.Contains(other.gameObject.tag)) return;
            if (_layerMask       != -1 && _layerMask.IncludesLayer(other.gameObject.layer)) return;
            
            Execute();
        }
        
        private void OnCollisionStay(Collision other) {
            if (type != Type.OnCollisionStay) return;
            //calls Execute() every _stayInterval seconds
            if (Time.time - _lastStayExecutionTime < _stayInterval) return;
            
            if (tagFilters.Count > 0   && !tagFilters.Contains(other.gameObject.tag)) return;
            if (_layerMask       != -1 && _layerMask.IncludesLayer(other.gameObject.layer)) return;
            
            _lastStayExecutionTime = Time.time;
            Execute();
        }
        

#if ODIN_INSPECTOR
    [Button]
#endif
        public void Execute() {
            execute.Invoke();
        }

        public enum Type {
            Awake,
            Start,
            None,
            Enable,
            Disable,
            OnTriggerEnter,
            OnTriggerExit,
            OnTriggerStay,
            OnCollisionEnter,
            OnCollisionExit,
            OnCollisionStay,
        }
    }
}