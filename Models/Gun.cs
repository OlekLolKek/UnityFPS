public class Gun : Weapon
{
    #region Methods

    public override void Fire()
    {
        if (!_isReady) return;
        if (Magazine.CountAmmunition <= 0) return;
        var temAmmunition = Instantiate(Ammunition, _barrel.position, _barrel.rotation);
        temAmmunition.AddForce(_barrel.forward * _force);
        _audioSource.Play();
        Magazine.CountAmmunition--;
        _isReady = false;
        _timeRemaining.AddTimeRemaining();
    }

    #endregion
}
