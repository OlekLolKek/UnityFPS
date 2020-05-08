﻿using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : BaseObjectScene
{
    #region Fields

    public Ammunition Ammunition;
    public Magazine Magazine;

    public AmmunitionType[] AmmunitionTypes = { AmmunitionType.Bullet };

    [SerializeField] protected Transform _barrel;
    [SerializeField] protected float _force = 999.0f;
    [SerializeField] protected float _rechargeTime = 0.2f;
    [SerializeField] private int _magSize = 30;
    [SerializeField] private int _countMag = 5;

    private bool _isInAutomaticMode;

    private Queue<Magazine> _mags = new Queue<Magazine>();

    protected bool _isReady = true;
    protected ITimeRemaining _timeRemaining;

    #endregion


    #region Properties

    public int CountMag => _mags.Count;

    public bool IsInAutomaticMode => _isInAutomaticMode;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _timeRemaining = new TimeRemaining(ReadyShoot, _rechargeTime);
        for (var i = 0; i < _countMag; i++)
        {
            AddMag(new Magazine { CountAmmunition = _magSize });
        }

        ReloadMag();
    }

    public abstract void Fire();

    protected void ReadyShoot()
    {
        _isReady = true;
    }

    protected void AddMag(Magazine mag)
    {
        _mags.Enqueue(mag);
    }

    public void ReloadMag()
    {
        if (CountMag <= 0) return;
        Magazine = _mags.Dequeue();
    }
    
    public void SwitchMode()
    {
        if (_isInAutomaticMode) _isInAutomaticMode = false;
        else _isInAutomaticMode = true;
    }

    #endregion
}
