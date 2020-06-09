using UnityEngine;
using System;
using TMPro;

public class Target : MonoBehaviour, ICollision, ISelectObj
{
    #region Fields

    public event Action OnPointChange = delegate { };

    public float HP = 100;

    [SerializeField] private float _healthDivider = 1;
    
    private bool _isDead;
    private float _timeToDestroy = 10.0f;

    #endregion


    #region Methods

    public void OnCollision(InfoCollision info)
    {
        if (_isDead) return;
        if (HP > 0)
        {
            HP -= info.Damage / _healthDivider;
        }

        if (HP <= 0)
        {
            if (!TryGetComponent<Rigidbody>(out _))
            {
                gameObject.AddComponent<Rigidbody>();
            }
            var rb = gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
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
