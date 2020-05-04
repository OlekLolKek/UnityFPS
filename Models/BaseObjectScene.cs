using UnityEngine;

public abstract class BaseObjectScene : MonoBehaviour
{
    #region Fields

    public string Name;
    public Rigidbody Rigidbody { get; private set; }
    public Transform Transform { get; private set; }

    public int Layer
    {
        get => _layer;
        set
        {
            _layer = value;
            AskLayer(Transform, _layer);
        }
    }

    private int _layer;

    #endregion


    #region UnityMethods

    protected virtual void Awake()
    {
        Name = name;
        Rigidbody = GetComponent<Rigidbody>();
        Transform = transform;
    }

    #endregion


    #region Methods

    private void AskLayer(Transform obj, int layer)
    {
        obj.gameObject.layer = layer;
        if (obj.childCount <= 0) return;

        foreach (Transform child in obj)
        {
            AskLayer(child, layer);
        }
    }

    #endregion
}
