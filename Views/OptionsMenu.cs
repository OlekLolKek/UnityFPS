using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class OptionsMenu : BaseMenu
{
    #region Fields

    [SerializeField] private GameObject _instance;
    [SerializeField] private ButtonUI _loadVideoOptionsButton;
    [SerializeField] private ButtonUI _loadSoundOptionsButtton;
    [SerializeField] private ButtonUI _loadGameOptionsButton;
    [SerializeField] private ButtonUI _backButton;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _loadVideoOptionsButton.GetText.text = LangManager.Instance.Text("OptionsMenuItems", "Video");
        _loadVideoOptionsButton.GetControl.onClick.AddListener(delegate { LoadVideoOptions(); });

        _loadSoundOptionsButtton.GetText.text = LangManager.Instance.Text("OptionsMenuItems", "Sound");
        _loadSoundOptionsButtton.GetControl.onClick.AddListener(delegate { LoadSoundOptions(); });

        _loadGameOptionsButton.GetText.text = LangManager.Instance.Text("OptionsMenuItems", "Game");
        _loadGameOptionsButton.GetControl.onClick.AddListener(delegate { LoadGameOptions(); });

        _backButton.GetText.text = LangManager.Instance.Text("OptionsMenuItems", "Back");
        _backButton.GetControl.onClick.AddListener(delegate { Back(); });
    }

    #endregion


    #region Methods

    private void LoadVideoOptions()
    {
        _interface.Execute(InterfaceObject.VideoOptions);
    }

    private void LoadSoundOptions()
    {
        _interface.Execute(InterfaceObject.AudioOptions);
    }

    private void LoadGameOptions()
    {
        _interface.Execute(InterfaceObject.GameOptions);
    }

    private void Back()
    {
        _interface.Execute(InterfaceObject.MainMenu);
    }

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

    #endregion
}
