using UnityEngine;
using System;


public class DestroyPoint : MonoBehaviour
{
    #region Fields

    public event Action<GameObject> OnFinishChange = delegate (GameObject o) { };

    #endregion


    #region Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bot>())
        {
            OnFinishChange.Invoke(gameObject);
        }
    }

    #endregion
}
