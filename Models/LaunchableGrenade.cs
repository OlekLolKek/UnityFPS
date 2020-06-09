using UnityEngine;


public sealed class LaunchableGrenade : Ammunition
{
    #region UnityMethods

    private void OnCollisionEnter(Collision collision)
    {
        var setDamage = collision.gameObject.GetComponent<ICollision>();

        var colliders = new Collider[100];
        Physics.OverlapSphereNonAlloc(transform.position, 1500, colliders);

        Explode(colliders, collision);

        DestroyAmmunition();
    }

    public void Explode(Collider[] hitColliders, Collision collision)
    {
        for (int i = 0; i < hitColliders.Length; i++)
        {
            var tempRigidbody = hitColliders[i]?.GetComponent<Rigidbody>();
            if (!tempRigidbody) continue;
            if (collision.gameObject.GetComponent<ICollision>() != null) collision.gameObject.GetComponent<ICollision>().OnCollision(new InfoCollision(_currentDamage, collision.contacts[0], collision.transform, Rigidbody.velocity));
            tempRigidbody.useGravity = true;
            tempRigidbody.isKinematic = false;
            tempRigidbody.AddExplosionForce(1500, transform.position, 5, 0.0f);
        }
    }

    #endregion
}
