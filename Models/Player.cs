using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseObjectScene, IDamageble
{
    #region Fields

    [SerializeField] private float HP;

    #endregion


    #region Methods

    public void Damage(InfoCollision info)
    {
        HP -= info.Damage;
        Debug.Log("Игрок получил урон");
    }

    #endregion
}
