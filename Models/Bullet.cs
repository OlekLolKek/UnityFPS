using UnityEngine;


public sealed class Bullet : Ammunition
{
    #region UnityMethods

    private void OnCollisionEnter(Collision collision)
    {
        //Дописать доп. урон
        var setDamage = collision.gameObject.GetComponent<ICollision>();

        if (setDamage != null)
        {
            setDamage.OnCollision(new InfoCollision(_currentDamage, collision.contacts[0], collision.transform, Rigidbody.velocity));
        }

        DestroyAmmunition();
    }

    #endregion
}
