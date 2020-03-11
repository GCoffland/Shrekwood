using System;
using System.Xml;
using System.Collections.Generic;

namespace DeadWood
{
    // Responsibilities: Holds roles and lets players act on them
    public static class XMLLoader
    {
        private static XmlDocument doc;

        static XMLLoader()
        {
            doc = new XmlDocument();
        }

        public static List<MovieSet> LoadSets(){
            List<MovieSet> moviesets = new List<MovieSet>();
            doc.Load("../../board.xml");
            XmlNodeList list = doc.ChildNodes;
            XmlNodeList sets = list[1].ChildNodes;
            for (int i = 0; i < sets.Count; i++)
            {
                if(sets[i].Name == "set")
                {
                    List<Role> roles = new List<Role>();
                    List<String> neighbors = new List<String>();
                    XmlNodeList setchildren = sets[i].ChildNodes;
                    int takes = 0;
                    for (int j = 0; j < setchildren.Count; j++)
                    {
                        if (setchildren[j].Name == "parts")
                        {
                            XmlNodeList parts = setchildren[j].ChildNodes;
                            for(int k = 0; k < parts.Count; k++)
                            {
                                roles.Add(new Role(parts[k].Attributes[0].Value, Int32.Parse(parts[k].Attributes[1].Value), parts[k].ChildNodes[1].InnerText, false));
                            }
                        }
                        if (setchildren[j].Name == "neighbors")
                        {
                            XmlNodeList neighborlist = setchildren[j].ChildNodes;
                            for (int k = 0; k < neighborlist.Count; k++)
                            {
                                neighbors.Add(neighborlist[k].Attributes[0].Value);
                            }
                        }
                        if (setchildren[j].Name == "takes")
                        {
                            XmlNodeList neighborlist = setchildren[j].ChildNodes;
                            takes = Int32.Parse(neighborlist[0].Attributes[0].Value);
                        }
                    }
                    moviesets.Add(new MovieSet(list[1].ChildNodes[i].Attributes[0].Value, roles, neighbors, takes));
                }
            }
            return moviesets;
        }

        public static List<Upgrade> LoadUpgrades(){
            doc.Load("../../board.xml");
            XmlNodeList list = doc.ChildNodes;
            XmlNodeList sets = list[1].ChildNodes;
            int[,] templist = new int[5,3];
            List<Upgrade> upgrades = new List<Upgrade>();
            for (int i = 0; i < sets.Count; i++)
            {
                if (sets[i].Name == "Castle")
                {
                    XmlNodeList officechildren = sets[i].ChildNodes;
                    for (int j = 0; j < officechildren.Count; j++)
                    {
                        if (officechildren[j].Name == "upgrades")
                        {
                            XmlNodeList upgradelist = officechildren[j].ChildNodes;
                            for (int k = 0; k < upgradelist.Count; k++)
                            {
                                if(upgradelist[k].Attributes[1].Value == "dollar")
                                {
                                    int level = Int32.Parse(upgradelist[k].Attributes[0].Value);
                                    int amount = Int32.Parse(upgradelist[k].Attributes[2].Value);
                                    templist[level - 2, 0] = level;
                                    templist[level - 2, 1] = amount;
                                }
                                else if(upgradelist[k].Attributes[1].Value == "credit")
                                {
                                    int level = Int32.Parse(upgradelist[k].Attributes[0].Value);
                                    int amount = Int32.Parse(upgradelist[k].Attributes[2].Value);
                                    templist[level - 2, 2] = amount;
                                }
                                else
                                {
                                    Console.WriteLine("Uh oh, LoadingCastingOffice() messed up");
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < templist.GetLength(0); i++)
            {
                upgrades.Add(new Upgrade(templist[i,0], templist[i,1], templist[i,2]));
            }

            return upgrades;
        }

        public static List<String> LoadOfficeNeighbors()
        {
            doc.Load("../../board.xml");
            XmlNodeList list = doc.ChildNodes;
            XmlNodeList sets = list[1].ChildNodes;
            int[,] templist = new int[5, 3];
            List<String> neighbors = new List<String>();
            for (int i = 0; i < sets.Count; i++)
            {
                if (sets[i].Name == "Castle")
                {
                    XmlNodeList officechildren = sets[i].ChildNodes;
                    for (int j = 0; j < officechildren.Count; j++)
                    {
                        if (officechildren[j].Name == "neighbors")
                        {
                            XmlNodeList neighborlist = officechildren[j].ChildNodes;
                            for (int k = 0; k < neighborlist.Count; k++)
                            {
                                neighbors.Add(neighborlist[k].Attributes[0].Value);
                            }
                        }
                    }
                }
            }

            return neighbors;
        }

        public static List<String> LoadTrailerNeighbors()
        {
            doc.Load("../../board.xml");
            XmlNodeList list = doc.ChildNodes;
            XmlNodeList sets = list[1].ChildNodes;
            int[,] templist = new int[5, 3];
            List<String> neighbors = new List<String>();
            for (int i = 0; i < sets.Count; i++)
            {
                if (sets[i].Name == "Swamp")
                {
                    XmlNodeList trailerchildren = sets[i].ChildNodes;
                    for (int j = 0; j < trailerchildren.Count; j++)
                    {
                        if (trailerchildren[j].Name == "neighbors")
                        {
                            XmlNodeList neighborlist = trailerchildren[j].ChildNodes;
                            for (int k = 0; k < neighborlist.Count; k++)
                            {
                                neighbors.Add(neighborlist[k].Attributes[0].Value);
                            }
                        }
                    }
                }
            }
            return neighbors;
        }

        public static List<SceneCard> LoadCards()
        {
            doc.Load("../../cards.xml");
            XmlNodeList list = doc.ChildNodes;
            XmlNodeList cards = list[1].ChildNodes;
            List<SceneCard> cardlist = new List<SceneCard>();
            for(int i = 0; i < cards.Count; i++)
            {
                XmlNodeList roles = cards[i].ChildNodes;
                List<Role> rolelist = new List<Role>();
                for (int j = 0; j < roles.Count; j++)
                {
                    if(roles[j].Name == "part")
                    {
                        rolelist.Add(new Role(roles[j].Attributes[0].Value, Int32.Parse(roles[j].Attributes[1].Value), roles[j].ChildNodes[1].InnerText, true));
                    }
                }
                cardlist.Add(new SceneCard(cards[i].Attributes[0].Value, Int32.Parse(cards[i].Attributes[2].Value), rolelist));
            }
            return cardlist;
        }
    }
}
