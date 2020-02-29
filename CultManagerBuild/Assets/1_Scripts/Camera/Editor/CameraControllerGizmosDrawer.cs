using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CultManager
{
    [CustomEditor(typeof(CameraController))]
    public class CameraControllerGizmosDrawer : Editor
    {
        private CameraController camera;
        private SerializedProperty property;

        private void OnSceneGUI()
        {
            camera = target as CameraController;
            CamFocusGizmo();
            OrbitSpanGizmo();


        }

        private void CamFocusGizmo()
        {
            Handles.color = Color.cyan;
            Handles.DrawDottedLine((serializedObject.FindProperty("focusPoint").objectReferenceValue as Transform).position, (serializedObject.FindProperty("positionController").objectReferenceValue as Transform).position, 3.0f);
        }

        private void OrbitSpanGizmo()
        {
            Vector3 camPos = (serializedObject.FindProperty("camTransform").objectReferenceValue as Transform).position;
            Vector3 focusPos = (serializedObject.FindProperty("focusPoint").objectReferenceValue as Transform).position;
            Vector3 controllerPos = (serializedObject.FindProperty("positionController").objectReferenceValue as Transform).position;

            Vector2 startDirection;
            float radius;

            if (!Application.isPlaying)
            {
                Vector3 direction = (camPos - focusPos).normalized;
                startDirection = new Vector2(direction.x, direction.z);
                radius = Vector2.Distance(new Vector2(camPos.x, camPos.z), new Vector2(focusPos.x, focusPos.z));
            }
            else
            {
                startDirection = serializedObject.FindProperty("startDirection").vector2Value;
                radius = serializedObject.FindProperty("radius").floatValue;
            }
            

            float angle = (serializedObject.FindProperty("settings").objectReferenceValue as CameraControllerSettings).spanAngle * Mathf.Deg2Rad;
            float angleCorrection = Mathf.Atan2(startDirection.y, startDirection.x);

            Vector2 maxAnglePos = (new Vector2(Mathf.Cos(angleCorrection + angle), Mathf.Sin(angleCorrection + angle))).normalized;
            Vector2 minAnglePos = (new Vector2(Mathf.Cos(angleCorrection - angle), Mathf.Sin(angleCorrection - angle))).normalized;

            float maxAngle = Mathf.Atan2(maxAnglePos.y, maxAnglePos.x);
            float minAngle = Mathf.Atan2(minAnglePos.y, minAnglePos.x);


            Handles.color = Color.blue;
            Handles.DrawLine(focusPos, new Vector3(Mathf.Cos(minAngle) * radius, focusPos.y, Mathf.Sin(minAngle) * radius));
            Handles.DrawLine(focusPos, new Vector3(Mathf.Cos(maxAngle) * radius, focusPos.y, Mathf.Sin(maxAngle) * radius));
            Handles.DrawWireArc(focusPos, Vector3.up, new Vector3(startDirection.x, 0.0f, startDirection.y), angle * Mathf.Rad2Deg, radius);
            Handles.DrawWireArc(focusPos, Vector3.up, new Vector3(startDirection.x, 0.0f, startDirection.y), angle * Mathf.Rad2Deg *-1, radius);

            Handles.color = Color.green;
            Handles.DrawLine(controllerPos, new Vector3(controllerPos.x, focusPos.y, controllerPos.z));
            Handles.DrawLine(focusPos, new Vector3(controllerPos.x, focusPos.y, controllerPos.z));

            Handles.color = Color.yellow;
            Handles.DrawWireCube(new Vector3(controllerPos.x, focusPos.y, controllerPos.z), Vector3.one * 0.1f);
        }
    }
}