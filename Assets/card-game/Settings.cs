using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering;

public static class Settings
{
    public static readonly float CardPause = .35f;
    public static readonly float EnemyTurnPause = 0.6f;
    public static readonly float CardSpeed = 6;

    public static float LongCardPause = .5f;

    public static SettingsData Data = new SettingsData();

    public static void SaveSettings()
    {
        if (!File.Exists($"{Application.dataPath}/Save/settings.xml"))
        {
            Directory.CreateDirectory($"{Application.dataPath}/Save");
            var x = File.Create($"{Application.dataPath}/Save/settings.xml");
            x.Close();
        }
        var serializer = new XmlSerializer(typeof(SettingsData));
        var stream = new FileStream($"{Application.dataPath}/Save/settings.xml", FileMode.Create);
        serializer.Serialize(stream, Data);
        stream.Close();
    }

    public static void LoadSettings()
    {
        if (File.Exists($"{Application.dataPath}/Save/settings.xml"))
        {
            var serializer = new XmlSerializer(typeof(SettingsData));
            var stream = new FileStream($"{Application.dataPath}/Save/settings.xml", FileMode.Open);

            SettingsData loadedData = serializer.Deserialize(stream) as SettingsData;
            stream.Close();

            Data = loadedData;
        }
    }
}

[System.Serializable]
public class SettingsData
{
    public bool FirstTutorialPassed;
    public bool OnlyTutorial;
}
