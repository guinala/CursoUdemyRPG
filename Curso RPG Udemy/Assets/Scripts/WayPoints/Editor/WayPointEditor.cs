using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WayPoint))]
public class WayPointEditor : Editor
{
    WayPoint wayPoint => target as WayPoint;

    private void OnSceneGUI()
    {
        Handles.color = Color.green;

        for (int i = 0; i < wayPoint.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            //Create Handle
            Vector3 actualPoints = wayPoint.Points[i] + wayPoint.pos;
            Vector3 newPoint = Handles.FreeMoveHandle(actualPoints, 0.5f, new Vector3(0.3f,0.3f,0.3f), Handles.SphereHandleCap);

            //Create Text
            GUIStyle text = new GUIStyle();
            text.fontStyle = FontStyle.Bold;
            text.fontSize = 16;
            text.normal.textColor = Color.white;
            Vector3 align = Vector3.down * 0.3f + Vector3.right * 0.3f;
            Handles.Label(align + wayPoint.pos + wayPoint.Points[i], $"{i + 1}", text);

            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(wayPoint, "Free Move Handle");
                wayPoint.Points[i] = newPoint - wayPoint.pos;
            }
        }
    }
}
