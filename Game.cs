using System;
using System.Collections.Generic;
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
                    PressAnyKeyToContinue();
                    _gameState = GameState.CharacterCreation;
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
                    break;
                #endregion

                #region GAME
                case GameState.Game:
                    Console.Clear();

                    // Start Battle with generic enemy
                    Enemy enemy = new Enemy();
                    DoBattle(_player, enemy);

                    break;
                #endregion

                default:
                    _gameState = GameState.MainMenu;
                    break;
            }
        }

        private void End()
        {

        }

        private void DoShop()
        {

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
                    int itemIndex = Convert.ToInt32(Input.GetSelection(player._inventory.GetContents().Length).ToString());
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
        }
    }
}