using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GameEngine.GameModels
{
    public class Trader : LivingEntity
    {
        public int ID { get; }

        public Trader(int id, string name) : base(name, 9999, 9999, 18, 9999)
        {
            ID = id;
        }

    }
}
