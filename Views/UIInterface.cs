using UnityEngine;


public sealed class UIInterface
{
    #region Fields

    private FlashlightUIText _flashlightUIText;
    private FlashlightUIBar _flashlightUIBar;
    private WeaponUIText _weaponUIText;
    private SelectionObjMessageUI _selectionObjMessageUI;

    #endregion


    #region Properties

    public FlashlightUIText LightUIText
    {
        get
        {
            if (!_flashlightUIText)
            {
                _flashlightUIText = Object.FindObjectOfType<FlashlightUIText>();
            }
            return _flashlightUIText;
        }
    }

    public FlashlightUIBar FlashlightUIBar
    {
        get
        {
            if (!_flashlightUIBar)
            {
                _flashlightUIBar = Object.FindObjectOfType<FlashlightUIBar>();
            }
            return _flashlightUIBar;
        }
    }

    public WeaponUIText WeaponUIText
    {
        get
        {
            if (!_weaponUIText)
            {
                _weaponUIText = Object.FindObjectOfType<WeaponUIText>();
            }
            return _weaponUIText;
        }
    }

    public SelectionObjMessageUI SelectionObjMessageUI
    {
        get
        {
            if (!_selectionObjMessageUI)
            {
                _selectionObjMessageUI = Object.FindObjectOfType<SelectionObjMessageUI>();
            }
            return _selectionObjMessageUI;
        }
    }

    #endregion
}
