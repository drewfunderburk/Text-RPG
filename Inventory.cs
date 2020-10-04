using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    // Used to manage the inventory of anything that needs to store items
    class Inventory
    {
        // Array of Items to store the goods
        private Item[] _inventory;
        
        // Default constructor to initialize a new Inventory
        public Inventory(int inventorySize)
        {
            _inventory = new Item[inventorySize];
        }

        // Print the contents of this inventory to the console
        public void PrintContents()
        {
            for (int i = 0; i < _inventory.Length; i++)
            {
                Console.Write(" [" + (i + 1) + "] ");
                if (_inventory[i] != null)
                    Console.WriteLine(_inventory[i].GetStoreString());
                else
                    Console.WriteLine("None");
            }
        }
        
        // Set a specific item
        public void SetItemAtIndex(int index, Item item)
        {
            _inventory[index] = item;
        }

        // Returns the inventory array
        public Item[] GetContents()
        {
            return _inventory;
        }

        // Remove's an item from the inventory
        public void RemoveItem(int index)
        {
            _inventory[index] = null;
        }
    }
}
