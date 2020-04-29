using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.ViewModels
{
    public class ItemPercentage
    {
        public int ID { get; }
        public int Percentage { get; }

        public ItemPercentage(int id, int percentage)
        {
            ID = id;
            Percentage = percentage;
        }
    }
}
