using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public sealed class MainMenu : BaseMenu
{
    #region Fields

    [SerializeField] private GameObject _instance;
    [SerializeField] private ButtonUI _buttonStartGame;
    [SerializeField] private ButtonUI _buttonContinue;
    [SerializeField] private ButtonUI _buttonOptions;
    [SerializeField] private ButtonUI _buttonQuit;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _buttonStartGame.GetText.text = LangManager.Instance.Text("MainMenuItems", "NewGame");
        _buttonStartGame.GetControl.onClick.AddListener(delegate { LoadNewGame(1); });
        Debug.Log(SceneManagerHelper.Instance);
        Debug.Log(SceneManagerHelper.Instance.Scenes);
        Debug.Log(SceneManagerHelper.Instance.Scenes.Game);
        Debug.Log(SceneManagerHelper.Instance.Scenes.Game.SceneAsset);
        //Debug.Log(SceneManagerHelper.Instance.Scenes.Game.SceneAsset.name);

        _buttonContinue.GetText.text = LangManager.Instance.Text("MainMenuItems", "Continue");
        _buttonContinue.SetInteractible(false);

        _buttonOptions.GetText.text = LangManager.Instance.Text("MainMenuItems", "Options");
        _buttonOptions.GetControl.onClick.AddListener(ShowOptions);

        _buttonQuit.GetText.text = LangManager.Instance.Text("MainMenuItems", "Quit");
        _buttonQuit.GetControl.onClick.AddListener(delegate { _interface.QuitGame(); });
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

    private void ShowOptions()
    {
        _interface.Execute(InterfaceObject.OptionsMenu);
    }

    private void LoadNewGame(int lvl)
    {
        _interface.LoadSceneAsync(lvl);
    }

    private void LoadNewGame(string lvl)
    {
        //SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
        _interface.LoadSceneAsync(lvl);
    }

    private void LoadNewGame(Scene lvl)
    {
        //SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
        _interface.LoadSceneAsync(lvl);
    }

    private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
    }

    #endregion
}
