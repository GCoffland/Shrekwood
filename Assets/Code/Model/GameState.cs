using System;
using System.Collections.Generic;

namespace DeadWood
{
    public class GameState
    {
        public static GameState gameState;
        public int currentDay;
        public List<Location> locations;
        public List<SceneCard> reserveScenes;
        public List<SceneCard> usedScenes;
        public Player currentPlayer;
        public int numOfPlayers;
        public int numOfDays; 

        public GameState(List<SceneCard> startupScenes)
        {
            locations = new List<Location>();
            usedScenes = new List<SceneCard>();
            reserveScenes = startupScenes;
            numOfDays = 4;
            currentDay = 0;
            gameState = this;
        }

        public void randomizeLocations()
        {
            Random ran = new Random();
            for(int i = 0; i < locations.Count; i++)
            {
                if(locations[i].GetType() == typeof(MovieSet))
                {
                    MovieSet temp = ((MovieSet)locations[i]);
                    if (temp.card != null)
                    {
                        usedScenes.Add(temp.card);
                        temp.card = null;
                    }
                    int num = ran.Next(0, reserveScenes.Count);
                    temp.card = reserveScenes[num];
                    reserveScenes.RemoveAt(num);
                    temp.resetShotCounters();
                }
            }
        }

        public void resetPlayerLocations()
        {
            Player temp = currentPlayer;
            for(int i = 0; i < numOfPlayers; i++)
            {
                if(temp.currentLocation != null)
                {
                    temp.currentLocation.playersAtLocation.Remove(temp);
                    temp.currentLocation = null;
                }
                if(temp.currentRole != null)
                {
                    temp.currentRole.currentPlayer = null;
                    temp.currentRole = null;
                }
                temp.currentLocation = getLocationByName("Swamp");
                temp = temp.nextPlayer;
            }
        }

        public Location getLocationByName(String inname)
        {
            for(int i = 0; i < locations.Count; i++)
            {
                if(locations[i].name == inname)
                {
                    return locations[i];
                }
            }
            return null;
        }

        public Role getRoleByName(String inname)
        {
            foreach(Location l in locations)
            {
                if(l.GetType() == typeof(MovieSet))
                {
                    Role ret = ((MovieSet)l).getRoleByName(inname);
                    if(ret != null)
                    {
                        return ret;
                    }
                }
            }
            return null;
        }

        public bool CheckForEndOfDay()
        {
            int count = 0;
            foreach(Location l in locations)
            {
                if(l.GetType() == typeof(MovieSet))
                {
                    if(((MovieSet)l).card != null)
                    {
                        count++;
                    }
                }
            }
            if(count == 1)
            {
                return true;
            }else if(count <= 0)
            {
                Console.WriteLine("ITS A BUG! SQUASH IT!!!!");
            }
            return false;
        }

        public void RemoveAllSceneCards()
        {
            foreach (Location l in locations)
            {
                if (l.GetType() == typeof(MovieSet))
                {
                    if(((MovieSet)l).card != null)
                    {
                        ((MovieSet)l).RemoveSceneCard();
                    }
                }
            }
        }

        public void EndDay()
        {
            resetPlayerLocations();
            RemoveAllSceneCards();
            currentDay++;
        }

        public void StartDay()
        {
            resetPlayerLocations();
            randomizeLocations();
        }

        public List<Tuple<String, int>> CalculateScores()
        {
            List<Tuple<String, int>> playerscores = new List<Tuple<String, int>>();
            Player curr = currentPlayer;
            for(int i = 0; i < numOfPlayers; i++)
            {
                playerscores.Add(new Tuple<String, int>(curr.playerName, curr.rank * 5 + curr.dollars + curr.credits));
            }
            return playerscores;
        }

        public void nextPlayer()
        {
            currentPlayer.hasPerformedAction = false;
            currentPlayer = currentPlayer.nextPlayer;
        }
    }
}