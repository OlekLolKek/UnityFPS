using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class OptionsMenu : BaseMenu
{
    #region Methods

    private void LoadVideoOptions()
    {
        _interface.Execute(InterfaceObject.VideoOptions);
    }

    private void LoadSoundOptions()
    {
        _interface.Execute(InterfaceObject.AudioOptions);
    }

    private void LoadGameOptions()
    {
        _interface.Execute(InterfaceObject.GameOptions);
    }

    private void Back()
    {
        _interface.Execute(InterfaceObject.MainMenu);
    }

    public override void Hide()
    {
        if (!_isShown) return;
        _isShown = false;
    }

    public override void Show()
    {
        if (_isShown) return;
        _isShown = true;
    }

    #endregion
}
