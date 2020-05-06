using UnityEngine;


public sealed class Inventory : IInitialization
{
    #region Fields

    private Weapon[] _weapons = new Weapon[5];

    #endregion


    #region Properties

    public Weapon[] Weapons => _weapons;

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

    #endregion
}
