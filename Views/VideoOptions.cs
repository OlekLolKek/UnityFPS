using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;


public class VideoOptions : BaseMenu
{
    #region PrivateData

    enum VideoOptionsMenuItems
    {
        CurrentName,
        SoftParticles,
        ShadowQuality,
        SaveAndReturn,
        Back
    }

    #endregion


    #region Fields

    private int _selectSettings;
    private VideoSettings _videoSettings;

    [SerializeField] private GameObject _instance;
    [SerializeField] private DropdownUI _currentVideoSettingsDD;
    [SerializeField] private DropdownUI _shadowQualityDD;
    [SerializeField] private ToggleUI _softParticlesToggle;
    [SerializeField] private ButtonUI _backButton;
    [SerializeField] private ButtonUI _saveAndReturnButton;

    #endregion


    #region Properties

    public VideoSettings VideoSettings
    {
        get { return _videoSettings ?? (_videoSettings = VideoSettingsRepository.VideoSettings); }
    }

    #endregion


    #region Methods

    private void CreateMenu(string[] menuItems)
    {
        _elementsOfInterface = new IControl[menuItems.Length];
        for (var i = 0; i < menuItems.Length; i++)
        {
            switch (i)
            {
                case (int)VideoOptionsMenuItems.CurrentName:
                    {
                        _currentVideoSettingsDD.GetControl.ClearOptions();
                        _currentVideoSettingsDD.GetControl.AddOptions(new List<string>(QualitySettings.names));
                        _currentVideoSettingsDD.GetControl.onValueChanged.AddListener(Call);
                        //SetSettings(QualitySettings.GetQualityLevel());
                        Call(QualitySettings.GetQualityLevel());
                        _elementsOfInterface[i] = _currentVideoSettingsDD;
                        break;
                    }
                case (int)VideoOptionsMenuItems.SoftParticles:
                    {
                        _softParticlesToggle.GetText.text = LangManager.Instance.Text("VideoOptionsMenuItems", "SoftParticles");
                        _softParticlesToggle.GetControl.onValueChanged.AddListener(SoftParticles);
                        _elementsOfInterface[i] = _softParticlesToggle;
                        break;
                    }
                case (int)VideoOptionsMenuItems.ShadowQuality:
                    {
                        _shadowQualityDD.GetControl.ClearOptions();
                        List<string> shadowLevels = new List<string>
                        {
                            LangManager.Instance.Text("ShadowsOptions", "Disable"),
                            LangManager.Instance.Text("ShadowsOptions", "Hard"),
                            LangManager.Instance.Text("ShadowsOptions", "HardAndSoft"),
                        };
                        _shadowQualityDD.GetControl.AddOptions(shadowLevels);
                        _shadowQualityDD.GetControl.onValueChanged.AddListener(ShadowQuality);
                        _elementsOfInterface[i] = _shadowQualityDD;
                        break;
                    }
                case (int)VideoOptionsMenuItems.SaveAndReturn:
                    {
                        _saveAndReturnButton.GetText.text = LangManager.Instance.Text("VideoOptionsMenuItems", "SaveAndReturn");
                        _saveAndReturnButton.GetControl.onClick.AddListener(SaveAndReturn);
                        _elementsOfInterface[i] = _saveAndReturnButton;
                        break;
                    }
                case (int)VideoOptionsMenuItems.Back:
                    {
                        _backButton.GetText.text = LangManager.Instance.Text("VideoOptionsMenuItems", "Back");
                        _backButton.GetControl.onClick.AddListener(Back);
                        _elementsOfInterface[i] = _backButton;
                        break;
                    }

                    //Debug.Log("case Save&return");
                    //var tempControl = CreateControl(_saveAndReturnButton, LangManager.Instance.Text("VideoOptionsMenuItems", "SaveAndReturn"));
                    //tempControl.GetControl.onClick.AddListener(SaveAndReturn);
                    //_elementsOfInterface[i] = tempControl;
                    //break;
            }
        }
        if (_elementsOfInterface.Length < 0) return;

        _elementsOfInterface[0].Control.Select();
        _elementsOfInterface[0].Control.OnSelect(new BaseEventData(EventSystem.current));
    }

    /// <summary>
    /// Задаём значение по умолчанию элементам интерфейса
    /// </summary>
    /// <param name="arg"></param>
    private void Call(int arg)
    {
        _selectSettings = arg;
        foreach (var control in _elementsOfInterface)
        {
            if (control == null)
            {
                continue;
            }
            if (control.Instance.name == VideoOptionsMenuItems.CurrentName.ToString() && control is DropdownUI)
            {
                var temp = control as DropdownUI;
                temp.GetControl.value = arg;
                temp.GetControl.RefreshShownValue();
            }
            else if (control.Instance.name == VideoOptionsMenuItems.SoftParticles.ToString() && control is ToggleUI)
            {
                (control as ToggleUI).GetControl.isOn = VideoSettings.Items[arg].SoftParticles;
            }
            else if (control.Instance.name == VideoOptionsMenuItems.ShadowQuality.ToString() && control is DropdownUI)
            {
                var temp = control as DropdownUI;
                temp.GetControl.value = (int)QualitySettings.shadows;
                temp.GetControl.RefreshShownValue();
            }
        }
    }

    private void SetSettings(int value)
    {
        QualitySettings.SetQualityLevel(value);
    }

    private void SoftParticles(bool value)
    {
        QualitySettings.softParticles = value;
    }

    private void ShadowQuality(int value)
    {
        QualitySettings.shadows = (ShadowQuality)value;
    }

    private void SaveAndReturn()
    {
        Debug.Log("SaveAndReturn button pressed");
        ApplySettings();
        VideoSettingsRepository.VideoSettings = _videoSettings;
        VideoSettingsRepository.SaveData();
        Hide();
        _interface.Execute(InterfaceObject.OptionsMenu);
    }

    private void Back()
    {
        Debug.Log("Back button pressed");
        Hide();
        _interface.Execute(InterfaceObject.OptionsMenu);
    }

    public override void Hide()
    {
        if (!_isShown) return;
        //Clear(_elementsOfInterface);
        _instance.SetActive(false);
        _isShown = false;
    }

    public override void Show()
    {
        if (_isShown) return;
        var tempMenuItems = System.Enum.GetNames(typeof(VideoOptionsMenuItems));
        CreateMenu(tempMenuItems);
        _instance.SetActive(true);
        Call(QualitySettings.GetQualityLevel());
        _isShown = true;
    }

    private void ApplySettings()
    {
        Save();
        QualitySettings.softParticles = VideoSettings.Items[_selectSettings].SoftParticles;
        Debug.Log("Качество после первого: " + QualitySettings.GetQualityLevel());
        QualitySettings.shadows = VideoSettings.Items[_selectSettings].ShadowQuality;
        Debug.Log("Качество после второго: " + QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(_selectSettings);
        Debug.Log("Качество после третьего: " + QualitySettings.GetQualityLevel());
    }

    /// <summary>
    /// Заносим значения с элементов UI в нашу структуру данных
    /// </summary>
    private void Save()
    {
        foreach (var control in _elementsOfInterface)
        {
            if (control == null) continue;
            var videoSettings = VideoSettings;
            var videoSettingsItems = videoSettings.Items[_selectSettings];
            if (control.Instance.name == VideoOptionsMenuItems.SoftParticles.ToString() && control is ToggleUI)
            {
                Debug.Log("Применяются частицы");
                videoSettingsItems.SoftParticles = (control as ToggleUI).GetControl.isOn;
            }
            else if (control.Instance.name == VideoOptionsMenuItems.ShadowQuality.ToString() && control is DropdownUI)
            {
                Debug.Log("Применяются ntyb");
                videoSettingsItems.ShadowQuality = (ShadowQuality)(control as DropdownUI).GetControl.value;
            }
            else if (control.Instance.name == VideoOptionsMenuItems.CurrentName.ToString() && control is DropdownUI)
            {
                Debug.Log("Применяются настройки");
                VideoSettings.CurrentSettings = (control as DropdownUI).GetControl.value;
            }
            videoSettings.Items[_selectSettings] = videoSettingsItems;
            _videoSettings = videoSettings;
            Debug.Log(QualitySettings.GetQualityLevel());
        }
        Debug.Log("Качество после цикла: " + QualitySettings.GetQualityLevel());
    }

    #endregion
}
