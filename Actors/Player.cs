using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG.Actors
{
    class Player : Actor
    {
        int _armor;
        public Inventory _inventory { get; private set; }

        #region CONSTRUCTORS
        public Player()
        {
            _name = "N/A";
            _maxHealth = 100;
            _health = _maxHealth;
            _damage = 10;
            _armor = 0;
            _inventory = new Inventory(3);
        }

        public Player(string name, int maxHealth)
        {
            _name = name;
            _maxHealth = maxHealth;
            _health = _maxHealth;
            _damage = 10;
            _armor = 0;
            _inventory = new Inventory(3);
        }
        #endregion

        public override int TakeDamage(int damage)
        {
            // Remove armor from damage and clamp to >0 in case armor exceeds damage
            int actualDamage = Math.Max(0, damage - _armor);

            // Reduce health and clamp
            _health -= actualDamage;
            Math.Clamp(_health, 0, _maxHealth);

            return actualDamage;
        }

        public override void PrintStats()
        {
            Console.WriteLine("[" + _name + " " + _health + "/" + _maxHealth + "hp]");
        }

        

        public void UseItem(int inventorySlot)
        {
            // Figure out what the item buff is and use it
            Item item = _inventory.GetContents()[inventorySlot];
            if (item == null)
                return;

            switch (item._name)
            {
                case "Health Potion":
                    _health += item.ApplyBuff();
                    break;
                case "Armor Potion":
                    _armor += item.ApplyBuff();
                    break;
                case "Damage Potion":
                    _damage += item.ApplyBuff();
                    break;
            }

            // Set used item to null
            _inventory.ConsumeItem(inventorySlot);
        }
    }
}
