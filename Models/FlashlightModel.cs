using System;
using UnityEngine;


public sealed class FlashlightModel : BaseObjectScene
{
    #region Fields

    [SerializeField] private float _rotationSpeed = 11.0f;
    [SerializeField] private float _maxBatteryCharge = 10.0f;
    [SerializeField] private float _intensity = 10.0f;

    private Vector3 _vecOffset;
    private Light _light;
    private Transform _goFollow;
    private float _share;
    private float _takeAwayTheIntensity;

    #endregion


    #region Properties

    public float Charge => CurrentBatteryCharge / _maxBatteryCharge;

    public float CurrentBatteryCharge { get; private set; }

    #endregion


    #region UnityMethods

    protected override void Awake()
    {
        base.Awake();
        _light = GetComponent<Light>();
        _goFollow = Camera.main.transform;
        _vecOffset = Transform.position - _goFollow.position;
        CurrentBatteryCharge = _maxBatteryCharge;
        _light.intensity = _intensity;
        _share = _maxBatteryCharge / 4.0f;
        _takeAwayTheIntensity = _intensity / (_maxBatteryCharge * 100.0f);
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
            case FlashlightActiveType.None:
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
        if (CurrentBatteryCharge > 0)
        {
            CurrentBatteryCharge -= Time.deltaTime;

            if (CurrentBatteryCharge < _share)
            {
                _light.enabled = UnityEngine.Random.Range(0, 100) >= UnityEngine.Random.Range(0, 10);
            }
            else
            {
                _light.intensity -= _takeAwayTheIntensity;
            }
            return true;
        }

        return false;
    }

    public bool LowBattery()
    {
        return CurrentBatteryCharge <= _maxBatteryCharge / 2.0f;
    }

    public bool RechargeBattery()
    {
        if (CurrentBatteryCharge < _maxBatteryCharge)
        {
            CurrentBatteryCharge += Time.deltaTime;
            return true;
        }
        return false;
    }

    #endregion
}
