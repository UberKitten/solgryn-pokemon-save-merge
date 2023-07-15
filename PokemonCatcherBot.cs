using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

internal class PokemonCatcherBot
{

    public string SAVE_DATA_PATH;

    public PokemonCatcherBot(string SaveDataPath)
    {
        this.SAVE_DATA_PATH = SaveDataPath;
    }


    public string USERNAME_STREAMER = "theUberKitten";

    public PlayerData SaveData = new PlayerData();
    public Dictionary<string, TrainerData> TrainerDatabase = new Dictionary<string, TrainerData>();

    public List<Pokemon> PokemonDatabase = new List<Pokemon>();

    public void Load()
    {
        this.PokemonDatabase = new List<Pokemon>();


        // string[] strArray1 = this.PokemonDataFile.text.Split('\n');
        string[] strArray1 = File.ReadAllLines("assets\\gen2\\pokemon-data.txt");
        for (int index1 = 1; index1 < strArray1.Length - 1; ++index1)
        {
            string[] strArray2 = Regex.Split(strArray1[index1], ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            switch (Pokemon.PokemonSetID)
            {
                case "Solgymon":
                    int index2 = int.Parse(strArray2[0]);
                    string str1 = strArray2[1];
                    float num1 = float.Parse(strArray2[3]);
                    string name1 = str1;
                    double catchRate1 = (double)num1;
                    this.PokemonDatabase.Add(new Pokemon(index2, name1, (float)catchRate1)
                    {
                        DataIndex = index1,
                        Species = strArray2[2],
                        HP = (float)int.Parse(strArray2[4]),
                        Attack = (float)int.Parse(strArray2[5]),
                        Defense = (float)int.Parse(strArray2[6]),
                        SpecialAttack = (float)int.Parse(strArray2[7]),
                        SpecialDefense = (float)int.Parse(strArray2[8]),
                        Speed = (float)int.Parse(strArray2[9]),
                        Type1 = strArray2[10],
                        Type2 = strArray2[11],
                        DexDescription = strArray2[13],
                        Animation = strArray2[16]
                    });
                    break;
                case "Wettymon":
                    int index3 = int.Parse(strArray2[0]);
                    string str2 = strArray2[1];
                    float num2 = float.Parse(strArray2[3]);
                    string name2 = str2;
                    double catchRate2 = (double)num2;
                    this.PokemonDatabase.Add(new Pokemon(index3, name2, (float)catchRate2)
                    {
                        DataIndex = index1,
                        Species = strArray2[2],
                        HP = (float)int.Parse(strArray2[4]),
                        Attack = (float)int.Parse(strArray2[5]),
                        Defense = (float)int.Parse(strArray2[6]),
                        SpecialAttack = (float)int.Parse(strArray2[7]),
                        SpecialDefense = (float)int.Parse(strArray2[8]),
                        Speed = (float)int.Parse(strArray2[9]),
                        Type1 = strArray2[10],
                        Type2 = strArray2[11],
                        DexDescription = strArray2[13],
                        Ability = strArray2[14],
                        AbilityDescription = strArray2[15],
                        Animation = strArray2[16]
                    });
                    break;
                default:
                    if (string.IsNullOrEmpty(strArray2[2]))
                        strArray2[2] = "45";
                    Pokemon pokemon = new Pokemon(int.Parse(strArray2[0]), strArray2[1], float.Parse(strArray2[2]));
                    pokemon.DataIndex = pokemon.Index;
                    pokemon.Status = strArray2[3];
                    pokemon.Species = strArray2[4];
                    pokemon.HP = float.Parse(strArray2[5]);
                    pokemon.Attack = float.Parse(strArray2[6]);
                    pokemon.Defense = float.Parse(strArray2[7]);
                    pokemon.SpecialAttack = float.Parse(strArray2[8]);
                    pokemon.SpecialDefense = float.Parse(strArray2[9]);
                    pokemon.Speed = float.Parse(strArray2[10]);
                    if (strArray2.Length > 11)
                    {
                        pokemon.Type1 = strArray2[11];
                        pokemon.Type2 = strArray2[12];
                        pokemon.DexDescription = strArray2[13];
                        pokemon.Ability = strArray2[14];
                        pokemon.AbilityDescription = strArray2[15];
                        //Debug.Log((object)(strArray2[13] + " --- " + strArray2[14] + " --- " + strArray2[15]));
                    }
                    this.PokemonDatabase.Add(pokemon);
                    break;
            }
        }

        this.SaveData = (PlayerData)FileManager.LoadData(this.SAVE_DATA_PATH);
        if (this.SaveData == null)
        {
            //this.SaveData = new PlayerData();
            //this.SaveData.DataVersion = 1;
            throw new Exception("Could not find file " + this.SAVE_DATA_PATH);
        }
        for (int index = 0; index < this.SaveData.CaughtPokemon.GetLength(0); ++index)
        {
            if (!this.TrainerDatabase.ContainsKey(this.SaveData.CaughtPokemon[index, 0]))
            {
                TrainerData trainerData = this.GetTrainerData(this.SaveData.CaughtPokemon[index, 0]);
                string[] pokemonList = this.SaveData.CaughtPokemon[index, 1].Split(',');
                for (int k = 0; k < pokemonList.Length; k++)
                {
                    string[] pokemonBits = pokemonList[k].Split('|');
                    if (pokemonBits.Length > 1)
                    {
                        Pokemon pokemonFromName = this.GetPokemonFromName(pokemonBits[1]);
                        if (pokemonBits[2] == "S")
                            pokemonFromName.Shiny = true;
                        if (trainerData.Pokemon != null && !trainerData.Pokemon.Exists((Predicate<Pokemon>)(p => p != null && p.Name == pokemonBits[1])))
                            trainerData.Pokemon.Add(pokemonFromName);
                    }
                    else if (trainerData.Pokemon != null && !trainerData.Pokemon.Exists((Predicate<Pokemon>)(p => p != null && p.Name == pokemonList[k])))
                        trainerData.Pokemon.Add(this.GetPokemonFromName(pokemonList[k]));
                }
            }
        }
        for (int index = 0; index < this.SaveData.UserStatsData.GetLength(0); ++index)
        {
            TrainerData trainerData = this.GetTrainerData(this.SaveData.UserStatsData[index, 0]);
            trainerData.Stats = this.SaveData.GetUserStats(trainerData.Username);
        }
        if (this.SaveData.DataVersion == 0)
        {
            this.SaveData.DataVersion = 1;
            this.Save();
        }
        this.LoadUltraBalls();
    }

    public void Save()
    {
        for (int index1 = 0; index1 < this.TrainerDatabase.Count; ++index1)
        {
            TrainerData trainerData = this.TrainerDatabase.ElementAt<KeyValuePair<string, TrainerData>>(index1).Value;
            string str = "";
            if (trainerData.Pokemon == null)
                trainerData.Pokemon = new List<Pokemon>();
            for (int index2 = 0; index2 < trainerData.Pokemon.Count && trainerData.Pokemon[index2] != null; ++index2)
            {
                str += trainerData.Pokemon[index2].DataString;
                if (index2 < trainerData.Pokemon.Count)
                    str += ",";
            }
            ArrayExtensions.AddNonDuplicateEntryTo2DStringArray(ref this.SaveData.CaughtPokemon, trainerData.Username, str);
            if (trainerData.Stats == null)
                trainerData.Stats = new UserStats(trainerData.Username);
            this.SaveData.SaveUserStats(trainerData.Stats);
        }
        FileManager.SaveData(this.SAVE_DATA_PATH, (object)this.SaveData);
    }


    public TrainerData GetTrainerData(string username)
    {
        if (this.TrainerDatabase == null)
            this.TrainerDatabase = new Dictionary<string, TrainerData>();
        for (int index = 0; index < this.TrainerDatabase.Count; ++index)
        {
            KeyValuePair<string, TrainerData> keyValuePair = this.TrainerDatabase.ElementAt<KeyValuePair<string, TrainerData>>(index);
            if (!string.IsNullOrEmpty(username) && keyValuePair.Key.ToLower() == username.ToLower())
            {
                this.NullPokemonCheck(this.TrainerDatabase.ElementAt<KeyValuePair<string, TrainerData>>(index).Value);
                return this.TrainerDatabase.ElementAt<KeyValuePair<string, TrainerData>>(index).Value;
            }
        }
        if (!this.TrainerDatabase.ContainsKey(username))
            this.TrainerDatabase.Add(username, new TrainerData(username));
        if (this.TrainerDatabase[username] == null)
            this.TrainerDatabase[username] = new TrainerData(username);
        if (this.TrainerDatabase[username].Pokemon == null)
            this.TrainerDatabase[username].Pokemon = new List<Pokemon>();
        this.NullPokemonCheck(this.TrainerDatabase[username]);
        return this.TrainerDatabase[username];
    }
    private void NullPokemonCheck(TrainerData trainer)
    {
        for (int index = 0; index < trainer.Pokemon.Count; ++index)
        {
            if (trainer.Pokemon[index] == null)
                trainer.Pokemon.RemoveAt(index);
        }
    }
    public void LoadUltraBalls()
    {
        if (this.SaveData.UltraBalls == null)
            this.SaveData.UltraBalls = new string[0, 2];
        string[,] ultraBalls = this.SaveData.UltraBalls;
        for (int index = 0; index < ultraBalls.GetLength(0); ++index)
            this.GetTrainerData(ultraBalls[index, 0]).UltraBalls = int.Parse(ultraBalls[index, 1]);
    }
    private Pokemon GetPokemonFromName(string name)
    {
        Pokemon pokemon = this.PokemonDatabase.Find((Predicate<Pokemon>)(p => p.Name.ToLower() == name.ToLower()));
        return pokemon != null ? (Pokemon)pokemon.Clone() : (Pokemon)null;
    }

    public void GivePokemon(string username, Pokemon pokemon)
    {
        TrainerData trainerData = this.GetTrainerData(username);
        Pokemon pokemon1 = trainerData.Pokemon.Find((Predicate<Pokemon>)(p => p != null && p.Name == pokemon.Name));
        if (trainerData.Pokemon.Count > 0 && pokemon1 != null)
        {
            if (!pokemon1.Shiny && pokemon.Shiny)
            {
                //Debug.Log((object)("(SHINY UPDATE) Made " + username + "'s " + (object)pokemon + " shiny"));
                pokemon1.Shiny = true;
            }
            else
            {
                //Debug.Log((object)("(DUPLICATE) Can't give " + username + " a " + (object)pokemon + " (" + (object)pokemon1 + " & " + (object)pokemon + ")"));
                return;
            }
        }
        else
        {
            //Debug.Log((object)("(ADD) Gave " + username + " a " + (object)pokemon));
            trainerData.Pokemon.Add(pokemon);
        }
        //this.Save();
        //this.StartCoroutine(this.UploadUserData(trainerData.Username));
    }

    public List<Pokemon> GetUserTeam(string username)
    {
        List<Pokemon> userTeam = new List<Pokemon>();
        TrainerData trainerData = this.GetTrainerData(username);
        if (this.SaveData.Teams == null)
            return userTeam;
        for (int u = 0; u < this.SaveData.Teams.GetLength(0); u++)
        {
            if (username.ToLower() == this.SaveData.Teams[u, 0].ToLower())
            {
                for (int i = 1; i < 7; i++)
                {
                    if (!string.IsNullOrEmpty(this.SaveData.Teams[u, i]))
                    {
                        Pokemon pokemon = trainerData.Pokemon.Find((Predicate<Pokemon>)(p => p.Name.ToLower() == this.SaveData.Teams[u, i].ToLower()));
                        if (pokemon != null)
                        {
                            //Debug.Log((object)("mymon shiny? " + this.SaveData.Teams[u, i] + " - " + pokemon.Shiny.ToString()));
                            userTeam.Add(pokemon);
                        }
                    }
                }
            }
        }
        return userTeam;
    }
}
