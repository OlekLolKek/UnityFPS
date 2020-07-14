using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public sealed class PauseController : BaseController, IInitialization
{
    #region Fields

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioMixerGroup _group;
    [SerializeField] private Text _text;

    private PlayerController _player;
    private InputController _input;
    private WeaponController _weapon;
    private AudioMixerSnapshot _pausedSnapshot;
    private AudioMixerSnapshot _playingSnapshot;

    private bool _isPaused;

    #endregion


    #region Methods

    public void Initialization()
    {
        _audioMixer = (AudioMixer)Resources.Load("MainAudioMixer");
        _audioMixer.SetFloat("MyExposedParam", 0.1f);
        _player = ServiceLocator.Resolve<PlayerController>();
        _input = ServiceLocator.Resolve<InputController>();
        _weapon = ServiceLocator.Resolve<WeaponController>();
        _pausedSnapshot = _audioMixer.FindSnapshot("Paused");
        _playingSnapshot = _audioMixer.FindSnapshot("Playing");
    }

    public void Pause()
    {
        Debug.Log("Пауза");
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            _input.IsGamePaused = true;
            _player.Off();
            Time.timeScale = 0.0f;
            //_pausedSnapshot.TransitionTo(0.0001f);
        }
        else
        {
            _input.IsGamePaused = false;
            _player.On();
            Time.timeScale = 1.0f;
            //_pausedSnapshot.TransitionTo(0.0001f);
        }
    }

    #endregion


}
