public class GrenadeLauncher : Weapon
{
    #region Methods

    public override void Fire()
    {
        if (!_isReady) return;
        if (Magazine.CountAmmunition <= 0) return;
        var temAmmunition = Instantiate(Ammunition, _barrel.position, _barrel.rotation);
        temAmmunition.AddForce(temAmmunition.transform.forward * _force);
        _audioSource.Play();
        Magazine.CountAmmunition--;
        _isReady = false;
        _timeRemaining.AddTimeRemaining();
    }

    #endregion
}
