using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


public class MyWindow : EditorWindow
{
    #region Fields

    public static GameObject ObjectInstantiate;
    public string NameObject = "Hello World!";
    public bool IsGroupEnabled;
    public bool RandomColor = true;
    public bool RandomName = true;
    public int CountObject = 1;
    public float Radius = 10;

    private string _name;

    #endregion


    #region UnityMethods

    private void OnGUI()
    {
        GUILayout.Label("Базовые настройки", EditorStyles.boldLabel);
        ObjectInstantiate = EditorGUILayout.ObjectField("Объект который хотим вставить",
            ObjectInstantiate, typeof(GameObject), true) as GameObject;
        NameObject = EditorGUILayout.TextField("Имя объекта", NameObject);
        IsGroupEnabled = EditorGUILayout.BeginToggleGroup("Дополнительные настройки", IsGroupEnabled);
        RandomColor = EditorGUILayout.Toggle("Случайный цвет", RandomColor);
        RandomName = EditorGUILayout.Toggle("Случайное имя", RandomName);
        CountObject = EditorGUILayout.IntSlider("Количество объектов", CountObject, 1, 100);
        Radius = EditorGUILayout.Slider("Радиус окружности", Radius, 10, 50);
        EditorGUILayout.EndToggleGroup();
        if (GUILayout.Button("Создать объекты"))
        {
            if (ObjectInstantiate)
            {
                GameObject root = new GameObject("Root");
                for (int i = 0; i < CountObject; i++)
                {
                    float angle = i * Mathf.PI * 2 / CountObject;
                    Vector3 pos = new Vector3(Mathf.Cos(angle), 0,
                                              Mathf.Sin(angle)) * Radius;
                    GameObject temp = Instantiate(ObjectInstantiate, pos, Quaternion.identity);
                    if (RandomName)
                        temp.name = GenerateName(5);
                    else
                        temp.name = NameObject + "(" + i + ")";
                    temp.transform.parent = root.transform;
                    var tempRenderer = temp.GetComponent<Renderer>();
                    if (tempRenderer && RandomColor)
                    {
                        tempRenderer.material.color = Random.ColorHSV();
                    }
                }
            }
        }
        if (GUILayout.Button("Удалить объекты Root"))
        {
            if (GameObject.Find("Root") != null)
            {
                DestroyImmediate(GameObject.Find("Root"));
            }
        }
    }

    #endregion


    #region Methods

    private static string GenerateName(int length)
    {
        string name = string.Empty;
        for (int i = 0; i < length; i++)
        {
            name += (char)Random.Range(65, 93);
        }
        return name;
    }
    
    #endregion
}
