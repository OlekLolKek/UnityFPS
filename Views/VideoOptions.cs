using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

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
    }

    public override void Hide()
    {
        throw new System.NotImplementedException();
    }

    public override void Show()
    {
        throw new System.NotImplementedException();
    }

    #endregion
}
