using UnityEngine.UI;
using UnityEngine;


public class ToggleUI : MonoBehaviour, IControl
{
    #region Fields

    private Text _text;
    private Toggle _control;

    #endregion


    #region Properties

    public Text GetText 
    { 
        get
        {
            if (!_text)
            {
                _text = GetComponentInChildren<Text>();
            }
            return _text;
        }
    } 
    public Toggle GetControl 
    { 
        get
        {
            if (!_control)
            {
                _control = GetComponentInChildren<Toggle>();
            }
            return _control;
        }
    }
    public GameObject Instance => gameObject;
    public Selectable Control => GetControl;

    #endregion


    #region Methods

    public void Interactable(bool value)
    {
        GetControl.interactable = value;
    }

    #endregion
}

