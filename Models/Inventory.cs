using UnityEngine;


public sealed class Inventory : IInitialization
{
    #region Fields

    private Weapon[] _weapons = new Weapon[5];

    private int _activeWeapon;

    #endregion


    #region Properties

    public Weapon[] Weapons => _weapons;

    public int ActiveWeapon { get => _activeWeapon; set => _activeWeapon = value; }

    public FlashlightModel Flashlight { get; private set; }

    #endregion


    #region Methods

    public void Initialization()
    {
        _weapons = ServiceLocatorMonoBehaviour.GetService<CharacterController>().GetComponentsInChildren<Weapon>();

        foreach (var weapon in Weapons)
        {
            weapon.IsVisible = false;
        }

        Flashlight = Object.FindObjectOfType<FlashlightModel>();
        Flashlight.Switch(FlashlightActiveType.Off);
    }

    //todo добавить функционал

    public void RemoveWeapon(Weapon weapon)
    {

    }

    public void NextWeapon()
    {
        if (ActiveWeapon == _weapons.Length - 1) ActiveWeapon = 0;
        else ActiveWeapon++;
    }

    public void PreviousWeapon()
    {
        if (ActiveWeapon == 0) ActiveWeapon = _weapons.Length - 1;
        else ActiveWeapon--;
    }
    #endregion
}
