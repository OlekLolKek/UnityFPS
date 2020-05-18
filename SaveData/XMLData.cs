using System.IO;
using System.Xml;
using UnityEngine;


public class XMLData : IData<SerializableGameObject>
{
    public void Save(SerializableGameObject player, string path = "")
    {
        var xmlDoc = new XmlDocument();

        XmlNode rootNode = xmlDoc.CreateElement("Player");
        xmlDoc.AppendChild(rootNode);

        var element = xmlDoc.CreateElement(Crypto.CryptoXOR("Name"));
        element.SetAttribute("value", Crypto.CryptoXOR(player.Name));
        rootNode.AppendChild(element);

        element = xmlDoc.CreateElement("PosX");
        element.SetAttribute("value", player.Pos.X.ToString());
        element.SetAttribute("X", player.Pos.X.ToString());
        rootNode.AppendChild(element);

        element = xmlDoc.CreateElement("PosY");
        element.SetAttribute("value", player.Pos.Y.ToString());
        rootNode.AppendChild(element);

        element = xmlDoc.CreateElement("PosZ");
        element.SetAttribute("value", player.Pos.Z.ToString());
        rootNode.AppendChild(element);

        element = xmlDoc.CreateElement("IsEnable");
        element.SetAttribute("value", player.IsEnable.ToString());
        rootNode.AppendChild(element);

        XmlNode userNode = xmlDoc.CreateElement("Info");
        var attribute = xmlDoc.CreateAttribute("Unity");
        attribute.Value = Application.unityVersion;
        userNode.Attributes.Append(attribute);
        userNode.InnerText = "System Language: " + Application.systemLanguage;
        rootNode.AppendChild(userNode);
        xmlDoc.Save(path);
    }
    
    public SerializableGameObject Load(string path = "")
    {
        var result = new SerializableGameObject();
        if (!System.IO.File.Exists(path)) return result;
        using (var reader = new XmlTextReader(path))
        {
            while (reader.Read())
            {
                var key = Crypto.CryptoXOR("Name");
                if (reader.IsStartElement(key))
                {
                    result.Name = Crypto.CryptoXOR(reader.GetAttribute("value"));
                }
                key = "PosX";
                if (reader.IsStartElement(key))
                {
                    result.Pos.X = reader.GetAttribute("value").TrySingle();
                }
                key = "PosY";
                if (reader.IsStartElement(key))
                {
                    result.Pos.Y = reader.GetAttribute("value").TrySingle();
                }
                key = "PosZ";
                if (reader.IsStartElement(key))
                {
                    result.Pos.Z = reader.GetAttribute("value").TrySingle();
                }
                key = "IsEnable";
                if (reader.IsStartElement(key))
                {
                    result.IsEnable = reader.GetAttribute("value").TryBool();
                }
            }
        }
        return result;
    }
}
