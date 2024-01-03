using Game_2048;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem 
    
    //Внедрить систему Save/Load
    
    //Настроить кнопку Reset Record в главном меню
    //Настроить в игре кнопку Restart с окошком выбора (да/нет для подстраховки)
    //Настроить табло с BestRecord в главном меню

{
    private string _saveDirectory = "/save.dat";
    private SaveSystem() { }

    private static SaveSystem _instance;

    private SaveData _saveData;

    public SaveData SaveData()
    {
        
        if (_saveData == null)
            return _saveData = new SaveData();

        Load();

        return _saveData;
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

    public void UpdateBestScore(int newBestScore)
    {
        _saveData.SetBestScore(newBestScore);
        Save();
    }

    public void UpdateCurrentGameState(Field field, int curentScore)
    {
        _saveData.SetCurrentField(field);
        _saveData.SetCurrentScore(curentScore);
        Save();
    }

    private void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + _saveDirectory;
        FileStream stream = new FileStream(path, FileMode.Create);

        if(_saveData == null)
            _saveData = new SaveData();

        formatter.Serialize(stream, _saveData);
        stream.Close();
    }

    public void Load()
    {
        string path = Application.persistentDataPath + _saveDirectory;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            var data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            _saveData = data;
        }
        else
        {
            Debug.Log("Path error, no save date");
            Save();
        }
    }
}
