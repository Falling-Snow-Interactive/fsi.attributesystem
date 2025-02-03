using System;
using UnityEngine;

namespace Fsi.Attribute.Entry
{
    [Serializable]
    public class AttributeEntry : ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        [SerializeField]
        private EntryType type;
        public EntryType Type => type;

        [SerializeField]
        private float value = 0;
        public float Value => value;

        public AttributeEntry(AttributeEntry copyFrom)
        {
            type = copyFrom.Type;
            value = copyFrom.Value;
        }
        
        public void OnBeforeSerialize()
        {
            name = $"{type} - {value}";
        }

        public void OnAfterDeserialize() { }
    }
}