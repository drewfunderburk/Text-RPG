using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG.Actors
{
    class Player : Actor
    {
        #region CONSTRUCTORS
        public Player()
        {
            _health = 100;
        }

        public Player(int health)
        {
            _health = health;
        }
        #endregion

        public override void Attack()
        {
            throw new NotImplementedException();
        }

        public override void PrintStats()
        {
            throw new NotImplementedException();
        }

        public override void TakeDamage()
        {
            throw new NotImplementedException();
        }


    }
}
