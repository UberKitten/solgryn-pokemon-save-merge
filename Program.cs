using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: username data-old.dat data-new.dat");
                Console.WriteLine("data-old.dat should be the older file, data-new.dat should be the newer filer");
                Console.WriteLine("Outputs to data-merged.dat");
                return;
            }

            string[] fileNames = { args[1], args[2] };

            var mergedFilename = "data-merged.dat";
            if (File.Exists(mergedFilename))
            {
                File.Delete(mergedFilename);
            }
            File.Copy(fileNames[0], mergedFilename);

            var oldFile = new PokemonCatcherBot(mergedFilename);
            var newFile = new PokemonCatcherBot(fileNames[1]);

            oldFile.USERNAME_STREAMER = args[0];
            newFile.USERNAME_STREAMER = args[0];

            oldFile.Load();
            newFile.Load();

            Console.WriteLine("Loaded data for stream " + newFile.USERNAME_STREAMER);

            Console.WriteLine("Giving test pokemon to " + newFile.USERNAME_STREAMER);
            oldFile.GivePokemon(oldFile.USERNAME_STREAMER, oldFile.PokemonDatabase[1]);
            oldFile.GivePokemon(oldFile.USERNAME_STREAMER, oldFile.PokemonDatabase[2]);
            newFile.GivePokemon(newFile.USERNAME_STREAMER, newFile.PokemonDatabase[1]);
            newFile.GivePokemon(newFile.USERNAME_STREAMER, newFile.PokemonDatabase[3]);

            Console.WriteLine("************** Old file **************");
            foreach (var entry in oldFile.TrainerDatabase)
            {
                Console.WriteLine(entry.Value.ToFullString());
            }
            Console.WriteLine();

            Console.WriteLine("************** New file **************");
            foreach (var entry in newFile.TrainerDatabase)
            {
                Console.WriteLine(entry.Value.ToFullString());
            }
            Console.WriteLine();

            Merge(newFile, oldFile);

            Console.WriteLine("************** Merged file **************");
            foreach (var entry in oldFile.TrainerDatabase)
            {
                Console.WriteLine(entry.Value.ToFullString());
            }
            Console.WriteLine();

            oldFile.Save();
        }

        static void CatchEmAll(PokemonCatcherBot bot, string username)
        {
            foreach (var pokemon in bot.PokemonDatabase)
            {
                bot.GivePokemon(username, pokemon);
            }
        }

        static void Merge(PokemonCatcherBot source, PokemonCatcherBot destination)
        {
            // Combine TrainerDatabase
            foreach (var sourceTrainer in source.TrainerDatabase)
            {
                if (destination.TrainerDatabase.ContainsKey(sourceTrainer.Key))
                {
                    var destinationTrainer = destination.TrainerDatabase[sourceTrainer.Key];

                    // Combine Pokemon
                    foreach (var sourcePokemon in sourceTrainer.Value.Pokemon)
                    {
                        destination.GivePokemon(sourceTrainer.Key, sourcePokemon);
                    }

                    // Combine Ultra Balls
                    destinationTrainer.UltraBalls += sourceTrainer.Value.UltraBalls;

                    // Combine Stats
                    destinationTrainer.Stats = UserStats.Combine(sourceTrainer.Value.Stats, destinationTrainer.Stats);
                }
                else
                {
                    // In source, but not in destination, so copy over
                    destination.TrainerDatabase.Add(sourceTrainer.Key, sourceTrainer.Value);
                }
            }

            // Overwrite any teams in destination

            // Loop through each source team
            for (int i = 0; i < source.SaveData.Teams.GetLength(0); i++)
            {
                var sourceUsername = source.SaveData.Teams[i, 0];

                // Try to find a matching user in destination
                var foundUser = false;
                for (int j = 0; j < source.SaveData.Teams.GetLength(0); j++)
                {
                    if (sourceUsername.ToLower() == destination.SaveData.Teams[j, 0].ToLower())
                    {
                        foundUser = true;

                        // Overwrite destination teams
                        for (int k = 1; k < 7; k++)
                        {
                            destination.SaveData.Teams[j, k] = source.SaveData.Teams[i, k];
                        }

                    }
                }

                if (!foundUser)
                {
                    // Add new user to destination
                    ArrayExtensions.AddNonDuplicateEntryTo2DStringArray(ref destination.SaveData.Teams,
                        source.SaveData.Teams[i, 0],
                        source.SaveData.Teams[i, 1],
                        source.SaveData.Teams[i, 2],
                        source.SaveData.Teams[i, 3],
                        source.SaveData.Teams[i, 4],
                        source.SaveData.Teams[i, 5],
                        source.SaveData.Teams[i, 6]
                    );
                }
            }
        }

    }
}