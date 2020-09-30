using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TextRPG
{
    // Used to store the information about a given item
    class Item
    {
        // Store the item's name and gold value. Both can be publicly read, but privately set
        public string _name { get; private set; }
        public int _goldValue { get; private set; }

        private int _buff;
        private string _buffMessage;

        // Default constructor. All items must be initialized with a name and stats
        public Item(string name, int buff, string buffMessage, int goldValue)
        {
            _name = name;
            _buff = buff;
            _buffMessage = buffMessage;
            _goldValue = goldValue;
        }

        // Get the raw info as a string array for saving
        public string[] GetRawStats()
        {
            string[] output = new string[4];
            output[0] = _name;
            output[1] = _goldValue.ToString();
            output[2] = _buff.ToString();
            output[3] = _buffMessage;
            return output;
        }

        // Get a string to display this item in a store
        public string GetStoreString()
        {
            return "[" + _name + ": " + _goldValue + "gp]";
        }

        // Returns the buff to be applied and writes a the Item's message to the console
        public int ApplyBuff()
        {
            Console.WriteLine(_buffMessage);
            return _buff;
        }
    }
}
