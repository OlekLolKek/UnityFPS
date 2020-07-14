using UnityEngine;
using UnityEngine.UI;


public class ButtonUI : MonoBehaviour, IControlText
{
    #region Fields

    private Text _text;
    private Button _control;

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

    public Button GetControl
    {
        get
        {
            if (!_control)
            {
                _control = transform.GetComponentInChildren<Button>();
            }
            return _control;
        }
    }

    public GameObject Instance => gameObject;
    public Selectable Control => GetControl;

    #endregion


    #region Methods

    public void SetInteractible(bool value)
    {
        GetControl.interactable = value;
    }

    #endregion
}