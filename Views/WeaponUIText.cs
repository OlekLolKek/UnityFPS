using UnityEngine;
using UnityEngine.UI;


public sealed class WeaponUIText : MonoBehaviour
{
    #region Fields

    private Text _text;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    #endregion


    #region Methods

    public void ShowData(int countAmmunition, int countMag)
    {
        _text.text = $"{countAmmunition}/{countMag}";
    }

    public void SetActive(bool value)
    {
        _text.gameObject.SetActive(value);
    }

    #endregion
}
