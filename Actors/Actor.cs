using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    // Base class for both Player and Enemy.
    // Contains all functionality that the two share
    public abstract class Actor
    {
        // The actor's name. Can be read publicly, but only privately set
        public string _name { get; protected set; }

        protected int _maxHealth;
        protected int _health;
        protected int _damage;

        // Attack another actor. This function calls the given actor's TakeDamage function and gives it
        //  a randomized damage based on a base damage and allowed deviation
        public virtual void Attack(Actor enemy)
        {
            // Attack enemy with damage +/- 5
            int damage = enemy.TakeDamage(GetRandomDamage(5));

            Console.WriteLine(enemy._name + " takes " + damage + "dmg from " + _name);
        }

        // Print's the actor's stats
        public virtual void PrintStats()
        {
            Console.WriteLine("[" + _name + " " + _health + "/" + _maxHealth + "hp]");
        }

        // Reduces the actor's health by a given ammount, not allowing it to fall below zero.
        // Returns the damage dealt for the purpose of calculating damage reduction if necessary in
        //  a derived class
        public virtual int TakeDamage(int damage)
        {
            _health -= damage;
            Math.Clamp(_health, 0, _maxHealth);
            return damage;
        }

        // Returns whether the actor is considered alive
        public virtual bool isAlive()
        {
            return _health > 0;
        }

        // Used by this and derived classes to calculate a random damage based on a deviation
        protected virtual int GetRandomDamage(int deviation)
        {
            Random rand = new Random();
            return rand.Next(_damage - deviation, _damage + deviation);
        }
    }
}
