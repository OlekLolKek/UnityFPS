using UnityEngine;
using UnityEngine.SceneManagement;


public sealed class MainMenu : BaseMenu
{
    #region Fields

    [SerializeField] private GameObject _mainPanel;

    [SerializeField] private ButtonUI _newGame;
    [SerializeField] private ButtonUI _continue;
    [SerializeField] private ButtonUI _options;
    [SerializeField] private ButtonUI _quit;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _newGame.GetText.text = LangManager.Instance.Text("MainMenuItems", "NewGame");
        _newGame.GetControl.onClick.AddListener(delegate
        {
            LoadNewGame(SceneManagerHelper.Instance.Scenes.Game.SceneAsset.name);
        });

        _continue.GetText.text = LangManager.Instance.Text("MainMenuItems", "Continue");
        _continue.SetInteractible(false);
        _options.GetText.text = LangManager.Instance.Text("MainMenuItems", "Options");
        _options.SetInteractible(false);

        _quit.GetText.text = LangManager.Instance.Text("MainMenuItems", "Quit");
        _quit.GetControl.onClick.AddListener(delegate
        {
            _interface.QuitGame();
        });
    }

    #endregion


    #region Methods

    public override void Hide()
    {
        if (_isShown) return;
        _mainPanel.gameObject.SetActive(false);
        _isShown = false;
    }

    public override void Show()
    {
        if (_isShown) return;
        _mainPanel.gameObject.SetActive(true);
        _isShown = true;
    }

    private void ShowOptions()
    {
        _interface.Execute(InterfaceObject.OptionsMenu);
    }

    private void LoadNewGame(string lvl)
    {
        //SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
        _interface.LoadSceneAsync(lvl);
    }

    private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //init game;

        SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
    }

    #endregion
}
