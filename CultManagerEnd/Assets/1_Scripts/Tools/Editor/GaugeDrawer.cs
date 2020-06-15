using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CultManager
{
    [CustomPropertyDrawer(typeof(Gauge))]
    public class GaugeDrawer : PropertyDrawer
    {

        SerializedProperty currentProperty;
        SerializedProperty maxProperty;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float lineHeight = (position.height - 10) / 3;

            currentProperty = property.FindPropertyRelative("_value");
            maxProperty = property.FindPropertyRelative("_max");

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;



            var currentRectLabel = new Rect(position.x, position.y, (position.width - 10) / 2, lineHeight);
            var currentRect = new Rect(position.x, position.y + lineHeight + 5, (position.width - 10) / 2, lineHeight);
            var maxRectLabel = new Rect(position.x + (position.width - 10) / 2 + 10, position.y, (position.width - 10) / 2, lineHeight);
            var maxRect = new Rect(position.x + (position.width - 10) / 2 + 10, position.y + lineHeight + 5, (position.width - 10) / 2, lineHeight);

            var slashRect = new Rect(position.x + (position.width - 10) / 2 + 2, position.y + lineHeight + 5, 6, lineHeight);

            float progression = maxProperty.floatValue > 0 ? currentProperty.floatValue / maxProperty.floatValue : 0;

            EditorGUI.LabelField(currentRectLabel, "Current", new GUIStyle { alignment = TextAnchor.MiddleCenter });
            EditorGUI.LabelField(maxRectLabel, "Max", new GUIStyle { alignment = TextAnchor.MiddleCenter });

            EditorGUI.PropertyField(currentRect, currentProperty, GUIContent.none);
            EditorGUI.LabelField(slashRect, "/", new GUIStyle { alignment = TextAnchor.MiddleCenter });
            EditorGUI.PropertyField(maxRect, maxProperty, GUIContent.none);

            EditorGUI.ProgressBar(new Rect(position.x, position.y + lineHeight * 2 + 10, position.width, lineHeight), progression, System.Math.Round(progression * 100, 2).ToString() + "%");

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 3 + 10;
        }
    }
}