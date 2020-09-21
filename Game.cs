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
        Player player;
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
            player = new Player();
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
                    break;
                #endregion

                #region GAME
                case GameState.Game:
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

        private void PressAnyKeyToContinue()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
