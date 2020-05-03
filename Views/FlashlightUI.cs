using UnityEngine;
using UnityEngine.UI;


public sealed class FlashlightUI : MonoBehaviour
{
    #region Fields

    private Text _batteryText;
    //todo image

    #endregion


    #region Properties

    public float Text
    {
        set => _batteryText.text = $"{value:0.0}";
    }

    #endregion


    #region Methods

    private void Awake()
    {
        _batteryText = GetComponent<Text>();
    }

    public void SetActive(bool value)
    {
        _batteryText.gameObject.SetActive(value);
    }

    #endregion
}
