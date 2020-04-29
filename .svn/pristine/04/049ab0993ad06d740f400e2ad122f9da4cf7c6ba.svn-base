using GameEngine.Factories;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GameEngine.GameModels
{
    #region Properties
    public class Player : LivingEntity
    {
        private int _experiencePoints;
        private string _characterClass;
        private int _intelligence;
        private int _strength;
        private int _wisdom;
        private int _constitution;
        private int _charisma;
        private int _attributePoints;
        private string _sex;
        private string _race;
        private int _maxAttributePoints;


        public string CharacterClass
        {
            get => _characterClass;
            set
            {
                _characterClass = value;
                OnPropertyChanged();
            }
        }
        public string Sex
        {
            get => _sex; 
            set
            {
                _sex = value;
                OnPropertyChanged();
            }
        }
        public string Race
        {
            get => _race; 
            set
            {
                _race = value;
                OnPropertyChanged();
                //CharacterRace currentrace = CharacterRaceFactory.GetRace(_race);
            }
        }

        public int Strength
        {
            get => _strength; 
            set
            {
                _strength = value;
                OnPropertyChanged();
            }
        }

        public int Intelligence
        {
            get => _intelligence; 
            set
            {
                _intelligence = value;
                OnPropertyChanged();
            }
        }

        public int Wisdom
        {
            get => _wisdom; 
            set
            {
                _wisdom = value;
                OnPropertyChanged();
            }
        }

        public int Constitution
        {
            get => _constitution; 
            set
            {
                _constitution = value;
                OnPropertyChanged();
            }
        }


        public int Charisma
        {
            get => _charisma; 
            set
            {
                _charisma = value;
                OnPropertyChanged();
            }
        }
        public int AttributePoints
        {
            get => _attributePoints; 
            set
            {
                _attributePoints = value;
                OnPropertyChanged();
            }
        }
        public int MaxAttributePoints
        {
            get => _maxAttributePoints; 
            set
            {
                _maxAttributePoints = value;
                OnPropertyChanged();
            }
        }
        public int ExperiencePoints
        {
            get => _experiencePoints;
            private set 
            {
                _experiencePoints = value;
                OnPropertyChanged();

                SetLevelAndMaximumHitPoints();
            }
        }

        public ObservableCollection<QuestStatus> Quests { get; }
        public ObservableCollection<Recipe> Recipes { get; }

        #endregion

        public event EventHandler OnLeveledUp;

        public Player(string name, string characterClass, int experiencePoints,
                      int maximumHitPoints, int currentHitPoints, int gold, int strength, int intelligence, int wisdom, int constitution, int dexterity, int charisma, int attributePoints) :
            base(name, maximumHitPoints, currentHitPoints, dexterity, gold)
        {
            CharacterClass = characterClass;
            ExperiencePoints = experiencePoints;
            Strength = strength;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Constitution = constitution;
            Charisma = charisma;
            AttributePoints = attributePoints;
            MaxAttributePoints = 30;

            Quests = new ObservableCollection<QuestStatus>();
            Recipes = new ObservableCollection<Recipe>();
        }

        public void AddExperience(int experiencePoints)
        {
            ExperiencePoints += experiencePoints;
        }
        public void LearnRecipe(Recipe recipe)
        {
            if (!Recipes.Any(r => r.ID == recipe.ID))
            {
                Recipes.Add(recipe);
            }
        }

        private void SetLevelAndMaximumHitPoints()
        {
            int originalLevel = Level;

            Level = (ExperiencePoints / 100) + 1;

            if (Level != originalLevel)
            {
                MaximumHitPoints = Level * 10;

                OnLeveledUp?.Invoke(this, System.EventArgs.Empty);
            }
        }
       

        public CharacterRace GetCurrentRaceInfo(string race)
        {
            return CharacterRaceFactory.GetRace(race);
        }



        /*  Do for Race?????
        public Monster GetMonster()
        {
            if (!MonstersHere.Any())
            {
                return null;
            }

            // Total the percentages of all monsters at this location.
            int totalChances = MonstersHere.Sum(m => m.ChanceOfEncountering);

            // Select a random number between 1 and the total (in case the total chances is not 100).
            int randomNumber = RandomNumberGenerator.NumberBetween(1, totalChances);

            // Loop through the monster list, 
            // adding the monster's percentage chance of appearing to the runningTotal variable.
            // When the random number is lower than the runningTotal,
            // that is the monster to return.
            int runningTotal = 0;

            foreach (MonsterEncounter monsterEncounter in MonstersHere)
            {
                runningTotal += monsterEncounter.ChanceOfEncountering;

                if (randomNumber <= runningTotal)
                {
                    return MonsterFactory.GetMonster(monsterEncounter.MonsterID);
                }
            }

            // If there was a problem, return the last monster in the list.
            return MonsterFactory.GetMonster(MonstersHere.Last().MonsterID);
        }
        */


    }
}
