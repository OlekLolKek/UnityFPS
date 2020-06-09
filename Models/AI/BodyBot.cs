using UnityEngine;
using System;


public class BodyBot : MonoBehaviour, ICollision
{
    #region Fields

    public event Action<InfoCollision> OnApplyDamageChange;

    #endregion


    #region Methods

    public void OnCollision(InfoCollision info)
    {
        OnApplyDamageChange?.Invoke(info);
        Debug.Log("В тело");
    }

    #endregion
}
