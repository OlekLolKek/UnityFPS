using UnityEngine;


public abstract class BaseMenu : MonoBehaviour
{
    #region Fields

    protected bool _isShown { get; set; }
    protected Interface _interface;

    #endregion


    #region UnityMethods

    protected virtual void Awake()
    {
        _interface = FindObjectOfType<Interface>();
    }

    #endregion


    #region Methods

    public abstract void Hide();
    public abstract void Show();

    #endregion
}
