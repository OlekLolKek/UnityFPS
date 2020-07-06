using UnityEngine;


public class TestMenu : BaseMenu
{
    #region Fields

    [SerializeField] private GameObject _instance;
    [SerializeField] private ButtonUI _buttonQuit;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _buttonQuit.GetText.text = LangManager.Instance.Text("TestMenu", "Back");
        _buttonQuit.GetControl.onClick.AddListener(BackToMainMenu);
    }

    #endregion


    #region Methods

    public override void Hide()
    {
        if (!_isShown) return;

        _instance.SetActive(false);
        _isShown = false;
    }

    public override void Show()
    {
        if (_isShown) return;

        _instance.SetActive(true);
        _isShown = true;
    }

    private void BackToMainMenu()
    {
        _interface.Execute(InterfaceObject.MainMenu);
    }

    #endregion
}
