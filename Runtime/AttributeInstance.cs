using System;
using System.Collections.Generic;
using Fsi.Attribute.Entry;
using UnityEngine;

namespace Fsi.Attribute
{
    [Serializable]
    public abstract class AttributeInstance : ISerializationCallbackReceiver
    {
        public event Action<AttributeInstance> Changed;
        
        [HideInInspector]
        [SerializeField]
        private string name;
        
        [SerializeField]
        private AttributeType type;
        public AttributeType Type => type;

        [SerializeField]
        private List<AttributeEntry> entry;
        public List<AttributeEntry> Entry => entry;

        public AttributeInstance()
        {
        }

        public void SetFromInstance(AttributeInstance attributeInstance)
        {
            type = attributeInstance.Type;
            entry = new List<AttributeEntry>();
            foreach (AttributeEntry entry in attributeInstance.Entry)
            {
                var attribute = new AttributeEntry(entry);
                this.entry.Add(attribute);
            }
            
            Changed?.Invoke(this);
        }

        public float GetTotal()
        {
            float mod = 1;
            float value = 0;

            foreach (AttributeEntry entry in entry)
            {
                switch (entry.Type)
                {
                    case EntryType.Flat:
                        value += entry.Value;
                        break;
                    case EntryType.Modifier:
                        mod *= entry.Value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return value * mod;
        }
        
        public float GetValue()
        {
            float value = 0;

            foreach (AttributeEntry entry in entry)
            {
                switch (entry.Type)
                {
                    case EntryType.Flat:
                        value += entry.Value;
                        break;
                    case EntryType.Modifier:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return value;
        }

        public float GetModifier()
        {
            float mod = 1;

            foreach (AttributeEntry entry in entry)
            {
                switch (entry.Type)
                {
                    case EntryType.Flat:
                        break;
                    case EntryType.Modifier:
                        mod *= entry.Value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return mod;
        }
        
        public void GetAll(out float total, out float value, out float mod)
        {
            mod = 1;
            value = 0;

            if (entry == null)
            {
                total = 0;
                return;
            }
            
            foreach (AttributeEntry entry in entry)
            {
                switch (entry.Type)
                {
                    case EntryType.Flat:
                        value += entry.Value;
                        break;
                    case EntryType.Modifier:
                        mod *= entry.Value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            total = value * mod;
        }

        public void Add(AttributeInstance other)
        {
            if (other.type != type)
            {
                return;
            }

            foreach (AttributeEntry e in other.entry)
            {
                entry.Add(e);
            }
            
            Changed?.Invoke(this);
        }

        public virtual Dictionary<string, string> GetArgs()
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            return args;
        }

        public void OnBeforeSerialize()
        {
            GetAll(out float total, out float value, out float mod);
            name = $"{type} - {value}x{mod} = {total}";
        }

        public void OnAfterDeserialize() { }
    }
}