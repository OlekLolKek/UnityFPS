using UnityEngine;


public class Mine : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioClip _clip;

    private int _dmg = 10;

    #endregion


    #region UnityMethods

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Explode(collision);
        }
    }

    #endregion


    #region Methods

    public void Explode(Collision collision)
    {
        var colliders = new Collider[100];
        Physics.OverlapSphereNonAlloc(transform.position, 1500, colliders);
        for (int i = 0; i < colliders.Length; i++)
        {
            var tempRigidbody = colliders[i]?.GetComponent<Rigidbody>();
            if (!tempRigidbody) continue;
            if (collision.gameObject.GetComponent<ICollision>() != null) collision.gameObject.GetComponent<ICollision>().OnCollision(new InfoCollision(_dmg, collision.contacts[0], collision.transform, gameObject.GetComponent<Rigidbody>().velocity));
            tempRigidbody.useGravity = true;
            tempRigidbody.isKinematic = false;
            tempRigidbody.AddExplosionForce(1500, transform.position, 5, 0.0f);
        }
        AudioSource.PlayClipAtPoint(_clip, transform.position);
        Destroy(gameObject);
    }

    #endregion
}
