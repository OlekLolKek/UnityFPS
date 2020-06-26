using System;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class SceneField
{
    #region Fields

    [SerializeField] private Object sceneAsset;
    public Object SceneAsset => sceneAsset;

    #endregion


    #region Methods

    public static implicit operator string (SceneField sceneField)
    {
        return sceneField.sceneAsset != null ? sceneField.sceneAsset.name : String.Empty;
    }

    public override string ToString()
    {
        return this;
    }

    #endregion
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(SceneField))]
public class SceneFieldPropertyDrawer : PropertyDrawer
{
    #region Methods

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var sceneAsset = property.FindPropertyRelative("sceneAsset");

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        var value = EditorGUI.ObjectField(position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);

        sceneAsset.objectReferenceValue = value;
    }

    #endregion
}

#endif