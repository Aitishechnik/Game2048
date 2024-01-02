using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem 
    //���������� ������ � SaveData (�������� Score � Save data)
    //�������� ������� Save/Load
    //��������� ������� ���� � ����� � ������� ����
    //��������� ������ Reset Record � ������� ����
    //��������� � ���� ������ New Game
    //��������� ����� � BestRecord � ������� ����
    //�������� ������� ��������� ���� ��� ���������� 2048
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
