using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace TextRPG.Actors
{
    class Player : Actor
    {
        int _armor;
        int _gold;
        public Inventory _inventory { get; private set; }

        #region CONSTRUCTORS
        public Player()
        {
            _name = "N/A";
            _maxHealth = 100;
            _health = _maxHealth;
            _damage = 10;
            _armor = 0;
            _gold = 100;
            _inventory = new Inventory(3);
        }

        public Player(string name, int maxHealth)
        {
            _name = name;
            _maxHealth = maxHealth;
            _health = _maxHealth;
            _damage = 10;
            _armor = 0;
            _gold = 100;
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
            Console.WriteLine("[" + _name + " " + _health + "/" + _maxHealth + "hp " + _gold + "gp]");
        }

        public string[] GetRawVariables()
        {
            string[] output = new string[18];
            output[0] = _name;
            output[1] = _maxHealth.ToString();
            output[2] = _health.ToString();
            output[3] = _damage.ToString();
            output[4] = _armor.ToString();
            output[5] = _gold.ToString();
            int counter = 6;
            Item[] items = _inventory.GetContents();
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    string[] rawStats = items[i].GetRawStats();
                    for (int n = 0; n < rawStats.Length; n++)
                    {
                        output[counter] = rawStats[n];
                        counter++;
                    }
                }
                else
                {
                    output[counter] = "None";
                    counter++;
                }
            }

            return output;
        }

        public void SetRawVariables(string[] variables)
        {
            _name = variables[0];
            _maxHealth = int.Parse(variables[1]);
            _health = int.Parse(variables[2]);
            _damage = int.Parse(variables[3]);
            _armor = int.Parse(variables[4]);
            _gold = int.Parse(variables[5]);

            // Read inventory
            int counter = 6;
            for (int i = 0; i < _inventory.GetContents().Length; i++)
            {
                string nextLine = variables[counter];
                counter++;
                if (nextLine != "None")
                {
                    int goldValue = int.Parse(variables[counter]);
                    counter++;
                    int buff = int.Parse(variables[counter]);
                    counter++;
                    string buffMessage = variables[counter];
                    counter++;
                    _inventory.SetItemAtIndex(i, new Item(nextLine, buff, buffMessage, goldValue));
                }
            }
        }

        public void AddGold(int ammount)
        {
            _gold += ammount;
        }

        public bool BuyItem(Item item)
        {
            if (_gold >= item._goldValue)
            {
                Console.WriteLine("Choose an inventory slot");
                _inventory.PrintContents();
                int slot = (int) Char.GetNumericValue(Input.GetSelection(_inventory.GetContents().Length)) - 1;
                _inventory.SetItemAtIndex(slot, item);
                _gold -= item._goldValue;
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient gold");
                return false;
            }
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
