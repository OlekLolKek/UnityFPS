using System.Collections.Generic;
using System.IO;
using UnityEngine;


public static class VideoSettingsRepository
{
    #region Fields

    private static VideoSettings _videoSettings;
    private static readonly IData<VideoSettings> _data;

    #endregion


    #region ClassLifeCycles

    /// <summary>
    /// В конструкторе загружаем сохранённые настройки
    /// </summary>
    static VideoSettingsRepository()
    {
        _data = new DataXMLSerializer<VideoSettings>();
        _data.SetOptions(Path.Combine(Application.dataPath, "VideoSettings.xml"));
        _videoSettings = _data.Load();
    }

    #endregion


    #region Properties

    public static VideoSettings VideoSettings
    {
        get
        {
            return _videoSettings ?? (_videoSettings = DefaultSettings());
        }

        set
        {
            _videoSettings = value;
        }
    }

    #endregion


    #region Methods

    private static VideoSettings DefaultSettings()
    {
        var result = new VideoSettings();
        var qualityNamesList = QualitySettings.names;
        var i = 0;
        result.Items = new List<VideoSettingsItems>();
        foreach (var name in qualityNamesList)
        {
            QualitySettings.SetQualityLevel(i++);
            result.Items.Add(
                new VideoSettingsItems
                {
                    Name = name,
                    ScreenResolution = Screen.currentResolution,
                    SoftParticles = QualitySettings.softParticles,
                    ShadowQuality = QualitySettings.shadows
                });
        }
        return result;
    }

    public static void SaveData()
    {
        _data.Save(VideoSettings);
        Debug.Log(Path.Combine(Application.dataPath, "VideoSettings.xml"));
    }

    #endregion
}
