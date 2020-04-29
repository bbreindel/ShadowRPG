using GameEngine.Actions;
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
    public static class CharacterClassFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\CharacterClass.xml";

        private static readonly List<CharacterClass> _characterClasses = new List<CharacterClass>();

        static CharacterClassFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                string rootImagePath =
    data.SelectSingleNode("/CharacterClasses")
        .AttributeAsString("RootImagePath");

                LoadClassesFromNodes(data.SelectNodes("/CharacterClasses/Class"), rootImagePath);
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }

        private static void LoadClassesFromNodes(XmlNodeList nodes, string rootImagePath)
        {
            if (nodes == null)
            {
                return;
            }

            foreach (XmlNode node in nodes)
            {
                CharacterClass characterClass =
                    new CharacterClass(node.AttributeAsInt("ID"),
                                node.AttributeAsString("Name"),
                                node.SelectSingleNode("./Description")?.InnerText ?? "",
                                 $".{rootImagePath}{node.AttributeAsString("ImageName")}",
                                 node.SelectSingleNode("./Pro")?.InnerText ?? "",
                                 node.SelectSingleNode("./Con")?.InnerText ?? "");


                _characterClasses.Add(characterClass);
            }
        }

        public static CharacterClass GetClass(string name)
        {
            return _characterClasses.FirstOrDefault(r => r.Name == name)?.GetNewCInstance();
        }

        public static string GetClassName(string name)
        {
            return _characterClasses.FirstOrDefault(r => r.Name == name)?.GetRaceName();
        }

        public static ObservableCollection<CharacterClass> GetAllClasses()
        {
            ObservableCollection<CharacterClass> tempRaces = new ObservableCollection<CharacterClass>(); ;

            foreach (CharacterClass cc in _characterClasses)
            {
                tempRaces.Add(cc);

            }
            return tempRaces;
        }
    }
}
