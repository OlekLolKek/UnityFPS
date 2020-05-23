using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class Explosion : MonoBehaviour
{
    #region Fields

    public float Radius;
    public float Force;

    [SerializeField] private GameObject _light;

    private int _timeLight;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _light.GetComponent<Light>().color = Color.red;
        StartCoroutine(Light());
        _timeLight = 1;

        var colliders = new Collider[100];
        Physics.OverlapSphereNonAlloc(transform.position, Radius, colliders);
        Explode(colliders);
    }

    //private void OnTriggerExit(Collider other)
    //{
        // Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);
        // Explode(hitColliders);
    //}

    private void OnTriggerEnter(Collider other)
    {
        _timeLight = 2;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    #endregion


    #region Methods

    private void Explode(Collider[] hitColliders)
    {
        foreach (Collider obj in hitColliders)
        {
            var tempRigidbody = obj.GetComponent<Rigidbody>();
            if (!tempRigidbody) continue;
            tempRigidbody.useGravity = true;
            tempRigidbody.isKinematic = false;
            tempRigidbody.AddExplosionForce(Force, transform.position, Radius, 0.0f);
        }
    }

    IEnumerator Light()
    {
        while (true)
        {
            _light.SetActive(!_light.activeSelf);
            yield return new WaitForSeconds(_timeLight);
        }
    }

    IEnumerator Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);
        yield return new WaitForSeconds(2);
        Explode(hitColliders);
        yield return new WaitForSeconds(0.5f);
        Explode(hitColliders);
    }

    #endregion
}
