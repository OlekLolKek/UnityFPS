using System;
using UnityEngine;

public sealed class FlashlightModel : BaseObjectScene
{
    #region PrivateData

    private Vector3 _vecOffset;

    #endregion


    #region Fields

    public float BatteryChargeCurrent { get; private set; }

    [SerializeField] private float _rotationSpeed = 11;
    [SerializeField] private float _batteryChargeMax = 10;

    private Light _light;
    private Transform _goFollow;

    #endregion


    #region UnityMethods

    protected override void Awake()
    {
        base.Awake();
        _light = GetComponent<Light>();
        _goFollow = Camera.main.transform;
        _vecOffset = Transform.position - _goFollow.position;
        BatteryChargeCurrent = _batteryChargeMax;
    }

    #endregion


    #region Methods

    public void Switch(FlashlightActiveType value)
    {
        switch (value)
        {
            case FlashlightActiveType.On:
                _light.enabled = true;
                Transform.position = _goFollow.position + _vecOffset;
                Transform.rotation = _goFollow.rotation;
                break;
            case FlashlightActiveType.Off:
                _light.enabled = false;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(value), value, message: null);
        }
    }

    public void Rotation()
    {
        Transform.position = _goFollow.position + _vecOffset;
        Transform.rotation = Quaternion.Lerp(Transform.rotation, _goFollow.rotation, _rotationSpeed * Time.deltaTime);
    }

    public bool EditBatteryCharge()
    {
        if (BatteryChargeCurrent > 0 && _light.enabled)
        {
            BatteryChargeCurrent -= Time.deltaTime;
            return true;
        }
        else if (!_light.enabled && BatteryChargeCurrent < _batteryChargeMax)
        {
            BatteryChargeCurrent += Time.deltaTime / 3;
            return false;
        }
        else return false;
    }

    #endregion
}
