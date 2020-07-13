using UnityEngine;


public class LightPointer : MonoBehaviour
{
    #region Fields

    [SerializeField] private Light _mouseLight;

    private int _quality;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _quality = QualitySettings.GetQualityLevel();
    }

    private void Update()
    {
        if (_quality > 3)
        {
            LightMouse();
        }
    }

    #endregion


    #region Methods

    public void LightMouse()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit))
        {

            if (_mouseLight != null)
            {
                _mouseLight.transform.position = hit.point;
            }
        }
    }

    #endregion
}
