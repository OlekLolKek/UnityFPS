﻿using System;
using System.Collections;
using System.IO;
using UnityEngine;


public class PhotoController
{
    #region Fields

    private bool _isProcessed;
    private readonly string _path;
    private int _layers;
    private int _resolution;

    #endregion


    #region ClassLifeCycles

    public PhotoController()
    {
        _path = Application.dataPath;
    }

    #endregion


    #region Methods

    private IEnumerator DoTapExampleAsync()
    {
        _isProcessed = true;
        Camera.main.cullingMask = ~(1 << _layers);
        var sw = Screen.width;
        var sh = Screen.height;
        yield return new WaitForEndOfFrame();
        var sc = new Texture2D(sw, sh, TextureFormat.RGB24, true);
        sc.ReadPixels(new Rect(0, 0, sw, sh), 0, 0);
        var bytes = sc.EncodeToPNG();
        var filename = String.Format("{0:ddMMyyyy_HHmmssfff}.png", DateTime.Now);
        File.WriteAllBytes(Path.Combine(_path, filename), bytes);
        yield return new WaitForSeconds(2.3f);
        Camera.main.cullingMask |= 1 << _layers;
        _isProcessed = false;
    }

    public void FirstMethod()
    {
        var filename = string.Format("{0:ddMMyyyy_HHmmssfff}.png", DateTime.Now);
        ScreenCapture.CaptureScreenshot(Path.Combine(_path, filename), _resolution);
    }

    public void SecondMethod()
    {
        DoTapExampleAsync().StartCoroutine();
    }

    #endregion
}
