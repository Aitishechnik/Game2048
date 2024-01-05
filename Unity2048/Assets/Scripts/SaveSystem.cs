using Game_2048;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem 
{
    private string _saveDirectory = "/save.dat";
    private SaveSystem() { }

    private static SaveSystem _instance;

    private SaveData _gameData;

    public SaveData GameData
    {
        get
        {
            if (_gameData == null)
                _gameData = Load();

            return _gameData;
        }
    }

    public static SaveSystem Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SaveSystem();

            return _instance;
        }
    }
    public void Save(SaveData saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + _saveDirectory;

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(stream, saveData);
            stream.Close();
        }           
    }

    public SaveData Load()
    {
        string path = Application.persistentDataPath + _saveDirectory;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            SaveData data;

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                data = formatter.Deserialize(stream) as SaveData;
                stream.Close();
                
            }
            return data;
        }
        else
        {
            Debug.Log("Path error, no save date");
            var data = new SaveData();
            Save(data);
            return data;
        }
    }

    public void NullifyCurrentGameData()
    {
        _gameData.SetCurrentScore(0);
        _gameData.SetCurrentField(null);
        Save(SaveSystem.Instance.GameData);
    }

    public void NullifyBestScore()
    {
        _gameData.SetBestScore(0);
        NullifyCurrentGameData();
    }
}
