using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(DrawScriptableAttribute))]
public class DrawScriptableAttributeDrawer : PropertyDrawer
{
    Editor editor;
    private float baseHeight;
    private bool hide;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect field = new Rect(position.x, position.y, position.width, baseHeight);
        EditorGUI.ObjectField(field, property, typeof(ScriptableObject));

        DrawScriptableAttribute so = attribute as DrawScriptableAttribute;

        if (property.objectReferenceValue != null)
        {
            string id = property.serializedObject.targetObject.GetInstanceID()+property.name + property.objectReferenceValue.name;
            Rect menu = new Rect(position.x, position.y + (position.height-5)/2+5, position.width, baseHeight);

            hide = EditorGUI.InspectorTitlebar(menu, hide, property.objectReferenceValue, true);
            //if(Event.current.type == EventType.Layout )
            //hide = EditorGUILayout.InspectorTitlebar(hide, property.objectReferenceValue, true);

            if (hide)
            {
                Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);

                editor.OnInspectorGUI();
            }

            //using (var check = new EditorGUI.ChangeCheckScope())
            //{
            //    if (hide)
            //    {
            //        Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);

            //        editor.OnInspectorGUI();
            //    }
            //}
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        baseHeight = base.GetPropertyHeight(property, label);
        if (property.objectReferenceValue == null) return baseHeight;
        else return baseHeight * 2 + 5;
    }
}
