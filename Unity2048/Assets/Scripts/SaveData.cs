using System;

[Serializable]
public class SaveData
{
    public int CurrentScore { get; private set; }

    public int[,] CurrentField {  get; private set; }

    public int BestScore { get; private set; }

    public void SetBestScore(int newBestScore)
    {
        BestScore = newBestScore;
    }

    public void SetCurrentScore(int currentScore)
    {
        CurrentScore = currentScore;
    }

    public void SetCurrentField(int[,] field)
    {
        CurrentField = field;
    }
}
