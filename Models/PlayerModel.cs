using TMPro;
using UnityEngine;


public class PlayerModel : BaseObjectScene, IDamageble
{
    #region Fields

    [SerializeField] private float _HP;
    [SerializeField] private float _maxHP;
    [SerializeField] private float _healthDivider;
    private Vector3 _startPosition;

    #endregion


    #region UnityMethods

    protected override void Awake()
    {
        base.Awake();
        _startPosition = transform.position;
    }

    #endregion


    #region Methods

    public void Damage(InfoCollision info)
    {
        if (_HP > 0)
        {
            _HP -= info.Damage / _healthDivider;

            if (_HP <= 0)
            {
                Revive();
            }
            Debug.Log("Игрок получил урон");
        }
        else Revive();
    }

    private void Revive()
    {
        transform.position = _startPosition;
        _HP = _maxHP;
    }

    //public bool EditHP()
    //{
    //    if (_HP > 0)
    //    {
    //        _HP -= Time.deltaTime;

    //        if (_HP < _share)
    //        {
    //            _light.enabled = UnityEngine.Random.Range(0, 100) >= UnityEngine.Random.Range(0, 10);
    //        }
    //        else
    //        {
    //            _light.intensity -= _takeAwayTheIntensity;
    //        }
    //        return true;
    //    }

    //    return false;
    //}

    #endregion
}
