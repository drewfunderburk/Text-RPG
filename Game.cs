using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TextRPG.Actors;

namespace TextRPG
{
    public enum GameState
    {
        MainMenu,
        CharacterCreation,
        Game,
        GameOver
    }
    class Game
    {
        bool _gameOver = false;
        GameState _gameState;
        Player _player;
        Enemy[] _enemies;
        int _turn = 0;
        string _savePath = "save.txt";
        public void Run()
        {
            Start();
            while(!_gameOver)
            {
                Update();
            }
            End();
        }

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

        private void Update()
        {
            switch(_gameState)
            {
                #region MAIN MENU
                case GameState.MainMenu:
                    Console.Clear();
                    AsciiArt.DrawMainMenu();
                    Console.WriteLine();
                    Console.WriteLine(" [1] Continue");
                    Console.WriteLine(" [2] New Game");
                    Console.WriteLine(" [3] Quit");
                    char mainMenuInput = Input.GetSelection(3);
                    switch (mainMenuInput)
                    {
                        case '1':
                            if (File.Exists(_savePath))
                            {
                                Load();
                                _gameState = GameState.Game;
                            }
                            break;
                        case '2':
                            _gameState = GameState.CharacterCreation;
                            _turn = 0;
                            break;
                        case '3':
                            _gameOver = true;
                            return;
                    }
                    break;
                #endregion

                #region CHARACTER CREATION
                case GameState.CharacterCreation:
                    Console.Clear();

                    // Get player name
                    Console.WriteLine("[Character Creation]\n");
                    Console.WriteLine("What is your name?");
                    Console.Write("> ");
                    string name = Console.ReadLine();
                    Console.WriteLine("\nWelcome, " + name + "!");
                    
                    _player = new Player(name, 100);

                    // Confirm finished with character creation
                    Console.WriteLine("Are you ready to begin?");
                    Console.WriteLine(" [1] Yes");
                    Console.WriteLine(" [2] No");
                    char input = Input.GetSelection(2);

                    if (input == '1')
                        _gameState = GameState.Game;
                    Save(_player);
                    break;
                #endregion

                #region GAME
                case GameState.Game:
                    Console.Clear();

                    // Send player to shop
                    DoShop(_player);

                    // Start Battle with generic enemy
                    DoBattle(_player, _enemies[_turn]);

                    _turn++;
                    
                    if (_turn >= _enemies.Length)
                    {
                        Console.WriteLine("\n\nYou win!");
                        _gameOver = true;
                    }

                    Save(_player);
                    break;
                #endregion

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

            // Save which turn we're on
            writer.WriteLine(_turn);

            writer.Close();
        }

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
            _turn = int.Parse(rawInput[rawInput.Length - 1]);
            reader.Close();
        }

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

        private void DoBattle(Player player, Actor enemy)
        {
            while (player.isAlive() && enemy.isAlive())
            {
                Console.Clear();
                Console.WriteLine("It's a fight!\n");
                player.PrintStats();
                enemy.PrintStats();
                Console.WriteLine();
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
                Console.Clear();
                Console.WriteLine("\nYou died!");
                _gameState = GameState.MainMenu;
            }
            PressAnyKeyToContinue();
        }

        private void PressAnyKeyToContinue()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}