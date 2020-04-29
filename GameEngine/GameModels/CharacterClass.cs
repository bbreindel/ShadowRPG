using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.GameModels
{
    public class CharacterClass
    {
        public int ID { get; }
        public string Name { get; }
        public string Description { get; }
        public string ImageName { get; }

        public string CharClassPro { get; }

        public string CharClassCon { get; }

        public CharacterClass(int id, string name, string description, string imageName, string classpro, string classcon)
        {
            ID = id;
            Name = name;
            Description = description;
            ImageName = imageName;
            CharClassPro = classpro;
            CharClassCon = classcon;
        }





        public CharacterClass GetNewCInstance()
        {
            // "Clone" this monster to a new Monster object
            CharacterClass updateClass =
                new CharacterClass(ID, Name, Description, ImageName, CharClassPro, CharClassCon);

            return updateClass;
        }

        public string GetRaceName()
        {
            // "Clone" this monster to a new Monster object
            CharacterClass updateClass =
                new CharacterClass(ID, Name, Description, ImageName, CharClassPro, CharClassCon);

            return updateClass.Name;
        }
    }
}
