using UnityEditor;


public class MenuItems
{
    #region Methods

    [MenuItem("Geekbrains/Создание объектов")]
    private static void MenuOption()
    {
        EditorWindow.GetWindow(typeof(MyWindow), false, "Geekbrains");
    }

    [MenuItem("Assets/Geekbrains")]
    private static void LoadAdditiveScene()
    {

    }

    [MenuItem("Assets/Create/Geekbrains")]
    private static void AddConfig()
    {

    }
    [MenuItem("CONTEXT/Rigidbody/Geekbrains")]
    private static void NewOpenForRigidbody()
    {

    }

    #endregion
}
