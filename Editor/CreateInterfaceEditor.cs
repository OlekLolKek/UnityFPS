using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(CreateInterface))]
public class CreateInterfaceEditor : Editor
{
    #region Fields

    private static CreateInterface _interface;
    private static bool _isMainMenuButtonPressed;
    private static bool _isPauseMenuButtonPressed;

    #endregion


    #region UnityMethods

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var interfaceTarget = (CreateInterface)target;
        if (EditorApplication.isPlaying) return;
        _isMainMenuButtonPressed = GUILayout.Button("Создать главное меню", EditorStyles.miniButton);
        _isPauseMenuButtonPressed = GUILayout.Button("Создать меню паузы", EditorStyles.miniButton);
        if (_isMainMenuButtonPressed)
        {
            interfaceTarget.CreateMainMenu();
        }
        if (_isPauseMenuButtonPressed)
        {
            interfaceTarget.CreatePauseMenu();
        }
    }

    #endregion
}
