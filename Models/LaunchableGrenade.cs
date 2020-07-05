using UnityEngine;


public sealed class LaunchableGrenade : Ammunition
{
    #region Fields

    [SerializeField] private AudioClip _clip;
    [SerializeField] private float _radius = 5;
    [SerializeField] private float _force = 1500;
    [SerializeField] private AudioSource _explosionAudioSource;

    #endregion


    #region UnityMethods

    private void OnCollisionEnter(Collision collision)
    {
        var setDamage = collision.gameObject.GetComponent<ICollision>();

        Explode(collision);

        DestroyAmmunition();
    }

    #endregion


    #region Methods

    public void Explode(Collision collision)
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
                    tempRigidbody.gameObject.GetComponent<ICollision>().OnCollision(new InfoCollision(_currentDamage, collision.contacts[0], collision.transform, tempRigidbody.velocity));
                }
                var tempDamage = tempRigidbody.gameObject.GetComponent<IDamageble>();
                if (tempDamage != null)
                {
                    Debug.Log("Есть Damageble");
                    tempRigidbody.gameObject.GetComponent<IDamageble>().Damage(new InfoCollision(_currentDamage, collision.contacts[0], collision.transform, tempRigidbody.velocity));
                    Debug.Log(tempRigidbody.gameObject.name);
                }
                tempRigidbody.AddExplosionForce(_force, transform.position, _radius, 0.0f);
            }
        }
        AudioSource.PlayClipAtPoint(_clip, transform.position);
        //_explosionAudioSource.Play();
        //_explosionAudioSource.PlayOneShot(_clip);
        Destroy(gameObject);
    }

    #endregion

}
