using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public sealed class PauseController : BaseController, IInitialization
{
    #region Fields

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioMixerGroup _group;
    [SerializeField] private Text _text;

    private PlayerController _controller;
    private AudioMixerSnapshot _pausedSnapshot;
    private AudioMixerSnapshot _playingSnapshot;

    private bool _isPaused;

    #endregion


    #region Methods

    public void Initialization()
    {
        _audioMixer = (AudioMixer)Resources.Load("MainAudioMixer");
        _audioMixer.SetFloat("MyExposedParam", 0.1f);
        _controller = ServiceLocator.Resolve<PlayerController>();
        _pausedSnapshot = _audioMixer.FindSnapshot("Paused");
        _playingSnapshot = _audioMixer.FindSnapshot("Playing");
    }

    public void Pause()
    {
        Debug.Log("Пауза");
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            _controller.Off();
            Time.timeScale = 0.0f;
            _pausedSnapshot.TransitionTo(0.0001f);
        }
        else
        {
            _controller.On();
            Time.timeScale = 1.0f;
            _pausedSnapshot.TransitionTo(0.0001f);
        }
    }

    #endregion


}
