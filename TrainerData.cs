// Decompiled with JetBrains decompiler
// Type: TrainerData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8E6852E2-1B68-460A-A222-84B5FB6C5C57
// Assembly location: C:\Users\m\Downloads\Solgryns-Pokemon-On-Twitch-App-V2.2.6\Solgryns Pokemon On Twitch_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class TrainerData
{
  public string Username;
  public UserStats Stats;
  public List<global::Pokemon> Pokemon = new List<global::Pokemon>();
  public int UltraBalls;

  public TrainerData(string username)
  {
    this.Username = username;
    this.Stats = new UserStats(username);
    if (this.Pokemon != null)
      return;
    this.Pokemon = new List<global::Pokemon>();
  }

  public override string ToString() => this.Username;

  public string ToFullString() => this.Username + " ... Pokemon: " + string.Join<global::Pokemon>(", ", (IEnumerable<global::Pokemon>) this.Pokemon);

  public bool Equals(TrainerData obj) => this.Username.ToLower() == obj.Username.ToLower();
}
