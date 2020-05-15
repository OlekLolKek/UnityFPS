using UnityEngine;
using UnityEngine.UI;


public sealed class FlashlightUIText : MonoBehaviour
{
    #region Fields

    private Text _text;

    #endregion


    #region Properties

    public float Text
    {
        set => _text.text = $"{value:0.0}";
    }

    #endregion


    #region Methods

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    public void SetActive(bool value)
    {
        _text.gameObject.SetActive(value);
    }

    #endregion
}
