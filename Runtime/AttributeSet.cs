using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fsi.Attribute
{
    [Serializable]
    public class AttributeSet<T>
        where T : AttributeInstance, new()
    {
        [SerializeField]
        private List<T> attributes;
        public List<T> Attributes => attributes;
        
        public AttributeSet()
        {
            attributes = new List<T>();
        }

        public AttributeSet(List<T> attributes)
        {
            this.attributes = new List<T>();
            foreach (T attribute in attributes)
            {
                T t = new();
                t.SetFromInstance(attribute);
                this.attributes.Add(t);
            }
        }

        public AttributeSet(AttributeSet<T> attributes)
        {
            this.attributes = new List<T>();
            foreach (T attribute in attributes.Attributes)
            {
                T t = new();
                t.SetFromInstance(attribute);
                this.attributes.Add(t);
            }
        }

        public T GetAttribute(AttributeType type)
        {
            foreach (T a in attributes)
            {
                if (a.Type == type)
                {
                    return a;
                }
            }

            return null;
        }

        public bool TryGetAttribute(AttributeType type, out T attribute)
        {
            foreach (T a in attributes)
            {
                if (a.Type == type)
                {
                    attribute = a;
                    return true;
                }
            }
            
            attribute = null;
            return false;
        }

        public void AddAttribute(T attribute)
        {
            foreach (T a in attributes)
            {
                if (a.Type == attribute.Type)
                {
                    a.Add(attribute);
                    return;
                }
            }
            
            attributes.Add(attribute);
        }

        public void AddAttribute(AttributeSet<T> attribute)
        {
            foreach (T instance in attribute.attributes)
            {
                AddAttribute(instance);
            }
        }
    }
}