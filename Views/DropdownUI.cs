using UnityEngine;
using UnityEngine.UI;


public class DropdownUI : MonoBehaviour, IControl
{
    #region Fields

    private Text _text;
    private Dropdown _control;

    #endregion


    #region Properties

    public Text GetText 
    {
        get
        {
            if (!_text)
            {
                _text = transform.GetComponentInChildren<Text>();
            }
            return _text;
        }
    }
    public Dropdown GetControl
    {
        get
        {
            if (!_control)
            {
                _control = transform.GetComponentInChildren<Dropdown>();
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
