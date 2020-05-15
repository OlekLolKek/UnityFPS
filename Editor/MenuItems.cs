using UnityEditor;


public class MenuItems
{
    #region Methods

    [MenuItem("Geekbrains/Пункт меню №0")]
    private static void MenuOption()
    {
        EditorWindow.GetWindow(typeof(MyWindow), false, "Geekbrains");
    }

    [MenuItem("Geekbrains/Пункт меню №1 %#a")]
    private static void NewMenuOption()
    {

    }

    [MenuItem("Geekbrains/Пункт меню №2 %g")]
    private static void NewNestedOption()
    {

    }

    [MenuItem("Geekbrains/Пункт меню №3  g")]
    private static void NewOptionWithHotKey()
    {

    }

    [MenuItem("Geekbrains/Пункт меню/Пункт меню №3  g")]
    private static void NewOptionWithHot()
    {

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
