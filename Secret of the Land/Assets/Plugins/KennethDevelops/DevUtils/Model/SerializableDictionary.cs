using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace KennethDevelops.DevUtils.Model {
    
    /// <summary>
    /// The SerializableDictionary class allows you to create your own serializable dictionaries within Unity. 
    /// It can be used to create dictionaries of any key-value pair types. 
    /// For more information, check the full documentation https://github.com/kennethdevelops/DevUtilsDocs/wiki/SerializableDictionary.
    /// </summary>
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver{
    
        /// <summary>
        /// Represents a list of dictionary keys. 
        /// </summary>
        [SerializeField]
        private List<TKey> keys = new List<TKey>();
     
        /// <summary>
        /// Represents a list of dictionary values.
        /// </summary>
        [SerializeField]
        private List<TValue> values = new List<TValue>();
     
        /// <summary>
        /// Default constructor for the SerializableDictionary class.
        /// </summary>
        public SerializableDictionary() {}

        /// <summary>
        /// Constructor for the SerializableDictionary class used during the serialization process.
        /// </summary>
        protected SerializableDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// This method is called before Unity serializes the object. It adds the keys and values to separate lists.
        /// </summary>
        public void OnBeforeSerialize() {
            keys.Clear();
            values.Clear();
            foreach(var pair in this){
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }
     
        /// <summary>
        /// This method is called after Unity has deserialized the object. It clears the dictionary and adds keys and values from separate lists.
        /// </summary>
        public void OnAfterDeserialize() {
            Clear();
 
            if(keys.Count != values.Count)
                throw new Exception("[ SerializableDictionary ] There are '"+ keys.Count + "' keys and '" + values.Count + "' values after deserialization. Key and Value types must be marked as serializable.");
 
            for(var i = 0; i < keys.Count; i++)
                Add(keys[i], values[i]);
        }
    }
}