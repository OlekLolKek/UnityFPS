using UnityEngine;

public abstract class BaseObjectScene : MonoBehaviour
{
    #region Fields

    [HideInInspector] public Rigidbody Rigidbody;
    [HideInInspector] public Transform Transform;

    private Color _color;
    private int _layer;
    private bool _isVisible;

    #endregion


    #region Properties

    public string Name
    {
        get => gameObject.name;
        set => gameObject.name = value;
    }

    public int Layer
    {
        get => _layer;
        set
        {
            _layer = value;
            AskLayer(Transform, _layer);
        }
    }

    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            AskColor(transform, _color);
        }
    }

    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            _isVisible = value;
            RendererSetActive(transform);
            if (transform.childCount <= 0) return;
            foreach (Transform t in transform)
            {
                RendererSetActive(t);
            }
        }
    }

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

    public void EnableRigidbody(float force)
    {
        EnableRigidbody();
        Rigidbody.AddForce(transform.forward * force);
    }

    public void EnableRigidbody()
    {
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    public void DisableRigidbody()
    {
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    private void AskLayer(Transform obj, int layer)
    {
        obj.gameObject.layer = layer;
        if (obj.childCount <= 0) return;

        foreach (Transform child in obj)
        {
            AskLayer(child, layer);
        }
    }

    private void RendererSetActive(Transform renderer)
    {
        if (renderer.gameObject.TryGetComponent<Renderer>(out var component))
        {
            component.enabled = _isVisible;
        }
    }

    private void AskColor(Transform obj, Color color)
    {
        foreach (var curMaterial in obj.GetComponent<Renderer>().materials)
        {
            curMaterial.color = color;
        }
        if (obj.childCount <= 0) return;
        foreach (Transform d in obj)
        {
            AskColor(d, color);
        }
    }

    #endregion
}
