using System;
using UnityEngine;


public sealed class FlashlightModel : BaseObjectScene
{
    #region Fields

    public  float RotationSpeed = 11.0f;
    public float MaxBatteryCharge = 10.0f;
    public float Intensity = 10.0f;

    private Vector3 _vecOffset;
    private Light _light;
    private Transform _goFollow;
    private float _share;
    private float _takeAwayTheIntensity;

    #endregion


    #region Properties

    public float Charge => CurrentBatteryCharge / MaxBatteryCharge;

    public float CurrentBatteryCharge { get; private set; }

    #endregion


    #region UnityMethods

    protected override void Awake()
    {
        base.Awake();
        _light = GetComponent<Light>();
        _goFollow = Camera.main.transform;
        _vecOffset = Transform.position - _goFollow.position;
        CurrentBatteryCharge = MaxBatteryCharge;
        _light.intensity = Intensity;
        _share = MaxBatteryCharge / 4.0f;
        _takeAwayTheIntensity = Intensity / (MaxBatteryCharge * 100.0f);
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
                CustomDebug.Log("Типа включился");
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
        Transform.rotation = Quaternion.Lerp(Transform.rotation, _goFollow.rotation, RotationSpeed * Time.deltaTime);
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
        return CurrentBatteryCharge <= MaxBatteryCharge / 2.0f;
    }

    public bool RechargeBattery()
    {
        if (CurrentBatteryCharge < MaxBatteryCharge)
        {
            CurrentBatteryCharge += Time.deltaTime;
            return true;
        }
        return false;
    }

    #endregion
}
