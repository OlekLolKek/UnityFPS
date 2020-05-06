using UnityEngine;

public class WeaponController : BaseController
{
    #region Fields

    private Weapon _weapon;

    #endregion


    #region Methods

    public override void On(params BaseObjectScene[] weapon)
    {
        if (IsActive) return;
        if (weapon.Length > 0) _weapon = weapon[0] as Weapon;
        if (_weapon == null) return;
        base.On(_weapon);
        _weapon.IsVisible = true;
        UIInterface.WeaponUIText.SetActive(true);
        UIInterface.WeaponUIText.ShowData(_weapon.Magazine.CountAmmunition, _weapon.CountMag);
    }

    public override void Off()
    {
        if (!IsActive) return;
        base.Off();
        _weapon.IsVisible = false;
        _weapon = null;
        UIInterface.WeaponUIText.SetActive(false);
    }

    public void Fire()
    {
        _weapon.Fire();
        UIInterface.WeaponUIText.ShowData(_weapon.Magazine.CountAmmunition, _weapon.CountMag);
    }

    public void ReloadMag()
    {
        _weapon.ReloadMag();
        UIInterface.WeaponUIText.ShowData(_weapon.Magazine.CountAmmunition, _weapon.CountMag);

    }

    #endregion
}
