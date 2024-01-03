using Game_2048;
using System;

[Serializable]
public class SaveData
{    
    public int CurrentScore { get; private set; }

    public Field CurrentField {  get; private set; }

    public int BestScore { get; private set; }

    public SaveData()
    {
        CurrentField = new Field();
    }

    public void SetBestScore(int newBestScore)
    {
        BestScore = newBestScore;
    }

    public void SetCurrentScore(int currentScore)
    {
        CurrentScore = currentScore;
    }

    public void SetCurrentField(Field field)
    {
        CurrentField = field;
    }
}
