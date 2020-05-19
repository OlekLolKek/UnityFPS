using UnityEngine;
using UnityEngine.UI;


public class RadarObj : MonoBehaviour
{
    #region Fields

    [SerializeField] private Image _ico;

    #endregion


    #region UnityMethods

    private void OnValidate()
    {
        _ico = Resources.Load<Image>("Image (1)");
    }

    private void OnDisable()
    {
        Radar.RemoveRadarObject(gameObject);
    }

    private void OnEnable()
    {
        Radar.RegisterRadarObject(gameObject, _ico);
    }

    #endregion
}
