using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TextRPG.Actors;

namespace TextRPG
{
    // Used as an easy reference to which part of the game we are running in
    public enum GameState
    {
        MainMenu,
        CharacterCreation,
        Game,
        GameOver
    }
    
    // Main class for all game logic
    class Game
    {
        // Determines when we break out of Update()
        bool _gameOver = false;

        // Determines which part of update will be run this loop
        GameState _gameState;

        // Reference to the player
        Player _player;

        // Array of enemies to fight
        Enemy[] _enemies;

        // Store which round we are on
        int _round = 0;

        // Path to the save file
        string _savePath = "save.txt";

        // Main function for this class. Responsible for the game loop
        public void Run()
        {
            Start();
            while(!_gameOver)
            {
                Update();
            }
            End();
        }

        // Runs first. Used for initializing objects
        private void Start()
        {
            _gameState = GameState.MainMenu;
            _player = new Player();
            _enemies = new Enemy[5];
            for (int i = 0; i < _enemies.Length; i++)
            {
                string name = "Enemy " + (i + 1);
                int maxHealth = (int) Math.Round(100 + (i * 20f));
                int damage = (int) Math.Round(8 + (i * 2f));

                _enemies[i] = new Enemy(name, maxHealth, damage);
            }
        }

        // Runs in a loop. Used for all game logic
        private void Update()
        {

            // Show different screens based on which gamestate we are in
            switch(_gameState)
            {
                // Code for the main menu
                #region MAIN MENU
                case GameState.MainMenu:
                    Console.Clear();

                    // Reset round
                    _round = 0;

                    // Draw the main menu from the AsciiArt class
                    AsciiArt.DrawMainMenu();
                    Console.WriteLine();

                    // List main menu options
                    Console.WriteLine(" [1] Continue");
                    Console.WriteLine(" [2] New Game");
                    Console.WriteLine(" [3] Quit");
                    char mainMenuInput = Input.GetSelection(3);
                    switch (mainMenuInput)
                    {
                        // If player chose 1, load the previous game and continue
                        case '1':
                            if (File.Exists(_savePath))
                            {
                                Load();
                                _gameState = GameState.Game;
                            }
                            break;

                        // If player chose 2, go to character creation
                        case '2':
                            _gameState = GameState.CharacterCreation;
                            _round = 0;
                            break;

                        // If player chose 3, end the game loop and move on to End()
                        case '3':
                            _gameOver = true;
                            return;
                    }
                    break;
                #endregion

                // Code for character creation
                #region CHARACTER CREATION
                case GameState.CharacterCreation:
                    Console.Clear();

                    // Get player name
                    Console.WriteLine("[Character Creation]\n");
                    Console.WriteLine("What is your name?");
                    Console.Write("> ");
                    string name = Console.ReadLine();
                    Console.WriteLine("\nWelcome, " + name + "!");
                    
                    // Initialize a new player with the given name and a max health of 100
                    _player = new Player(name, 100);

                    // Confirm finished with character creation
                    Console.WriteLine("Are you ready to begin?");
                    Console.WriteLine(" [1] Yes");
                    Console.WriteLine(" [2] No");
                    char input = Input.GetSelection(2);

                    // If player is ready, change gamestate to game and save
                    if (input == '1')
                        _gameState = GameState.Game;
                    Save(_player);
                    break;
                #endregion

                // Main game code
                #region GAME
                case GameState.Game:
                    Console.Clear();

                    // Send player to shop
                    DoShop(_player);

                    // Start Battle with an enemy based on the round
                    if (_round >= _enemies.Length)
                        _round = 0;
                    DoBattle(_player, _enemies[_round]);

                    // Increment the round
                    _round++;
                    
                    if (_round >= _enemies.Length)
                    {
                        Console.WriteLine("\n\nYou win!");
                        _gameOver = true;
                    }

                    // Save after every round
                    Save(_player);
                    break;
                #endregion

                // If somehow the gamestate gets messed up, send the player back to the main menu
                default:
                    _gameState = GameState.MainMenu;
                    break;
            }
        }

        private void End()
        {
            Console.WriteLine("Thank you for playing!");
            PressAnyKeyToContinue();
        }

        // Used for saving the player's stats and our round number
        private void Save(Player player)
        {
            // Get player variables
            string[] playerRawVariables = player.GetRawVariables();

            // Write player variables to file
            StreamWriter writer = new StreamWriter(_savePath);
            for (int i = 0; i < playerRawVariables.Length; i++)
            {
                if (playerRawVariables[i] != null)
                    writer.WriteLine(playerRawVariables[i]);
            }

            // Save which round we're on
            writer.WriteLine(_round);

            writer.Close();
        }

        // Used to load the player's stats again and put us on the right round
        private void Load()
        {
            if (!File.Exists(_savePath))
                throw new FileNotFoundException();

            string[] rawInput = new string[File.ReadAllLines(_savePath).Count()];
            StreamReader reader = new StreamReader(_savePath);
            int counter = 0;
            while (!reader.EndOfStream)
            {
                rawInput[counter] = reader.ReadLine();
                counter++;
            }

            _player.SetRawVariables(rawInput);
            _round = int.Parse(rawInput[rawInput.Length - 1]);
            reader.Close();
        }

        // Send the player to the shop to buy items
        private void DoShop(Player player)
        {
            Shop shop = new Shop();
            while (true)
            {
                Console.Clear();
                player.PrintStats();
                player._inventory.PrintContents();
                Console.WriteLine();
                Console.WriteLine("Welcome to the Shop!");
                Console.WriteLine("Please select an item to purchase. Enter 'q' to leave shop");
                shop._inventory.PrintContents();
                char input = Input.GetSelection(shop._inventory.GetContents().Length, true);
                if (input == 'q')
                    break;
                int itemIndex = (int) Char.GetNumericValue(input) - 1;
                shop.SellItem(itemIndex, player);
                Console.WriteLine();
                player._inventory.PrintContents();
                PressAnyKeyToContinue();
            }
        }

        // Start a battle between the player and a given actor
        private void DoBattle(Player player, Actor enemy)
        {
            while (player.isAlive() && enemy.isAlive())
            {
                Console.Clear();
                Console.WriteLine("It's a fight!\n");

                // Show stats for both combattants
                player.PrintStats();
                enemy.PrintStats();
                Console.WriteLine();

                // Give player their options
                Console.WriteLine(" [1] Attack");
                Console.WriteLine(" [2] Use Item");
                char input = Input.GetSelection(2);

                // Player turn
                if (input == '1')
                    player.Attack(enemy);
                else if (input == '2')
                {
                    Console.WriteLine();
                    player._inventory.PrintContents();
                    Console.WriteLine();
                    Console.WriteLine("Select an Item to use");
                    int itemIndex = (int)Char.GetNumericValue(Input.GetSelection(player._inventory.GetContents().Length)) - 1;
                    player.UseItem(itemIndex);
                }

                // Enemy turn
                if (enemy.isAlive())
                    enemy.Attack(player);
                else
                    Console.WriteLine(enemy._name + " is dead!");
                PressAnyKeyToContinue();
            }

            // Check if player is alive
            if (_player.isAlive())
            {
                Console.Clear();
                Console.WriteLine("\nYou win!");

                // Random gold loot;
                Random rand = new Random();
                int baseGoldLoot = 20;
                int deviation = 10;
                int goldLoot = rand.Next(baseGoldLoot - deviation, baseGoldLoot + deviation);
                player.AddGold(goldLoot);
                Console.WriteLine("You've found " + goldLoot + "gp on the corpse!");
            }
            else
            {
                // Inform the player they died and go back to the main menu
                Console.Clear();
                Console.WriteLine("\nYou died!");
                _gameState = GameState.MainMenu;
            }
            PressAnyKeyToContinue();
        }

        // Quick and easy helper to do exactly what it says on the tin
        private void PressAnyKeyToContinue()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}