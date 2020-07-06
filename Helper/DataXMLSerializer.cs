using System.IO;
using System.Xml.Serialization;


public class DataXMLSerializer<T> : IData<T>
{
    #region Fields

    private string _path;
    private XmlSerializer _xmlSerializer;

    #endregion


    #region Methods

    public void Save(T value, string path = null)
    {
        if (!typeof(T).IsSerializable) return;
        using (var fs = new FileStream(_path, FileMode.Create))
        {
            _xmlSerializer.Serialize(fs, value);
        }
    }

    public T Load(string path = null)
    {
        T result;
        if (!typeof(T).IsSerializable || !File.Exists(_path)) return default(T);
        using (var fs = new FileStream(_path, FileMode.Open))
        {
            result = (T)_xmlSerializer.Deserialize(fs);
        }
        return result;
    }

    public void SetOptions(string value)
    {
        _xmlSerializer = new XmlSerializer(typeof(T));
        _path = value;
    }

    #endregion
}
