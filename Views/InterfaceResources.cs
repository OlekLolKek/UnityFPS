using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Audio;

public class InterfaceResources : MonoBehaviour
{
    #region Properties

    public ButtonUI ButtonPrefab { get; private set; }
    public Canvas MainCanvas { get; private set; }
    public LayoutGroup MainPanel { get; private set; }
    public SliderUI ProgressbarPrefab { get; private set; }
    public AudioMixer AudioMixer { get; private set; }
    public DropdownUI DDPrefab { get; private set; }
    public ToggleUI TogglePrefab { get; private set; }

    #endregion


    #region UnityMethods

    private void Awake()
    {
        ButtonPrefab = Resources.Load<ButtonUI>("Button");
        MainCanvas = FindObjectOfType<Canvas>();
        MainPanel = MainCanvas.GetComponentInChildren<LayoutGroup>();
        ProgressbarPrefab = Resources.Load<SliderUI>("Progressbar");
        AudioMixer = Resources.Load<AudioMixer>("MainAudioMixer");
    }

    #endregion
}
