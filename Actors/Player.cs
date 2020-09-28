using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG.Actors
{
    class Player : Actor
    {
        int _armor;

        #region CONSTRUCTORS
        public Player()
        {
            _name = "N/A";
            _maxHealth = 100;
            _health = _maxHealth;
            _damage = 10;
            _armor = 0;
        }

        public Player(string name, int maxHealth)
        {
            _name = name;
            _maxHealth = maxHealth;
            _health = _maxHealth;
            _damage = 10;
            _armor = 0;
        }
        #endregion

        public override int TakeDamage(int damage)
        {
            // Remove armor from damage and clamp to >0
            int actualDamage = Math.Max(0, damage - _armor);

            // Reduce health, but never below zero
            _health -= actualDamage;
            Math.Clamp(_health, 0, _maxHealth);

            return actualDamage;
        }

        public override void PrintStats()
        {
            Console.WriteLine("[" + _name + " " + _health + "/" + _maxHealth + "hp]");
        }

    }
}
