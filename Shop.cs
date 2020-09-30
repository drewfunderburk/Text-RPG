using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Actors;

namespace TextRPG
{
    class Shop
    {
        public Inventory _inventory { get; private set; }

        public Shop()
        {
            _inventory = new Inventory(3);
            _inventory.SetItemAtIndex(0, new Item("Health Potion", 20, "You've healed 20 hp!", 30));
            _inventory.SetItemAtIndex(1, new Item("Armor Potion", 5, "Your armor has increased by 5", 40));
            _inventory.SetItemAtIndex(2, new Item("Damage Potion", 10, "Your damage has increased by 10", 20));
        }

        public bool SellItem(int index, Player player)
        {
            return player.BuyItem(_inventory.GetContents()[index]);
        }
    }
}
