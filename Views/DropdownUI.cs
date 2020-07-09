using UnityEngine;
using UnityEngine.UI;


public class DropdownUI : MonoBehaviour, IControl
{
    #region Properties

    public Text GetText { get; private set; }
    public Dropdown GetControl { get; private set; }
    public GameObject Instance { get { return gameObject; } }
    public Selectable Control { get { return GetControl; } }

    #endregion


    #region UnityMethods

    private void Awake()
    {
        GetText = transform.GetComponentInChildren<Text>();
        GetControl = transform.GetComponentInChildren<Dropdown>();
    }

    public void Interactable(bool value)
    {
        GetControl.interactable = value;
    }

    #endregion
}
