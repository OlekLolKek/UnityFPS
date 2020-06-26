using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;


public abstract class Weapon : BaseObjectScene
{
    #region Fields

    public Ammunition Ammunition;
    public Magazine Magazine;

    public AmmunitionType[] AmmunitionTypes = { AmmunitionType.Bullet };

    [SerializeField] private Quaternion _bulletRotationMin;
    [SerializeField] private Quaternion _bulletRotationMax;

    [SerializeField] protected AudioClipPlayable _shotClip;
    [SerializeField] protected Transform _barrel;
    [SerializeField] protected Light _muzzleFlash;
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected float _force = 999.0f;
    [SerializeField] protected float _muzzleFlashTime = 0.025f;
    [SerializeField] protected float _rechargeTime = 0.2f;
    [SerializeField] protected float _reloadTime = 1.0f;
    [SerializeField] protected int _magSize = 30;
    [SerializeField] protected int _countMag = 5;
    [SerializeField] protected bool _isSwitchable = true;
    [SerializeField] protected bool _isInAutomaticMode;

    private Queue<Magazine> _mags = new Queue<Magazine>();
    protected Quaternion _bulletRotation;

    protected bool _isReady = true;
    protected ITimeRemaining _timeRemaining;
    protected ITimeRemaining _reloadTimeRemaining;

    #endregion


    #region Properties

    public int CountMag => _mags.Count;

    public bool IsInAutomaticMode { get => _isInAutomaticMode; set => _isInAutomaticMode = value; }


    #endregion


    #region UnityMethods

    private void Start()
    {
        _timeRemaining = new TimeRemaining(ReadyShoot, _rechargeTime);
        _reloadTimeRemaining = new TimeRemaining(ReadyShoot, _reloadTime);
        _muzzleFlash = GetComponentInChildren<Light>();
        for (var i = 0; i < _countMag; i++)
        {
            AddMag(new Magazine { CountAmmunition = _magSize });
        }

        ReloadMag();
        _isReady = true;
    }

    public abstract void Fire();

    protected void ReadyShoot()
    {
        _isReady = true;
        if (_muzzleFlash != null)
        {
            _muzzleFlash.enabled = false;
            Debug.Log("Свет выключился");
        }
    }

    protected IEnumerator Flash()
    {
        _muzzleFlash.enabled = true;
        yield return new WaitForSeconds(_muzzleFlashTime);
        _muzzleFlash.enabled = false;
    }

    protected void AddMag(Magazine mag)
    {
        _mags.Enqueue(mag);
    }

    protected void RotateBullet()
    {
        _bulletRotation = new Quaternion(
            _barrel.transform.rotation.x + Random.Range(_bulletRotationMin.x, _bulletRotationMax.x),
            _barrel.transform.rotation.y + Random.Range(_bulletRotationMin.y, _bulletRotationMax.y),
            _barrel.transform.rotation.z + Random.Range(_bulletRotationMin.z, _bulletRotationMax.z),
            _barrel.transform.rotation.w + Random.Range(_bulletRotationMin.w, _bulletRotationMax.w));
    }

    public void ReloadMag()
    {
        if (CountMag <= 0) return;
        _isReady = false;
        _reloadTimeRemaining.AddTimeRemaining();
        Magazine = _mags.Dequeue();
    }

    public void SwitchMode()
    {
        if (!_isSwitchable) return;
        Camera.main.GetComponent<AudioSource>().Play();
        if (IsInAutomaticMode) IsInAutomaticMode = false;
        else IsInAutomaticMode = true;
    }

    #endregion
}
