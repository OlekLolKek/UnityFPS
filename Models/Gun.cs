using UnityEngine;


public class Gun : Weapon
{
    #region Methods

    public override void Fire()
    {
        if (!_isReady) return;
        if (Magazine.CountAmmunition <= 0) return;
        RotateBullet();
        var temAmmunition = Instantiate(Ammunition, _barrel.position, _bulletRotation);
        temAmmunition.AddForce(temAmmunition.transform.forward * _force);
        _audioSource.Play();
        Magazine.CountAmmunition--;
        _isReady = false;
        _timeRemaining.AddTimeRemaining();
        StartCoroutine(Flash());
        Debug.Log("Свет включился");
    }

    #endregion
}
