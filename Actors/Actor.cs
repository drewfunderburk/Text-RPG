using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG
{
    public abstract class Actor
    {
        protected int _health;

        public abstract void PrintStats();
        public abstract void Attack();
        public abstract void TakeDamage();
    }
}
