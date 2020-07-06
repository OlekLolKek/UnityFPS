using UnityEngine;
using Object = UnityEngine.Object;
using System;

public abstract class BaseMenu : MonoBehaviour
{
    #region Fields

    protected IControl[] _elementsOfInterface;

    public bool _isShown { get; set; }
    
    protected Interface _interface;

    #endregion


    #region UnityMethods

    protected virtual void Awake()
    {
        _interface = FindObjectOfType<Interface>();
    }

    #endregion


    #region Methods

    protected void Clear(IControl[] controls)
    {
        foreach (var t in controls)
        {
            if (t == null) continue;
            Destroy(t.Instance);
        }
    }

    protected T CreateControl<T>(T prefab, string text) where T : Object, IControlText
    {
        if (!prefab) throw new Exception(string.Format("Отсутствует ссылка на {0}", typeof(T)));
        var tempControl = Instantiate(prefab, _interface.InterfaceResources.MainPanel.transform.position, Quaternion.identity,
            _interface.InterfaceResources.MainPanel.transform);
    }

    public abstract void Hide();
    public abstract void Show();

    #endregion
}
