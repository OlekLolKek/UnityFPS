using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(FlashlightModel))]
public class FlashlightModelEditor : UnityEditor.Editor
{
    #region UnityMethods

    public override void OnInspectorGUI()
    {
        FlashlightModel flashlightModel = (FlashlightModel)target;

        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.HelpBox("Rotation speed", MessageType.None);
        flashlightModel.RotationSpeed = EditorGUILayout.Slider(flashlightModel.RotationSpeed, 1, 100);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.HelpBox("Max battery charge", MessageType.None);
        flashlightModel.MaxBatteryCharge = EditorGUILayout.Slider(flashlightModel.MaxBatteryCharge, 1, 100);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.HelpBox("Light intensity", MessageType.None);
        flashlightModel.Intensity = EditorGUILayout.Slider(flashlightModel.Intensity, 1, 100);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

    #endregion
}
