using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    class Enemy : Actor
    {

        public Enemy()
        {
            _name = "Enemy";
            _maxHealth = 100;
            _health = _maxHealth;
            _damage = 5;
        }
        public Enemy(string name, int maxHealth, int damage)
        {
            _name = name;
            _maxHealth = maxHealth;
            _health = _maxHealth;
            _damage = damage;
        }
    }
}
