using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    class Inventory
    {
        private Item[] _inventory;
        
        public Inventory(int inventorySize)
        {
            _inventory = new Item[inventorySize];
        }

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
        
        public void SetItemAtIndex(int index, Item item)
        {
            _inventory[index] = item;
        }

        public Item[] GetContents()
        {
            return _inventory;
        }

        public void ConsumeItem(int index)
        {
            _inventory[index] = null;
        }
    }
}
