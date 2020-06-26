using UnityEngine;


public class CreateInterface : MonoBehaviour
{
#if UNITY_EDITOR

    #region Methods

    public void CreateMainMenu()
    {
        CreateComponent();
        gameObject.AddComponent<MainMenu>();
        gameObject.AddComponent<OptionsMenu>();
        Clear();
    }

    public void CreatePauseMenu()
    {
        CreateComponent();
        Clear();
    }

    private void Clear()
    {
        DestroyImmediate(this);
    }

    private void CreateComponent()
    {
        gameObject.AddComponent<Interface>();
        gameObject.AddComponent<InterfaceResources>();
    }

    #endregion

#endif
}
