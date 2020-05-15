using UnityEngine;
using System;


public class Target : MonoBehaviour, ICollision, ISelectObj
{
    #region Fields

    public event Action OnPointChange = delegate { };

    public float HP = 100;
    private bool _isDead;
    private float _timeToDestroy = 10.0f;
    //todo дописать поглощение урона

    #endregion


    #region Methods

    public void CollisionEnter(InfoCollision info)
    {
        if (_isDead) return;
        if (HP > 0)
        {
            HP -= info.Damage;
        }

        if (HP <= 0)
        {
            if (!TryGetComponent<Rigidbody>(out _))
            {
                gameObject.AddComponent<Rigidbody>();
            }
            Destroy(gameObject, _timeToDestroy);

            OnPointChange.Invoke();
            _isDead = true;
        }
    }

    public string GetMessage()
    {
        return $"{gameObject.name} - {HP}";
    }

    #endregion
}
