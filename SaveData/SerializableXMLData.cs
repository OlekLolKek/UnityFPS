using System;
using System.IO;
using System.Xml.Serialization;


public class SerializableXMLData<T> : IData<T>
{
    #region Fields

    private static XmlSerializer _formatter;

    #endregion


    #region ClassLifeCycle

    public SerializableXMLData()
    {
        _formatter = new XmlSerializer(typeof(T));
    }

    #endregion


    #region Methods

    public void Save(T data, string path = null)
    {
        using (var fs = new FileStream(path, FileMode.Create))
        {
            _formatter.Serialize(fs, data);
        }
    }

    public T Load(string path)
    {
        T result;
        if (!File.Exists(path)) return default(T);
        using (var fs = new FileStream(path, FileMode.Open))
        {
            result = (T)_formatter.Deserialize(fs);
        }
        return result;
    }

    #endregion
}
