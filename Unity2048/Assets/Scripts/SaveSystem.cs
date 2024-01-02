using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem 
    //Объединить данные в SaveData (добавить Score в Save data)
    //Внедрить систему Save/Load
    //Настроить главное меню и выход в главное меню
    //Настроить кнопку Reset Record в главном меню
    //Настроить в игре кнопку New Game
    //Настриоть табло с BestRecord в главном меню
    //Добавить условие окончания игры при достижении 2048
{
    private string _saveDirectory = "/save.dat";
    private SaveSystem() { }

    private static SaveSystem _instance;

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
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public SaveData Load()
    {
        string path = Application.persistentDataPath + _saveDirectory;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            var data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Path error, no save date");
            return null;
        }
    }
}
