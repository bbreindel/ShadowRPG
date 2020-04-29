using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.GameModels
{
    public class CharacterRace
    {
        public int ID { get; }
        public string Name { get; }
        public string Description { get; }
        public string ImageName { get; }

        public string CharRacePro { get; }

        public string CharRaceCon { get; }

        public CharacterRace(int id, string name, string description, string imageName, string racepro, string racecon)
        {
            ID = id;
            Name = name;
            Description = description;
            ImageName = imageName;
            CharRacePro = racepro;
            CharRaceCon = racecon;
        }





        public CharacterRace GetNewRInstance()
        {
            // "Clone" this monster to a new Monster object
            CharacterRace updateRace =
                new CharacterRace(ID, Name, Description, ImageName, CharRacePro, CharRaceCon);

            return updateRace;
        }

        public string GetRaceName()
        {
            // "Clone" this monster to a new Monster object
            CharacterRace updateRace =
                new CharacterRace(ID, Name, Description, ImageName, CharRacePro, CharRaceCon);

            return updateRace.Name;
        }

    }
}
