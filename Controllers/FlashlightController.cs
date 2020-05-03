using UnityEngine;


public sealed class FlashlightController : BaseController, IExecute, IInitialization
{
    #region Fields

    private FlashlightModel _flashlightModel;
    private FlashlightUI _flashlightUI;

    #endregion


    #region Methods

    public void Initialization()
    {
        _flashlightModel = Object.FindObjectOfType<FlashlightModel>();
        _flashlightUI = Object.FindObjectOfType<FlashlightUI>();
        _flashlightUI.Text = _flashlightModel.BatteryChargeCurrent;
    }

    public override void On()
    {
        if (IsActive) return;
        if (_flashlightModel.BatteryChargeCurrent <= 0) return;
        base.On();
        _flashlightModel.Switch(FlashlightActiveType.On);
    }

    public override void Off()
    {
        if (!IsActive) return;
        base.Off();
        _flashlightModel.Switch(FlashlightActiveType.Off);
    }

    public void Execute()
    {
        _flashlightUI.Text = _flashlightModel.BatteryChargeCurrent;

        if (!IsActive)
        {
            if (!_flashlightModel.EditBatteryCharge())
            {
                Off();
            }
        }

        _flashlightModel.Rotation();
        _flashlightModel.EditBatteryCharge();
    }

    #endregion
}
