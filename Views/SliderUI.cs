using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour, IControl
{
    #region Fields

    private Text _text;
    private Slider _control;

    #endregion


    #region Properties

    public Text GetText => _text;
    public Slider GetControl => _control;
    public GameObject Instance => gameObject;
    public Selectable Control => GetControl;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        _control = transform.GetComponentInChildren<Slider>();
        _text = transform.GetComponentInChildren<Text>();
    }

    #endregion


    #region Methods

    public void Interactible(bool value)
    {
        GetControl.interactable = value;
    }

    #endregion
}
