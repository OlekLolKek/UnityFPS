using UnityEngine;


public sealed class InputController : BaseController, IExecute
{
    #region Fields

    private KeyCode _switchFlashlight = KeyCode.F;

    #endregion


    #region Methods

    public void Execute()
    {
        if (!IsActive) return;
        if (Input.GetKeyDown(_switchFlashlight))
        {
            ServiceLocator.Resolve<FlashlightController>().Switch();
        }
    }

    #endregion
}
