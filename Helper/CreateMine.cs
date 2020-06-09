using System;
using UnityEngine;


public class CreateMine : MonoBehaviour
{
    #region Fields

    [SerializeField] private Mine _prefab;
    private GameObject _mainMine;

    #endregion


    #region Methods

    public void InstantiateObj(Vector3 pos)
    {
        if (!_mainMine)
        {
            _mainMine = new GameObject("Mine");
        }

        if (_prefab != null)
        {
            Instantiate(_prefab, pos, Quaternion.identity, _mainMine.transform);
        }
        else
        {
            throw new Exception($"Нет префаба на компоненте {typeof(CreateMine)} {gameObject.name}");
        }
    }

    #endregion
}

