using System;
using System.Collections.Generic;

namespace DeadWood
{
    // Responsibilities: Play deadwood and have fun
    public class Player
    {
        public String playerName;
        public Player nextPlayer;  // This is done because turns are based on a linked list 
        public int rank;
        public int dollars;
        public int credits;
        private int rehearsalTokens;
        public Location currentLocation;
        public Role currentRole;
        private double karma;
        public Boolean hasPerformedAction;

        public Player(String inName)
        {
            hasPerformedAction = false;
            playerName = inName;
            rank = 1;
        }

        public HashSet<string> GetPlayerActions()
        {
            // Allow the player to do different things depending on the location
            // Store in a hashset to avoid repeats incase multiple contexts line up
            HashSet<string> possibleActions = new HashSet<string>();
            possibleActions.Add("end turn");
            if (!hasPerformedAction)
            {
                if (currentLocation.GetType() == typeof(Trailer))
                {
                    possibleActions.Add("move");
                }
                if (currentLocation.GetType() == typeof(CastingOffice))
                {
                    possibleActions.Add("upgrade");
                    possibleActions.Add("move");
                }
                if (currentLocation.GetType() == typeof(MovieSet))
                {
                    if (currentRole == null)
                    {
                        if (((MovieSet)currentLocation).card == null)
                        {
                            possibleActions.Add("move");
                        }
                        else
                        {
                            possibleActions.Add("take role");
                            possibleActions.Add("move");
                        }
                    }
                    else
                    {
                        possibleActions.Add("act");
                        if(rehearsalTokens < 6)
                        {
                            possibleActions.Add("rehearse");
                        }
                    }
                }
            }
            else
            {
                if (currentLocation.GetType() == typeof(MovieSet))
                {
                    if (currentRole == null)
                    {
                        if (((MovieSet)currentLocation).card != null)
                        {
                            possibleActions.Add("take role");
                        }
                    }
                }
            }
            return possibleActions;
        }

        public void TakeRole(Role role)
        {
            currentRole = role;
            role.currentPlayer = this;
        }

        public Tuple<Boolean,int,int> Act() // order is: success?, dollars gained, credits gained
        {
            hasPerformedAction = true;
            Boolean status = false;
            int dollarsgained = 0;
            int creditsgained = 0;
            Random ran = new Random();
            if(currentRole == null)
            {
                return null;
            }
            int dice = ran.Next(1, 7);
            dice += rehearsalTokens;
            if (dice >= ((MovieSet)currentLocation).card.budget)
            {
                status = true;
                if (currentRole.leadRole)
                {
                    creditsgained = 2;
                    credits += 2;
                    ((MovieSet)currentLocation).shotsRemaining--;
                }
                else
                {
                    creditsgained = 1;
                    dollarsgained = 1;
                    credits++;
                    dollars++;
                    ((MovieSet)currentLocation).shotsRemaining--;
                }
            }
            else
            {
                status = false;
                if (currentRole.leadRole)
                {

                }
                else
                {
                    dollarsgained = 1;
                    dollars++;
                }
            }
            return new Tuple<bool, int, int>(status, dollarsgained, creditsgained);
        }
        
        public void Rehearse()
        {
            hasPerformedAction = true;
            rehearsalTokens++;
        }

        public void SetLocation(Location inlocation)
        {
            currentLocation.playersAtLocation.Remove(this);
            currentLocation = inlocation;
            inlocation.playersAtLocation.Add(this);
            rehearsalTokens = 0;
        }

        public void Move(Location inlocation)
        {
            hasPerformedAction = true;
            SetLocation(inlocation);
        }

        public string GetPlayerName()
        {
            return playerName;
        }

        public int GetPlayerRank()
        {
            return rank;
        }
        public int GetPlayerDollars()
        {
            return dollars;
        }
        public int GetPlayerCredits()
        {
            return credits;
        }
        
        public void UpgradePlayer(int inrank)
        {
            hasPerformedAction = true;
            if (inrank > rank)
            {
                rank = inrank;
            }
        }
        public void AddCredits(int add)
        {
            credits += add;
        }
        public void AddDollars(int add)
        {
            dollars += add;
        }
    }
}