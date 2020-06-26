using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class InterfaceResources : MonoBehaviour
{
    #region Properties

    public ButtonUI ButtonPrefab { get; private set; }
    public Canvas MainCanvas { get; private set; }
    public SliderUI ProgressbarPrefab { get; private set; }

    #endregion


    #region UnityMethods

    private void Awake()
    {
        ButtonPrefab = Resources.Load<ButtonUI>("Button");
        MainCanvas = FindObjectOfType<Canvas>();
        ProgressbarPrefab = Resources.Load<SliderUI>("Progressbar");
    }

    #endregion
}
