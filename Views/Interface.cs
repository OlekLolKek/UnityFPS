﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public sealed class Interface : MonoBehaviour
{
    #region Fields

    public InterfaceResources InterfaceResources { get; private set; }

    private SliderUI _progressBar;
    private BaseMenu _currentMenu;

    private readonly Stack<InterfaceObject> _interfaceObjects = new Stack<InterfaceObject>(); //dz

    private MainMenu _mainMenu;
    private OptionsMenu _optionsMenu;
    private TestMenu _testMenu;
    private VideoOptions _videoOptions;
    //private GameOptions _gameOptions;
    //private AudioOptions _audioOptions;
    //private MenuPause _menuPause;
    //private OptionsPauseMenu _optionsPauseMenu;

    #endregion


    #region UnityMethods

    private void Start()
    {
        InterfaceResources = GetComponent<InterfaceResources>();
        _mainMenu = GetComponent<MainMenu>();
        _optionsMenu = GetComponent<OptionsMenu>();
        _testMenu = GetComponent<TestMenu>();
        _videoOptions = GetComponent<VideoOptions>();
        //_gameOptions = GetComponent<GameOptions>();
        //_audioOptions = GetComponent<AudioOptions>();
        //_menuPause = GetComponent<MenuPause>();
        //_optionsPauseMenu = GetComponent<OptionsPauseMenu>();

        if (_mainMenu)
        {
            Execute(InterfaceObject.MainMenu);
        }
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (_currentMenu != null) return;

        if (_currentMenu._isShown)
        {
            _currentMenu.Hide();
        }
        else
        {
            _currentMenu.Show();
        }
    }

    #endregion


    #region Methods

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Execute(InterfaceObject menuItem) //добавить отмену
    {
        if (_currentMenu != null)
        {
            _currentMenu.Hide();
            Debug.Log($"Hidden {_currentMenu}");
            Debug.Log(_currentMenu.isActiveAndEnabled);
        }
        Debug.Log("Switch started");
        switch(menuItem)
        {
            case InterfaceObject.MainMenu:
                _currentMenu = _mainMenu;
                Debug.Log("_currentMenu = _mainMenu");
                break;
            case InterfaceObject.OptionsMenu:
                _currentMenu = _optionsMenu;
                Debug.Log("_currentMenu = _optionsMenu");
                break;
            case InterfaceObject.VideoOptions:
                _currentMenu = _videoOptions;
                Debug.Log("_currentMenu = _videoOptions");
                break;
            case InterfaceObject.GameOptions:
                break;
            case InterfaceObject.AudioOptions:
                break;
            case InterfaceObject.TestMenu:
                _currentMenu = _testMenu;
                break;
            case InterfaceObject.MenuPause:
                break;
            default:
                break;
        }

        if (_currentMenu != null)
        {
            _currentMenu.Show();
            _interfaceObjects.Push(menuItem);
        }
    }

    public void ProgressBarSetValue(float value)
    {
        if (_progressBar == null) return;
        _progressBar.GetControl.value = value;
        _progressBar.GetText.text = $"{Math.Truncate(value * 100)}%";
    }

    public void ProgressBarEnabled()
    {
        if (_progressBar) return;
        _progressBar = Instantiate(InterfaceResources.ProgressbarPrefab, InterfaceResources.MainCanvas.transform);
        ProgressBarSetValue(0);
    }

    public void ProgressBarDisable()
    {
        if (!_progressBar) return;
        Destroy(_progressBar.Instance);
    }

    public void LoadSceneAsync(int lvl)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(lvl);
        StartCoroutine(LoadSceneAsync(async));
    }

    public void LoadSceneAsync(Scene lvl)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(lvl.buildIndex);
        StartCoroutine(LoadSceneAsync(async));
    }

    public void LoadSceneAsync(string lvl)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(lvl);
        StartCoroutine(LoadSceneAsync(async));
    }

    private IEnumerator LoadSceneAsync(AsyncOperation async)
    {
        ProgressBarEnabled();
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            ProgressBarSetValue(async.progress + 0.1f);
            float progress = async.progress * 100.0f;
            if (async.progress < 0.9f && Mathf.RoundToInt(progress) != 100)
            {
                async.allowSceneActivation = false;
            }
            else
            {
                if (async.allowSceneActivation) yield return null;
                async.allowSceneActivation = true;
                ProgressBarDisable();
            }
            yield return null;
        }
    }

    #endregion

}
