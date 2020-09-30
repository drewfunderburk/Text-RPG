using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    class Item
    {
        public string _name { get; private set; }

        private int _buff;
        private int _goldValue;
        private string _buffMessage;

        public Item(string name, int buff, string buffMessage, int goldValue)
        {
            _name = name;
            _buff = buff;
            _buffMessage = buffMessage;
            _goldValue = goldValue;
        }

        public string GetStoreString()
        {
            return "[" + _name + ": " + _goldValue + "gp]";
        }

        public int ApplyBuff()
        {
            Console.WriteLine(_buffMessage);
            return _buff;
        }
    }
}
