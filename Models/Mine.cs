using UnityEngine;


public class Mine : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioClip _clip;
    [SerializeField] private float _radius = 5;
    [SerializeField] private float _force = 1500;

    private int _dmg = 50;

    #endregion


    #region UnityMethods

    private void Start()
    {
        Debug.Log("Старт");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Триггер");
        if (other.gameObject.GetComponent<IDamageble>() != null || other.gameObject.GetComponent<ICollision>() != null)
        {
            Debug.Log("Триггер с игроком/ботом");
            Explode(other);
        }
    }

    #endregion


    #region Methods

    public void Explode(Collider collider)
    {
        var colliders = new Collider[100];
        Physics.OverlapSphereNonAlloc(transform.position, 5, colliders);
        for (int i = 0; i < colliders.Length; i++)
        {
            var tempRigidbody = colliders[i]?.GetComponent<Rigidbody>();

            if (tempRigidbody)
            {
                Debug.Log("Есть RB");
                Debug.Log(tempRigidbody.gameObject.name);

                var tempCollision = tempRigidbody.gameObject.GetComponent<ICollision>();

                if (tempCollision != null)
                {
                    Debug.Log("Есть ICollision");
                    Debug.Log(tempRigidbody.gameObject.name);
                    tempRigidbody.gameObject.GetComponent<ICollision>().OnCollision(new InfoCollision(_dmg, /*collider.transform.position,*/ collider.transform, tempRigidbody.velocity));
                }
                var tempDamage = tempRigidbody.gameObject.GetComponent<IDamageble>();
                if (tempDamage != null)
                {
                    Debug.Log("Есть Damageble");
                    tempRigidbody.gameObject.GetComponent<IDamageble>().Damage(new InfoCollision(_dmg, /*collider.transform.position,*/ collider.transform, tempRigidbody.velocity));
                    Debug.Log(tempRigidbody.gameObject.name);
                }
                tempRigidbody.AddExplosionForce(_force, transform.position, _radius, 0.0f);
            }
        }
        AudioSource.PlayClipAtPoint(_clip, transform.position);
        Destroy(gameObject);
    }

    #endregion
}
