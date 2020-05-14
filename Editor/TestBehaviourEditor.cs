using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(TestBehaviour))]
public class TestBehaviourEditor : UnityEditor.Editor
{
    #region Fields

    private bool _isPressedButtonOk;

    #endregion


    #region Methods

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        TestBehaviour testTarget = (TestBehaviour)target;

        testTarget.count = EditorGUILayout.IntSlider(testTarget.count, 10, 50);
        testTarget.offset = EditorGUILayout.IntSlider(testTarget.offset, 1, 5);

        testTarget.obj =
            EditorGUILayout.ObjectField("Объект, который хотим вставить",
            testTarget.obj, typeof(GameObject), false)
            as GameObject;

        var isPressButton = GUILayout.Button("Создание объектов по кнопке",
            EditorStyles.miniButtonLeft);

        _isPressedButtonOk = GUILayout.Toggle(_isPressedButtonOk, "Ok");

        if (isPressButton)
        {
            testTarget.CreateObj();
            _isPressedButtonOk = true;
        }

        if (_isPressedButtonOk)
        {
            testTarget.Test = EditorGUILayout.Slider(testTarget.Test, 10, 50);
            EditorGUILayout.HelpBox("Вы нажали на кнопку", MessageType.Warning);

            var isPressAddButton = GUILayout.Button("Add com",
                EditorStyles.miniButtonLeft);
            if (isPressAddButton)
            {
                testTarget.AddComponent();
            }
            if (GUILayout.Button("Rem Com",
                EditorStyles.miniButtonLeft))
            {
                testTarget.RemoveComponent();
            }
        }
    }

    #endregion
}
