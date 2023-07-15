// Decompiled with JetBrains decompiler
// Type: PlayerData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8E6852E2-1B68-460A-A222-84B5FB6C5C57
// Assembly location: C:\Users\m\Downloads\Solgryns-Pokemon-On-Twitch-App-V2.2.6\Solgryns Pokemon On Twitch_Data\Managed\Assembly-CSharp.dll

using System;

[Serializable]
public class PlayerData
{
    public bool ChulkWipeDex;
    public int DataVersion;
    private const int STATS_NUM = 8;
    public bool HasUploaded;
    public string[,] CaughtPokemon = new string[0, 2];
    public string[,] UserStatsData = new string[0, 8];
    public bool HasCaughtMissingno;
    public string[,] UltraBalls = new string[0, 2];
    public string[,] Teams = new string[0, 7];

    public UserStats GetUserStats(string username)
    {
        this.CheckStatsArray();
        for (int index = 0; index < this.UserStatsData.GetLength(0); ++index)
        {
            if (this.UserStatsData[index, 0].ToLower() == username.ToLower())
                return new UserStats(this.UserStatsData[index, 0], this.IntParseOrElseZero(this.UserStatsData[index, 1]), this.IntParseOrElseZero(this.UserStatsData[index, 2]), this.IntParseOrElseZero(this.UserStatsData[index, 3]), this.IntParseOrElseZero(this.UserStatsData[index, 4]), this.IntParseOrElseZero(this.UserStatsData[index, 5]), this.IntParseOrElseZero(this.UserStatsData[index, 6]), this.IntParseOrElseZero(this.UserStatsData[index, 7]));
        }
        // return new UserStats(username, 0, 0, 0, 0, 0, 0, 1 + UnityEngine.Random.Range(0, PokemonCatcherBot.AMOUNT_OF_TRAINER_SPRITES));
        return new UserStats(username, 0, 0, 0, 0, 0, 0, 1);
    }

    public void SaveUserStats(UserStats stats)
    {
        this.CheckStatsArray();
        ArrayExtensions.AddNonDuplicateEntryTo2DStringArray(ref this.UserStatsData, stats.Username, stats.NumEntered.ToString(), stats.NumCaught.ToString(), stats.NumFailed.ToString(), stats.NumAttempts.ToString(), stats.NumBattles.ToString(), stats.NumBattlesWon.ToString(), stats.TrainerSprite.ToString());
    }

    private int IntParseOrElseZero(string toParse)
    {
        if (toParse == null)
            return 0;
        try
        {
            return int.Parse(toParse);
        }
        catch
        {
            return 0;
        }
    }

    private void CheckStatsArray()
    {
        if (this.UserStatsData == null)
            this.UserStatsData = new string[0, 8];
        if (this.UserStatsData.GetLength(1) >= 8)
            return;
        // Debug.Log((object) "Resized UserStatsData");
        this.UserStatsData = this.ResizeArray<string>(this.UserStatsData, this.UserStatsData.GetLength(0), 8);
    }

    private T[,] ResizeArray<T>(T[,] original, int rows, int cols)
    {
        T[,] objArray = new T[rows, cols];
        int num1 = Math.Min(rows, original.GetLength(0));
        int num2 = Math.Min(cols, original.GetLength(1));
        for (int index1 = 0; index1 < num1; ++index1)
        {
            for (int index2 = 0; index2 < num2; ++index2)
                objArray[index1, index2] = original[index1, index2];
        }
        return objArray;
    }
}
