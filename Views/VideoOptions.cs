using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using System.Windows.Markup;

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
                        break;
                    }
                case (int)VideoOptionsMenuItems.SoftParticles:
                    {
                        break;
                    }
                case (int)VideoOptionsMenuItems.ShadowQuality:
                    {
                        break;
                    }
                case (int)VideoOptionsMenuItems.SaveAndReturn:
                    {
                        var tempControl =
                            CreateControl(_interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("VideoOptionsMenuItems", "SaveAndReturn"));
                        tempControl.GetControl.onClick.AddListener(SaveAndReturn);
                        _elementsOfInterface[i] = tempControl;
                        break;
                    }
                case (int)VideoOptionsMenuItems.Back:
                    {
                        var tempControl = CreateControl(_interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("VideoOptionsMenuItems", "Back"));

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

    /// <summary>
    /// Задаём значение по умолчанию элементам интерфейса
    /// </summary>
    /// <param name="arg"></param>
    private void Call(int arg)
    {
        _selectSettings = arg;
        foreach (var control in _elementsOfInterface)
        {
            if (control == null) continue;
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
                temp.GetControl.value = (int)VideoSettings.Items[arg].ShadowQuality;
                temp.GetControl.RefreshShownValue();
            }
        }
    }

    private void SaveAndReturn()
    {
        ApplySettings();
        VideoSettingsRepository.VideoSettings = _videoSettings;
        VideoSettingsRepository.SaveData();
        Hide();
        _interface.Execute(InterfaceObject.OptionsMenu);
    }

    private void Back()
    {
        Hide();
        _interface.Execute(InterfaceObject.OptionsMenu);
    }

    public override void Hide()
    {
        if (!_isShown) return;
        Clear(_elementsOfInterface);
        _isShown = false;
    }

    public override void Show()
    {
        if (_isShown) return;
        var tempMenuItems = System.Enum.GetNames(typeof(VideoOptionsMenuItems));
        CreateMenu(tempMenuItems);
        Call(QualitySettings.GetQualityLevel());
        _isShown = true;
    }

    private void ApplySettings()
    {
        Save();
        QualitySettings.softParticles = VideoSettings.Items[_selectSettings].SoftParticles;
        QualitySettings.shadows = VideoSettings.Items[_selectSettings].ShadowQuality;
        QualitySettings.SetQualityLevel(_selectSettings, true);
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
                videoSettingsItems.SoftParticles = (control as ToggleUI).GetControl.isOn;
            }
            else if (control.Instance.name == VideoOptionsMenuItems.ShadowQuality.ToString() && control is DropdownUI)
            {
                videoSettingsItems.ShadowQuality = (ShadowQuality)(control as DropdownUI).GetControl.value;
            }
            else if (control.Instance.name == VideoOptionsMenuItems.CurrentName.ToString() && control is DropdownUI)
            {
                VideoSettings.CurrentSettings = (control as DropdownUI).GetControl.value;
            }
            videoSettings.Items[_selectSettings] = videoSettingsItems;
            _videoSettings = videoSettings;
        }
    }

    #endregion
}
