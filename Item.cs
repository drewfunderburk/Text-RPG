using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TextRPG
{
    class Item
    {
        public string _name { get; private set; }
        public int _goldValue { get; private set; }

        private int _buff;
        private string _buffMessage;

        public Item(string name, int buff, string buffMessage, int goldValue)
        {
            _name = name;
            _buff = buff;
            _buffMessage = buffMessage;
            _goldValue = goldValue;
        }

        public string[] GetRawStats()
        {
            string[] output = new string[4];
            output[0] = _name;
            output[1] = _goldValue.ToString();
            output[2] = _buff.ToString();
            output[3] = _buffMessage;
            return output;
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
