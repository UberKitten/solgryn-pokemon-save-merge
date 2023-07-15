// Decompiled with JetBrains decompiler
// Type: UserStats
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8E6852E2-1B68-460A-A222-84B5FB6C5C57
// Assembly location: C:\Users\m\Downloads\Solgryns-Pokemon-On-Twitch-App-V2.2.6\Solgryns Pokemon On Twitch_Data\Managed\Assembly-CSharp.dll


public class UserStats
{
    public string Username;
    public int NumEntered;
    public int NumCaught;
    public int NumFailed;
    public int NumAttempts;
    public int NumBattles;
    public int NumBattlesWon;
    public int TrainerSprite;

    public UserStats(string username) => this.Username = username;

    public UserStats(
      string username,
      int numEntered,
      int numCaught,
      int numFailed,
      int numAttempts,
      int numBattles,
      int numBattlesWon,
      int trainerSprite)
    {
        this.Username = username;
        this.NumEntered = numEntered;
        this.NumCaught = numCaught;
        this.NumFailed = numFailed;
        this.NumAttempts = numAttempts;
        this.NumBattles = numBattles;
        this.NumBattlesWon = numBattlesWon;
        this.TrainerSprite = trainerSprite;
    }

    public static UserStats Combine(UserStats primary, UserStats secondary)
    {
        return new UserStats(primary.Username, primary.NumEntered + secondary.NumEntered, primary.NumCaught + secondary.NumCaught, primary.NumFailed + secondary.NumFailed, primary.NumAttempts + secondary.NumAttempts, primary.NumBattles + secondary.NumBattles, primary.NumBattlesWon + secondary.NumBattlesWon, primary.TrainerSprite); 
    }

    // public override string ToString()
    // {
    //   if (this.NumAttempts == 0)
    //     return " | No attempts yet.";
    //   string str = " | STATS: " + (object) this.NumAttempts + "/" + (object) this.NumEntered + " joined (" + (object) Mathf.Round((float) (((double) this.NumAttempts + 0.0) / ((double) this.NumEntered + 0.0) * 100.0)) + "%) | " + (object) this.NumCaught + "/" + (object) this.NumAttempts + " caught (" + (object) Mathf.Round((float) (((double) this.NumCaught + 0.0) / ((double) this.NumAttempts + 0.0) * 100.0)) + "%)";
    //   if (this.NumBattles > 0)
    //     str = str + " | " + (object) this.NumBattlesWon + "/" + (object) this.NumBattles + " battles won (" + (object) Mathf.Round((float) (((double) this.NumBattlesWon + 0.0) / ((double) this.NumBattles + 0.0) * 100.0)) + "%)";
    //   return str;
    // }
}
