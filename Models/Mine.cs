using UnityEngine;


public class Mine : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioClip _clip;

    private int _dmg = 999;

    #endregion


    #region UnityMethods

    private void Start()
    {
        Debug.Log("Старт");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Триггер");
        if (other.gameObject.GetComponent<CharacterController>() || /*other.gameObject.CompareTag("Bot") ||*/ other.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Триггер с игроком/ботом");
            Explode(other);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Коллизия");
    //    if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bot") || collision.gameObject.CompareTag("Projectile"))
    //    {
    //        Debug.Log("Коллизия с игроком/ботом");
    //        Explode(collision);
    //    }
    //}

    #endregion


    #region Methods

    //public void Explode(Collision collision)
    //{
    //    var colliders = new Collider[100];
    //    Physics.OverlapSphereNonAlloc(transform.position, 1500, colliders);
    //    for (int i = 0; i < colliders.Length; i++)
    //    {
    //        var tempRigidbody = colliders[i]?.GetComponent<Rigidbody>();
    //        if (!tempRigidbody) continue;
    //        if (colliders[i].gameObject.GetComponent<ICollision>() != null)
    //        {
    //            Debug.Log("Есть ICollision");
    //            collision.gameObject.GetComponent<ICollision>().OnCollision(new InfoCollision(_dmg, collision.contacts[0], collision.transform, gameObject.GetComponent<Rigidbody>().velocity));
    //        }
    //        tempRigidbody.useGravity = true;
    //        tempRigidbody.isKinematic = false;
    //        tempRigidbody.AddExplosionForce(1500, transform.position, 5, 0.0f);
    //    }
    //    AudioSource.PlayClipAtPoint(_clip, transform.position);
    //    Destroy(gameObject);
    //}

    public void Explode(Collider collider)
    {
        var colliders = new Collider[100];
        Physics.OverlapSphereNonAlloc(transform.position, 1500, colliders);
        for (int i = 0; i < colliders.Length; i++)
        {
            var tempRigidbody = colliders[i]?.GetComponent<Rigidbody>();
            if (!tempRigidbody) continue;
            if (tempRigidbody.gameObject.GetComponent<ICollision>() != null)
            {
                Debug.Log("Есть ICollision");
                if (!tempRigidbody.gameObject.GetComponent<Mine>())
                {
                    Debug.Log("Это не мина");
                    tempRigidbody.gameObject.GetComponent<ICollision>().OnCollision(new InfoCollision(_dmg, /*collider.transform.position,*/ collider.transform, gameObject.GetComponent<Rigidbody>().velocity));
                }
            }
            tempRigidbody.useGravity = true;
            tempRigidbody.isKinematic = false;
            tempRigidbody.AddExplosionForce(1500, transform.position, 5, 0.0f);
        }
        AudioSource.PlayClipAtPoint(_clip, transform.position);
        Destroy(gameObject);
    }

    #endregion
}
