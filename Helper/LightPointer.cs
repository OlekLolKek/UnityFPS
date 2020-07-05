using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPointer : MonoBehaviour
{
    #region Fields

    [SerializeField] private Light _mouseLight;

    #endregion


    #region UnityMethods

    private void Update()
    {
        LightMouse();
    }

    #endregion


    #region Methods

    public void LightMouse()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit))
        {

            if (_mouseLight != null)
            {
                _mouseLight.transform.position = hit.point;
            }
            else
            {
                throw new Exception($"Нет префаба на компоненте {typeof(CreateMine)} {gameObject.name}");
            }
        }
    }

    #endregion
}
