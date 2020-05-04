using UnityEngine;


public sealed class FlashlightController : BaseController, IExecute, IInitialization
{
    #region Fields

    private FlashlightModel _flashlightModel;

    #endregion


    #region Methods

    public void Initialization()
    {
        UIInterface.LightUIText.SetActive(false);
        UIInterface.FlashlightUIBar.SetActive(false);
    }

    public override void On()
    {
        if (IsActive)
        { 
            return; 
        }
        if (flaslight.Length > 0)
        {
            _flashlightModel = flashlight[0] as FlashlightModel;
        }
        if (_flashlightModel.CurrentBatteryCharge <= 0)
        { 
            return; 
        }
        base.On(FlashlightModel);
        _flashlightModel.Switch(FlashlightActiveType.On);
        UIInterface.LightUIText.SetActive(true);
        UIInterface.FlashlightUIBar.SetActive(true);
        UIInterface.FlashlightUIBar.SetColor(Color.green);
    }

    public override void Off()
    {
        if (!IsActive) 
        { 
            return; 
        }
        base.Off();
        _flashlightModel.Switch(FlashlightActiveType.Off);
        UIInterface.FlashlightUIBar.SetActive(false);
        UIInterface.LightUIText.SetActive(false);
    }

    public void Execute()
    {
        _flashlightUI.Text = _flashlightModel.CurrentBatteryCharge;

        if (!IsActive)
        {
            return;
        }
        if (_flashlightModel.EditBatteryCharge())
        {
            UIInterface.LightUIText.Text = _flashlightModel.CurrentBatteryCharge;
            UIInterface.FlashlightUIBar.Fill = _flashlightModel.Charge;
            _flashlightModel.Rotation();

            if (_flashlightModel.LowBattery())
            {
                UIInterface.FlashlightUIBar.SetColor(Color.red);
            }
        }
        else
        {
            Off();
        }
    }

    #endregion
}
