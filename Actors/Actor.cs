using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    public abstract class Actor
    {
        public string _name { get; protected set; }
        protected int _maxHealth;
        protected int _health;
        protected int _damage;

        public virtual void Attack(Actor enemy)
        {
            // Attack enemy with damage +/- 5
            int damage = enemy.TakeDamage(GetRandomDamage(5));

            Console.WriteLine(enemy._name + " takes " + damage + "dmg from " + _name);
        }

        public virtual void PrintStats()
        {
            Console.WriteLine("[" + _name + " " + _health + "/" + _maxHealth + "hp]");
        }

        public virtual int TakeDamage(int damage)
        {
            _health -= damage;
            return damage;
        }

        public virtual bool isAlive()
        {
            return _health > 0;
        }

        protected virtual int GetRandomDamage(int deviation)
        {
            Random rand = new Random();
            return rand.Next(_damage - deviation, _damage + deviation);
        }
    }
}
