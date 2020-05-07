using UnityEngine;


public sealed class InputController : BaseController, IExecute
{
    #region Fields

    private KeyCode _switchFlashlight = KeyCode.F;
    private KeyCode _cancel = KeyCode.Escape;
    private KeyCode _reloadMag = KeyCode.R;
    private int _mouseButton = (int)MouseButton.LeftButton;

    #endregion


    #region ClassLyfeCycles

    public InputController()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    #endregion


    #region Methods

    public void Execute()
    {
        if (!IsActive) return;
        if (Input.GetKeyDown(_switchFlashlight))
        {
            ServiceLocator.Resolve<FlashlightController>().Switch(ServiceLocator.Resolve<Inventory>().Flashlight);
        }
        //todo реализовать выбор оружия по колесу мыши

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(0);
        }

        if (Input.GetMouseButton(_mouseButton))
        {
            if (ServiceLocator.Resolve<WeaponController>().IsActive)
            {
                ServiceLocator.Resolve<WeaponController>().Fire();
            }
        }

        if (Input.GetKeyDown(_cancel))
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            ServiceLocator.Resolve<FlashlightController>().Off();
        }

        if (Input.GetKeyDown(_reloadMag))
        {
            if (ServiceLocator.Resolve<WeaponController>().IsActive)
            {
                ServiceLocator.Resolve<WeaponController>().ReloadMag();
            }
        }
    }


    private void SelectWeapon(int i)
    {
        ServiceLocator.Resolve<WeaponController>().Off();
        var tempWeapon = ServiceLocator.Resolve<Inventory>().Weapons[i]; //todo инкапсулировать
        if (tempWeapon != null)
        {
            ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
        }
    }

    #endregion
}
