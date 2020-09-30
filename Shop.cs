using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Actors;

namespace TextRPG
{
    // Used to create a shop for all your item needs
    class Shop
    {
        // The shop's inventory can be publicly read, but only privately set
        public Inventory _inventory { get; private set; }

        // Default constructor
        public Shop()
        {
            _inventory = new Inventory(3);
            _inventory.SetItemAtIndex(0, new Item("Health Potion", 20, "You've healed 20 hp!", 30));
            _inventory.SetItemAtIndex(1, new Item("Armor Potion", 5, "Your armor has increased by 5", 40));
            _inventory.SetItemAtIndex(2, new Item("Damage Potion", 10, "Your damage has increased by 10", 20));
        }

        // Attempt to sell an item to a player
        public bool SellItem(int index, Player player)
        {
            return player.BuyItem(_inventory.GetContents()[index]);
        }
    }
}
