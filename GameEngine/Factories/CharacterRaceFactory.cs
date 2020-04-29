using GameEngine.GameModels;
using GameEngine.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace GameEngine.Factories
{

    public static class CharacterRaceFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\CharacterRaces.xml";

        private static readonly List<CharacterRace> _characterRaces = new List<CharacterRace>();

        static CharacterRaceFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                string rootImagePath =
    data.SelectSingleNode("/CharacterRaces")
        .AttributeAsString("RootImagePath");

                LoadRacesFromNodes(data.SelectNodes("/CharacterRaces/Race"), rootImagePath);
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }

        private static void LoadRacesFromNodes(XmlNodeList nodes, string rootImagePath)
        {
            if (nodes == null)
            {
                return;
            }

            foreach (XmlNode node in nodes)
            {
                CharacterRace characterRace =
                    new CharacterRace(node.AttributeAsInt("ID"),
                                node.AttributeAsString("Name"),
                                node.SelectSingleNode("./Description")?.InnerText ?? "",
                                 $".{rootImagePath}{node.AttributeAsString("ImageName")}",
                                 node.SelectSingleNode("./Pro")?.InnerText ?? "",
                                 node.SelectSingleNode("./Con")?.InnerText ?? "");


                _characterRaces.Add(characterRace);
            }
        }

        public static CharacterRace GetRace(string name)
        {
            return _characterRaces.FirstOrDefault(r => r.Name == name)?.GetNewRInstance();
        }

        public static string GetRaceName(string name)
        {
            return _characterRaces.FirstOrDefault(r => r.Name == name)?.GetRaceName();
        }

        public static ObservableCollection<CharacterRace> GetAllRaces()
        {
            ObservableCollection<CharacterRace> tempRaces = new ObservableCollection<CharacterRace>(); ;

            foreach (CharacterRace cr in _characterRaces)
            {
                tempRaces.Add(cr);

            }
            return tempRaces;
        }
    }
}
