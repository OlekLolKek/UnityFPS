using UnityEngine;
using UnityEngine.UI;


public class TargetUIText : MonoBehaviour
{
    #region Fields

    private Target[] _targets;
    private Text _text;
    private int _countPoint;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        _targets = FindObjectsOfType<Target>();
        _text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        foreach (var target in _targets)
        {
            target.OnPointChange += UpdatePoint;
        }
    }

    private void OnDisable()
    {
        foreach (var target in _targets)
        {
            target.OnPointChange -= UpdatePoint;
        }
    }

    #endregion


    #region Methods

    private void UpdatePoint()
    {
        var pointTXT = "очков";
        ++_countPoint;
        if (_countPoint >= 5) pointTXT = "очков";
        else if (_countPoint == 1) pointTXT = "очко";
        else if (_countPoint < 5) pointTXT = "очка";
        _text.text = $"Вы заработали {_countPoint} {pointTXT}";

        //todo Отписаться и удалить из списка
    }

    #endregion
}
