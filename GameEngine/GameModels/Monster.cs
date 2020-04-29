using GameEngine.Factories;
using GameEngine.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GameEngine.GameModels
{
    public class Monster : LivingEntity
    {

        /*  public string ImageName { get; }
          public int RewardExperiencePoints { get; }

          public Monster(string name, string imageName,
                         int maximumHitPoints, int currentHitPoints,
                         int rewardExperiencePoints, int gold) :
              base(name, maximumHitPoints, currentHitPoints, gold)
          {
              ImageName = $"C:\\Users\\Bill\\Projects\\FIRSTRPG\\FIRSTRPG\\GameEngine\\Images\\Monsters\\{imageName}";
              RewardExperiencePoints = rewardExperiencePoints;

          } OLD*/

        private readonly List<ItemPercentage> _lootTable =
            new List<ItemPercentage>();

        public int ID { get; }
        public string ImageName { get; }
        public int RewardExperiencePoints { get; }

        public Monster(int id, string name, string imageName,
                               int maximumHitPoints, int dexterity,
                               GameItem currentWeapon,
                               int rewardExperiencePoints, int gold) :
                    base(name, maximumHitPoints, maximumHitPoints, dexterity, gold)
        {
            ID = id;
            ImageName = imageName;
            CurrentWeapon = currentWeapon;
            RewardExperiencePoints = rewardExperiencePoints;
        }

        public void AddItemToLootTable(int id, int percentage)
        {
            // Remove the entry from the loot table,
            // if it already contains an entry with this ID
            _lootTable.RemoveAll(ip => ip.ID == id);

            _lootTable.Add(new ItemPercentage(id, percentage));
        }

        public Monster GetNewInstance()
        {
            // "Clone" this monster to a new Monster object
            Monster newMonster =
                new Monster(ID, Name, ImageName, MaximumHitPoints, Dexterity,
                            CurrentWeapon, RewardExperiencePoints, Gold);

            foreach (ItemPercentage itemPercentage in _lootTable)
            {
                // Clone the loot table - even though we probably won't need it
                newMonster.AddItemToLootTable(itemPercentage.ID, itemPercentage.Percentage);

                // Populate the new monster's inventory, using the loot table
                if (RandomNumberGenerator.NumberBetween(1, 100) <= itemPercentage.Percentage)
                {
                    newMonster.AddItemToInventory(ItemFactory.CreateGameItem(itemPercentage.ID));
                }
            }

            return newMonster;
        }
    }
}
