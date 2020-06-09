using UnityEngine;
using UnityEngine.UI;


public class PlayerModel : BaseObjectScene, IDamageble
{
    #region Fields

    [SerializeField] private float _HP;
    [SerializeField] private float _maxHP;
    [SerializeField] private float _healthDivider;
    [SerializeField] private Image _hpBar;
    [SerializeField] private Text _hpText;
    [SerializeField] private Transform _player;
    private Vector3 _startPosition;

    #endregion


    #region UnityMethods

    protected override void Awake()
    {
        base.Awake();
        _hpText.text = $"{_HP}";
        _startPosition = new Vector3(0, 1, -7);
    }

    #endregion


    #region Methods

    public void Damage(InfoCollision info)
    {
        if (_HP > 0)
        {
            _HP -= info.Damage / _healthDivider;
            _hpBar.fillAmount = _HP / 100;
            _hpText.text = _HP.ToString();

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
        _player.position = _startPosition;
        _HP = _maxHP;
        _hpBar.fillAmount = _HP / 100;
        _hpText.text = _HP.ToString();
        Debug.Log("Возрождение");
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
