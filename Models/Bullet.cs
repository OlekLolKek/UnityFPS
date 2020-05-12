﻿using UnityEngine;


public sealed class Bullet : Ammunition
{
    #region UnityMethods

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        //Дописать доп. урон
        var setDamage = collision.gameObject.GetComponent<ICollision>();

        if (setDamage != null)
        {
            setDamage.OnCollision(new InfoCollision(_currentDamage, Rigidbody.velocity));
        }

        DestroyAmmunition();
    }

    #endregion
}
