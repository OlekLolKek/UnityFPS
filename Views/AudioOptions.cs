using UnityEngine.EventSystems;


public class AudioOptions : BaseMenu
{
    #region PrivateData

    enum AudioMenuItems
    {
        Music,
        SFX,
        Back
    }

    #endregion
    
    
    #region Fields

    private AudioSettings _audioSettings;

    #endregion


    #region Properties

    public AudioSettings AudioSettings
    {
        get { return _audioSettings ?? (_audioSettings = AudioSettingsRepository.AudioSettings); }
    }

    #endregion


    #region Methods

    protected void CreateMenu(string[] menuItems)
    {
        _elementsOfInterface = new IControl[menuItems.Length];
        for (int i = 0; i < menuItems.Length; i++)
        {
            switch (i)
            {
                case (int)AudioMenuItems.Music:
                    {
                        break;
                    }
                case (int)AudioMenuItems.SFX:
                    {
                        break;
                    }
                case (int)AudioMenuItems.Back:
                    {
                        var tempControl = CreateControl(_interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("AudioMenuItems", "Back"));
                        tempControl.GetControl.onClick.AddListener(Back);
                        _elementsOfInterface[i] = tempControl;
                        break;
                    }
            }
        }
        if (_elementsOfInterface.Length < 0) return;
        _elementsOfInterface[0].Control.Select();
        _elementsOfInterface[0].Control.OnSelect(new BaseEventData(EventSystem.current));
    }

    public void SFXVolume(float value)
    {
        _interface.InterfaceResources.AudioMixer.SetFloat("SFXVolume", value);
    }

    private void MusicVolume(float value)
    {
        _interface.InterfaceResources.AudioMixer.SetFloat("MusicVolume", value);
    }

    private void Back()
    {
        Save();
        _interface.Execute(InterfaceObject.OptionsMenu);
    }

    public override void Hide()
    {
        if (!_isShown) return;
        Clear(_elementsOfInterface);
        AudioSettingsRepository.SaveData();
        _isShown = false;

    }

    public override void Show()
    {
        if (_isShown) return;
        var tempMenuItems = System.Enum.GetNames(typeof(AudioMenuItems));
        CreateMenu(tempMenuItems);
        _isShown = true;
    }

    private void Save()
    {
        _interface.InterfaceResources.AudioMixer.GetFloat("MusicVolume", out var music);
        _interface.InterfaceResources.AudioMixer.GetFloat("SFXVolume", out var sfx);
        _audioSettings = new AudioSettings
        {
            Music = music,
            SFX = sfx
        };
        AudioSettingsRepository.AudioSettings = _audioSettings;
        AudioSettingsRepository.SaveData();
    }

    #endregion
}
