using System.Threading;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TestEditorBehaviour : MonoBehaviour
{
    #region Fields

    public float Count = 42;
    public int Step = 2;

    #endregion


    #region UnityMethods

    private void Start()
    {
#if UNITY_EDITOR

        for (var i = 0; i < Count; i++)
        {
            EditorUtility.DisplayProgressBar("Загрузка", $"Проценты {i}", i / Count);
            Thread.Sleep(Step * 100);
        }
        EditorUtility.ClearProgressBar();
        var isPressed = EditorUtility.DisplayDialog("Вопрос", @"А оно тебе нужно ? ", "Ага", "Или нет");
        if (isPressed)
        {
            EditorApplication.isPaused = true;
        }

#endif
    }

    #endregion
}
