using UnityEngine.UI;
using UnityEngine;


public class ToggleUI : MonoBehaviour, IControl
{
    #region Properties

    public Text GetText { get; private set; }   
    public Toggle GetControl { get; private set; }
    public GameObject Instance { get { return gameObject; } }
    public Selectable Control { get { return GetControl; } }

    #endregion


    #region UnityMethods

    private void Awake()
    {
        GetControl = GetComponent<Toggle>();
        GetText = transform.GetComponentInChildren<Text>();
    }

    #endregion


    #region Methods

    public void Interactable(bool value)
    {
        GetControl.interactable = value;
    }

    #endregion
}

