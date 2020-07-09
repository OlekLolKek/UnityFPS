using System.IO;
using UnityEngine;
using UnityEngine.Audio;


public class AudioSettingsRepository 
{
    #region Fields

    private static AudioSettings _audioSettings;

    private static readonly IData<AudioSettings> _data;

    #endregion


    #region Properties

    public static AudioSettings AudioSettings
    {
        get
        {
            return _audioSettings ?? (_audioSettings = DefaultSettings());
        }
        set
        {
            _audioSettings = value;
        }
    }

    #endregion


    #region ClassLifeCycles

    static AudioSettingsRepository()
    {
        _data = new DataXMLSerializer<AudioSettings>();
        if (_data == null) return;
        _data.SetOptions(Path.Combine(Application.dataPath, "AudioSettings.xml"));
        _audioSettings = _data.Load();
    }

    #endregion


    #region Methods

    private static AudioSettings DefaultSettings()
    {
        var audioMixer = Resources.Load<AudioMixer>("MainAudioMixer");

        audioMixer.GetFloat("MusicVolume", out var music);
        audioMixer.GetFloat("SFXVolume", out var sfx);
        return new AudioSettings
        {
            Music = music,
            SFX = sfx
        };
    }

    public static void SaveData()
    {
        _data.Save(AudioSettings);
    }

    #endregion
}
