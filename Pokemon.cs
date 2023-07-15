// Decompiled with JetBrains decompiler
// Type: Pokemon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8E6852E2-1B68-460A-A222-84B5FB6C5C57
// Assembly location: C:\Users\m\Downloads\Solgryns-Pokemon-On-Twitch-App-V2.2.6\Solgryns Pokemon On Twitch_Data\Managed\Assembly-CSharp.dll


using System;

public class Pokemon : ICloneable
{
    public static string PokemonSetID = "Gen2";
    public static string PokemonName = "Pokémon";
    public static string PokemartName = "Pokémart";
    public static string MissingNoTypePokemonName = "MissingNo.";
    public float CatchRate;
    public int Index;
    public int DataIndex;
    public string Name;
    public string Species;
    public string Status;
    public string Type1;
    public string Type2;
    public string DexDescription;
    public string Ability;
    public string AbilityDescription;
    public string Animation;
    public float HP;
    public float Attack;
    public float Defense;
    public float SpecialAttack;
    public float SpecialDefense;
    public float Speed;
    public float Health;
    public bool Shiny;
    public static float ShinyChance = 0.04f;
    private float _faintAnimationTimer = -1f;

    public string DataString => this.DataIndexWithLeadingZeros + "|" + this.Name + "|" + (this.Shiny ? "S" : "N");

    public string IndexWithLeadingZeros => string.Format("{0:000}", (object)this.Index);

    public string DataIndexWithLeadingZeros => string.Format("{0:000}", (object)this.DataIndex);

    // public float CatchChance => Mathf.Max((float)((double)this.CatchRate / (double)byte.MaxValue * 0.699999988079071), 0.08f) * (float)(1.0 + (1.0 - (double)this.Health / (double)this.Healthmax) * 2.0);

    // public float TimeToSpawn => (float)(PlayerPrefs.GetInt("MinimumAppearance", -1) * 60) + 8000f / Mathf.Max(this.CatchRate, 40f);

    public bool IsLegendary => (double)this.CatchRate <= 3.0;

    public float DodgeChance => (float)((0.05000000074505806 + (double)this.Speed / 1200.0) * (this.Shiny ? 1.1000000238418579 : 1.0));

    public float CritChance => (float)(0.10000000149011612 * (this.Shiny ? 1.25 : 1.0));

    // public float BST => this.HP + this.AverageAttack + this.AverageDefense + this.Speed;

    // public float AverageAttack => (float)(((double)this.MaxAttack + (double)this.Attack + (double)this.SpecialAttack) / 3.0);

    // public float AverageDefense => (float)(((double)this.MaxDefense + (double)this.Defense + (double)this.SpecialDefense) / 3.0);

    // public float MaxAttack => Mathf.Max(this.Attack, this.SpecialAttack);

    // public float MaxDefense => Mathf.Max(this.Defense, this.SpecialDefense);

    public static string PokemonImagePrefix
    {
        get
        {
            switch (Pokemon.PokemonSetID)
            {
                case "Wettymon":
                    return "";
                case "Original151":
                    return "Spr_1y_";
                default:
                    return "Spr_2c_";
            }
        }
    }

    public string SpritePath
    {
        get
        {
            if (Pokemon.PokemonSetID == "Solgymon")
                return Pokemon.PokemonSetID + "/Pokemon Sprites/" + this.Name.ToLower() + (this.Shiny ? "_s" : "");
            return Pokemon.PokemonSetID + "/Pokemon Sprites/" + Pokemon.PokemonImagePrefix + this.DataIndexWithLeadingZeros + (this.Shiny ? "_s" : "");
        }
    }

    public string CryPath => Pokemon.PokemonSetID + "/Pokemon Cries/" + this.DataIndexWithLeadingZeros + " - " + this.Name;

    // public float Healthmax => (float)Mathf.RoundToInt((float)(15.0 + (double)this.HP * 0.800000011920929));

    // public float HealthPercentage => this.Health / this.Healthmax;

    public Pokemon(int index, string name, float catchRate)
    {
        this.Index = index;
        this.Name = name;
        this.CatchRate = catchRate;
    }

    public object Clone() => this.MemberwiseClone();

    public override string ToString() => (this.Shiny ? "Shiny " : "") + this.Name;

    // public string ToFullString() => string.Format("#{0} {1} the {2} | {3} Health| {4} Attack| {5} Special Attack| {6} Defense| {7} Special Defense| {8} Speed| {9} Rating", (object)this.IndexWithLeadingZeros, (object)this.Name, (object)this.Species, (object)this.Healthmax, (object)this.Attack, (object)this.SpecialAttack, (object)this.Defense, (object)this.SpecialDefense, (object)this.Speed, (object)Mathf.RoundToInt(this.BST));

    // public Pokemon.PokemonHit HitAnotherPokemon(
    //   ref Pokemon opponent,
    //   Transform opponentTransform,
    //   float overallDamageMult = 1f)
    // {
    //     Pokemon.PokemonHit pokemonHit = new Pokemon.PokemonHit();
    //     if ((double)this.Health <= 0.0 || (double)opponent.Health <= 0.0)
    //         return pokemonHit;
    //     float num1 = this.CalcDamage(this.Attack, opponent.Defense);
    //     float num2 = this.CalcDamage(this.SpecialAttack, opponent.SpecialDefense);
    //     float num3 = num1;
    //     pokemonHit.PhysicalHit = (double)num1 > (double)num2;
    //     Debug.Log((object)(this.Name + ": " + (object)num1 + " vs " + (object)num2));
    //     if (!pokemonHit.PhysicalHit)
    //     {
    //         num3 = num2;
    //         pokemonHit.PhysicalHit = false;
    //     }
    //     if ((double)UnityEngine.Random.value < (double)opponent.DodgeChance)
    //     {
    //         pokemonHit.Miss = true;
    //         overallDamageMult = 0.0f;
    //         PokemonCatcherBot.Instance.PlaySoundEffect(PokemonCatcherBot.Instance.SOUND_MISS);
    //     }
    //     else if (pokemonHit.PhysicalHit)
    //         ObjectPooler.Instance.Spawn(ObjectPooler.TACKLE_EFFECT, opponentTransform.position.Offset(0.0f, 15f));
    //     else
    //         ObjectPooler.Instance.Spawn(ObjectPooler.SPECIAL_HIT_EFFECT, opponentTransform.position.Offset(0.0f, 15f));
    //     if ((double)UnityEngine.Random.value < (double)this.CritChance)
    //     {
    //         overallDamageMult *= 2f;
    //         pokemonHit.Crit = true;
    //         if (!pokemonHit.Miss)
    //             PokemonCatcherBot.Instance.PlaySoundEffect(PokemonCatcherBot.Instance.SOUND_CRIT);
    //     }
    //     else if (!pokemonHit.Miss)
    //     {
    //         if (pokemonHit.PhysicalHit)
    //             PokemonCatcherBot.Instance.PlaySoundEffect(PokemonCatcherBot.Instance.SOUND_TACKLE);
    //         else
    //             PokemonCatcherBot.Instance.PlaySoundEffect(PokemonCatcherBot.Instance.SOUND_SPECIAL);
    //     }
    //     pokemonHit.Damage = overallDamageMult * num3;
    //     opponent.Health = opponent.Health.GoTowards(0.0f, pokemonHit.Damage);
    //     return pokemonHit;
    // }

    // private float CalcDamage(float attackStat, float defenseStat) => (float)(9.0 * (double)UnityEngine.Random.Range(0.9f, 1.1f) * ((double)attackStat / (double)defenseStat));

    // public bool FaintAnimation(ref PokemonDisplay display)
    // {
    //     PokemonCatcherBot instance = PokemonCatcherBot.Instance;
    //     this._faintAnimationTimer += Time.deltaTime * 60f;
    //     double number = (double)Mathf.Round(this._faintAnimationTimer);
    //     if (number == 30.0)
    //     {
    //         instance.StartCoroutine(PokemonCatcherBot.Instance.PlayPokemonCryCoroutine(this, display.transform.position, display._renderer, false));
    //         instance.PokemonCryAudioSource.pitch = 0.7f;
    //         instance.SoundEffects.pitch = 0.99f;
    //         display.RestartAnimation(4);
    //         display.AnimationSpeed = 0.75f;
    //     }
    //     float num = (float)((double)instance.PokemonCryAudioSource.clip.length * 60.0 - 30.0);
    //     if (((float)number).IsBetween(78f + num, 110f + num))
    //     {
    //         if ((double)instance.SoundEffects.pitch == 0.99000000953674316)
    //         {
    //             instance.SoundEffects.pitch = 1f;
    //             instance.PlaySoundEffect(instance.SOUND_FAINT);
    //         }
    //         display.transform.localPosition = display.transform.localPosition + new Vector3(0.0f, -16f);
    //     }
    //     if (number < 110.0 + (double)num)
    //         return false;
    //     instance.SoundEffects.pitch = 1f;
    //     instance.PokemonCryAudioSource.pitch = 1f;
    //     display.gameObject.SetActive(false);
    //     Vector3 localPosition = display.transform.localPosition with
    //     {
    //         y = 0.0f
    //     };
    //     display.transform.localPosition = localPosition;
    //     this._faintAnimationTimer = -1f;
    //     display.AnimationSpeed = 1f;
    //     return true;
    // }

    public string ToDexDescription() => string.IsNullOrEmpty(this.Ability) ? "#" + this.IndexWithLeadingZeros + " " + this.Name + " the " + this.Species : "#" + this.IndexWithLeadingZeros + " " + this.Name + " the " + this.Species + " ... " + this.DexDescription + ". MorphinTime Ability: " + this.Ability + " (" + this.AbilityDescription + ") ";

    public class PokemonHit
    {
        public bool Crit;
        public bool Miss;
        public float Damage;
        public bool PhysicalHit = true;
    }
}
