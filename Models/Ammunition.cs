using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ammunition : BaseObjectScene
{
    #region Fields

    public AmmunitionType Type = AmmunitionType.Bullet;

    [SerializeField] private float _timeToDestruct = 10.0f;
    [SerializeField] private float _baseDamage = 10.0f;
    protected float _currentDamage; //todo доделать свой урон
    private float _lossOfDamageAtTime = 0.2f;
    private ITimeRemaining _timeRemaining;

    #endregion


    #region UnityMethods

    protected override void Awake()
    {
        base.Awake();
        _currentDamage = _baseDamage;
    }

    private void Start()
    {
        Destroy(gameObject, _timeToDestruct);
        _timeRemaining = new TimeRemaining(LossOfDamage, 1.0f, true);
        _timeRemaining.AddTimeRemaining();
    }

    #endregion


    #region Methods

    public void AddForce(Vector3 dir)
    {
        if (!Rigidbody) return;
        Rigidbody.AddForce(dir);
    }

    private void LossOfDamage()
    {
        _currentDamage -= _lossOfDamageAtTime;
    }

    protected void DestroyAmmunition()
    {
        Destroy(gameObject);
        _timeRemaining.RemoveTimeRemaining();
    }

    #endregion
}
