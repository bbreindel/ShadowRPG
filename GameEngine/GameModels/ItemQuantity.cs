using GameEngine.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.GameModels
{
    public class ItemQuantity
    {
        public int ItemID { get; }
        public int Quantity { get; }

        public string QuantityItemDescription =>
            $"{Quantity} {ItemFactory.ItemName(ItemID)}";

        public ItemQuantity(int itemID, int quantity)
        {
            ItemID = itemID;
            Quantity = quantity;
        }
    }
}
