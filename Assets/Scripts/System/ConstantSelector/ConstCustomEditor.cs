using KBP.CORE;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KBP.EDITOR
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ConstSelector))]
    public class ConstantSelectorDrawer: StringAttributeDrawer
    {
        const string DEFAULT_CONSTANTS_ROOT = "Assets/Constants/SOConstantsContainer.asset";

        protected override Rect Init(Rect position, SerializedProperty property)
        {
            SOConstantsContainer cContainer =
                AssetDatabase.LoadAssetAtPath<SOConstantsContainer>(DEFAULT_CONSTANTS_ROOT);
           
            var attr = (ConstSelector)attribute;
            List<string> types = attr != null && attr.Types != null ? attr.Types.ToList() : new List<string>();

            if(Values != null)
            {
                Values.Clear();
            }
            else
            {
                Values = new List<string>();
            }

            AddItem("none");
            AddItem(cContainer.GetConsts(attr.Types));

            return base.Init(position, property);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var value = property.stringValue;

            position = Init(position, property);
            
            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            if(GUI.Button(position, value))
            {

                ConstSelectorWindow.Open((string res) =>
                    {
                        using(new EditorGUI.PropertyScope(position, label, property))
                        {
                            property.stringValue = res;
                            property.serializedObject.ApplyModifiedProperties();
                        }
                    },
                    value, Values);
            }
        }
    }

    public class StringAttributeDrawer: PropertyDrawer
    {
        protected List<string> values = null;

        protected List<string> Values
        {
            get
            {
                return values;
            }

            set
            {
                values = value;
            }
        }

        protected void AddItem(string value)
        {
            Values.Add(value);
        }

        protected void AddItem(IEnumerable<string> values)
        {
            Values.AddRange(values);
        }

        protected virtual Rect Init(Rect position, SerializedProperty property)
        {
            return position;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(property == null)
            {
                return;
            }

            EditorGUI.BeginProperty(position, label, property);


            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            position = Init(position, property);

            if(Values.Count == 0)
            {
                return;
            }

            // Don't make child fields be indented
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            Rect pathRect = new Rect(position.x + 0 * position.width / 2 + 0 * 4, position.y, position.width - 6, position.height);

            string stringValue = property.stringValue;

            int intValue = Values.IndexOf(stringValue);
            intValue = Mathf.Clamp(EditorGUI.Popup(pathRect, intValue, Values.ToArray()), 0, Values.Count - 1);
            property.stringValue = Values[intValue];

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
#endif
    public class ConstSelector: PropertyAttribute
    {
        public string[] Types;

        public ConstSelector(params string[] constType)
        {
            Types = constType;
        }
    }

}

