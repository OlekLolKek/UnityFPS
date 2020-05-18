using System.IO;
using UnityEngine;


public sealed class SaveDataRepository
{
    #region Fields

    private readonly IData<SerializableGameObject> _data;

    private const string FOLDER_NAME = "dataSave";
    private const string FILE_NAME = "data.bat";
    private readonly string _path;

    #endregion


    #region ClassLyfeCycle

    public SaveDataRepository()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            _data = new PlayerPrefsData();
        }
        else
        {
            _data = new JsonData<SerializableGameObject>();
        }
        _path = Path.Combine(Application.dataPath, FOLDER_NAME);
    }

    #endregion


    #region Methods

    public void Save()
    {
        if (!Directory.Exists(Path.Combine(_path)))
        {
            Directory.CreateDirectory(_path);
        }
        var player = new SerializableGameObject
        {
            Pos = ServiceLocatorMonoBehaviour.GetService<CharacterController>().gameObject.transform.position,
            Name = "NEDNAR",
            IsEnable = true
        };

        _data.Save(player, Path.Combine(_path, FILE_NAME));
    }

    public void Load()
    {
        var file = Path.Combine(_path, FILE_NAME);
        if (!File.Exists(file)) return;
        var newPlayer = _data.Load(file);
        ServiceLocatorMonoBehaviour.GetService<CharacterController>().gameObject.transform.position = newPlayer.Pos;
        ServiceLocatorMonoBehaviour.GetService<CharacterController>().gameObject.name = newPlayer.Name;
        ServiceLocatorMonoBehaviour.GetService<CharacterController>().gameObject.SetActive(newPlayer.IsEnable);

        Debug.Log(newPlayer);
    }

    #endregion
}
