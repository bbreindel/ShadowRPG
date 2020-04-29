using GameEngine.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.GameModels
{
    public class GameItem
    {
        public enum ItemCategory
        {
            Armor,
            Food,
            Ingredient,
            Miscellaneous,
            Potion,
            Weapon,
            Consumable
        }
        public ItemCategory Category { get; }
        public int ItemTypeID { get; }
        public string Name { get; }
        public int BuyPrice { get; }
        public int SellPrice { get; }
        public int Durability { get; set; }
        public bool IsUnique { get; }
        public IAction Action { get; set; }



        public GameItem(ItemCategory category, int itemTypeID, string name, int buyPrice, int sellPrice, int durability = 0, bool isUnique = false, IAction action = null)
        {
            Category = category;
            ItemTypeID = itemTypeID;
            Name = name;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            Durability = durability;
            IsUnique = isUnique;
            Action = action;

        }
        public void PerformAction(LivingEntity actor, LivingEntity target)
        {
            Action?.Execute(actor, target);
        }
        public GameItem Clone()
        {
            return new GameItem(Category, ItemTypeID, Name, BuyPrice, SellPrice, Durability, IsUnique, Action);
        }
    }
}
