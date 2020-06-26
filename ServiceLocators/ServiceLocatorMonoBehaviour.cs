using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


public static class ServiceLocatorMonoBehaviour
{
    #region Fields

    private static Dictionary<Type, Object> _servicecontainer = null;

    #endregion


    #region Methods

    public static T GetService<T>(bool createObjectIfNotFound = true) where T : Object
    {
        if (_servicecontainer == null)
        {
            _servicecontainer = new Dictionary<Type, Object>();
        }

        if (!_servicecontainer.ContainsKey(typeof(T)))
        {
            return FindService<T>(createObjectIfNotFound);
        }

        var service = (T)_servicecontainer[typeof(T)];
        if (service != null)
        {
            return service;
        }

        _servicecontainer.Remove(typeof(T));
        return FindService<T>(createObjectIfNotFound);
    }

    private static T FindService<T>(bool createObjectIfNotFound = true) where T : Object
    {
        T type = Object.FindObjectOfType<T>();
        if (type != null)
        {
            _servicecontainer.Add(typeof(T), type);
        }
        else if (createObjectIfNotFound)
        {
            var go = new GameObject(typeof(T).Name, typeof(T));
            _servicecontainer.Add(typeof(T), go.GetComponent<T>());
        }
        return (T)_servicecontainer[typeof(T)];
    }

    public static void Cleanup()
    {
        var objects = _servicecontainer.Values.ToList();
        foreach (var t in objects)
        {
           Object.Destroy(t);
        }

        _servicecontainer.Clear();
    }

    #endregion
}
