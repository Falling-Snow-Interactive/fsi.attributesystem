using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace Fsi.Attribute
{
    [UxmlElement]
    public partial class AttributeSetField<TSet, TInst> : VisualElement
        where TSet : AttributeSet<TInst>
        where TInst : AttributeInstance, new()
    {
        private const string DOCUMENT_PATH = "Packages/com.fallingsnowinteractive.attributesystem/Editor/AttributeSetField.uxml";
        
        private TSet attributeSet;
        
        private readonly VisualElement content;

        private readonly List<AttributeField> attributeFields = new();
        
        public AttributeSetField()
        {
            #if UNITY_EDITOR
            var asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(DOCUMENT_PATH);
            asset.CloneTree(this);
            #endif
            
            content = this.Q<VisualElement>("content");
        }

        public AttributeSetField(TSet attributeSet)
        {
            this.attributeSet = attributeSet;
            
            #if UNITY_EDITOR
            var asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(DOCUMENT_PATH);
            asset.CloneTree(this);
            #endif
            
            content = this.Q<VisualElement>("content");
            SetAttributeSet(attributeSet);
        }

        public void SetAttributeSet(TSet attributeSet)
        {
            this.attributeSet = attributeSet;

            foreach (AttributeField field in attributeFields)
            {
                content.Remove(field);
            }
            attributeFields.Clear();
            
            foreach (TInst attribute in attributeSet.Attributes)
            {
                var attributeField = new AttributeField(attribute);
                content.Add(attributeField);
                attributeFields?.Add(attributeField);
            }
        }
    }
}