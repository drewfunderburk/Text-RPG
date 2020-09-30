using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    // Mainly here for extended functionality later. At the moment it's little more than it's base class
    class Enemy : Actor
    {
        // Default constructor
        public Enemy()
        {
            _name = "N/A";
            _maxHealth = 100;
            _health = _maxHealth;
            _damage = 5;
        }

        // Overloads default constructor
        public Enemy(string name, int maxHealth, int damage)
        {
            _name = name;
            _maxHealth = maxHealth;
            _health = _maxHealth;
            _damage = damage;
        }
    }
}
