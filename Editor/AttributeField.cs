using UnityEditor;
using UnityEngine.UIElements;

namespace Fsi.Attribute
{
    [UxmlElement]
    public partial class AttributeField : VisualElement
    {
        private const string DOCUMENT_PATH = "Packages/com.fallingsnowinteractive.attributesystem/Editor/AttributeField.uxml";

        private const string ATTRIBUTE_LABEL_KEY = "attribute_label";
        private const string VALUE_LABEL_KEY = "value_label";
        private const string MODIFIER_LABEL_KEY = "modifier_label";
        private const string TOTAL_LABEL_KEY = "total_label";
        
        public AttributeInstance Attribute { get; private set; }

        private readonly Label attributeLabel;
        private readonly Label valueLabel;
        private readonly Label modifierLabel;
        private readonly Label totalLabel;
        
        public AttributeField()
        {
            #if UNITY_EDITOR
            var asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(DOCUMENT_PATH);
            asset.CloneTree(this);
            #endif
            
            attributeLabel = this.Q<Label>(ATTRIBUTE_LABEL_KEY);
            valueLabel = this.Q<Label>(VALUE_LABEL_KEY);
            modifierLabel = this.Q<Label>(MODIFIER_LABEL_KEY);
            totalLabel = this.Q<Label>(TOTAL_LABEL_KEY);

            attributeLabel.text = "Not Set";
            valueLabel.text = "/";
            modifierLabel.text = "/";
            totalLabel.text = "/";
        }

        public AttributeField(AttributeInstance attribute)
        {
            #if UNITY_EDITOR
            var asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(DOCUMENT_PATH);
            asset.CloneTree(this);
            #endif

            attributeLabel = this.Q<Label>(ATTRIBUTE_LABEL_KEY);
            valueLabel = this.Q<Label>(VALUE_LABEL_KEY);
            modifierLabel = this.Q<Label>(MODIFIER_LABEL_KEY);
            totalLabel = this.Q<Label>(TOTAL_LABEL_KEY);
            
            SetAttribute(attribute);
        }

        public void SetAttribute(AttributeInstance attribute)
        {
            Attribute = attribute;
            
            attribute.GetAll(out float total, out float value, out float modifier);
            
            attributeLabel.text = $"{attribute.Type.ToString()}";
            valueLabel.text = $"{value:0.0}";
            modifierLabel.text = $"x{modifier:0.0}";
            totalLabel.text = $"= {total:0.0}";
        }
    }
}
