using System;
using System.Collections.Generic;
using UnityEngine;


// Responsibilities: Provides the arbitration between the view and the model(which are the gameplay classes)
public class Controller
{
    public enum PROGRAMSTATE
    {
        PROGRAMSTARTUP,
        GAMESTARTUP,
        GAMEINPROGRESS
    };

    public static Controller cont = new Controller();
    public PROGRAMSTATE state;
    private GameState gameState;

    private Controller()
    {
        state = PROGRAMSTATE.PROGRAMSTARTUP;
    }

    public void ProgramStartup()
    {
        //view = View.GetInstance(this);
        int num = 2;//view.PromptGreeting();
        Player[] players = new Player[num];
        for (int i = 0; i < num; i++)
        {
            players[i] = new Player("Player" + i);
            if(num == 5)
            {
                players[i].AddCredits(2);
            }
            if(num == 6)
            {
                players[i].AddCredits(4);
            }
            if(num >= 7)
            {
                players[i].rank = 2;
            }
            if(i == num - 1)
            {
                players[i].nextPlayer = players[0];
            }
            if(i > 0)
            {
                players[i - 1].nextPlayer = players[i];
            }
        }
        gameState = new GameState(XMLLoader.LoadCards());
        if (num == 2 || num == 3)
        {
            gameState.numOfDays = 3;
        }
        gameState.currentPlayer = players[0];
        gameState.numOfPlayers = num;
    }

    public void GameStartUp()
    {
        //view.PromptBoardStartUp(gameState.numOfPlayers);
        gameState.locations.AddRange(XMLLoader.LoadSets());
        gameState.locations.Add(Trailer.GetInstance(XMLLoader.LoadTrailerNeighbors()));
        gameState.locations.Add(CastingOffice.GetInstance(XMLLoader.LoadUpgrades(), XMLLoader.LoadOfficeNeighbors()));
        gameState.randomizeLocations();
        gameState.resetPlayerLocations();
        //view.StartDayDisplay(gameState.currentDay, gameState.numOfDays);
    }

    public void GameUpdate()
    {
        List<string> playerCommand = new List<string>();
        //view.StartTurnDisplay(gameState.currentPlayer.playerName, gameState.currentPlayer.currentLocation.name);
        //playerCommand = view.PromptPlayerTurn(gameState.currentDay, gameState.numOfDays,gameState.currentPlayer.GetPlayerName(), gameState.currentPlayer.currentLocation.name);
        /*ProcessPlayerCommand(playerCommand);
        while (playerCommand[0] != "end turn")
        {
            //playerCommand = view.PromptPlayerTurn(gameState.currentDay, gameState.numOfDays, gameState.currentPlayer.GetPlayerName(), gameState.currentPlayer.currentLocation.name);
            ProcessPlayerCommand(playerCommand);
        }
        if (gameState.CheckForEndOfDay())
        {
            gameState.EndDay();
            if(gameState.currentDay > gameState.numOfDays)
            {
                //view.displayWinner(gameState.CalculateScores());

            }
            else
            {
                gameState.StartDay();
                //view.StartDayDisplay(gameState.currentDay, gameState.numOfDays);
            }
        }
        gameState.nextPlayer();*/
    }

    public void ProcessPlayerCommand(List <string> incommand)
    {
        switch (incommand[0])
        {
            case ("act"):
                Act(gameState.currentPlayer);
                break;
            case ("rehearse"):
                Rehearse(gameState.currentPlayer);
                break;
            case ("upgrade"):
                Upgrade(gameState.currentPlayer, incommand[1]);
                break;
            case ("move"):
                Move(gameState.currentPlayer, incommand[1]);
                break;
            case ("take role"):
                TakeRole(gameState.currentPlayer, gameState.getRoleByName(incommand[1]));
                break;
            case ("end turn"):
            case ("back"):
                break;
            default:
                break;
        }
    }
    public void Move(Player inplayer, string inlocation)
    {
        //Console.WriteLine("******CONTROLLER REACHED MOVE WITH: " + inlocation);
        gameState.currentPlayer.Move(gameState.getLocationByName(inlocation));
    }

    public void TakeRole(Player inplayer, Role inrole) // This is if a player wants to take a role
    {
        inplayer.TakeRole(inrole);
    }

    public void Act(Player inplayer)  // Just take in the player because their role can be retrieved 
    {
        //view.displayActResults(inplayer.Act());
    }

    public void Rehearse(Player inplayer)  // Just take in the player because their role can be retrieved 
    {
        inplayer.Rehearse();
    }
        
    public void Upgrade(Player inplayer, string inupgrade)  // Just take in the player because their role can be retrieved 
    {
        //Console.WriteLine("******CONTROLLER REACHED UPGRADE WITH: " + inupgrade);
        // Split the input to get the correct int at the end of inupgrade 
        bool bUsingDollars = false;
        bool bUpgradeSuccessful = false;
        int rankNumber = 1;
        if (inupgrade[1].Equals('d'))
        {
            bUsingDollars = true;
        }
        rankNumber = Int32.Parse(inupgrade[0].ToString());
        string[] separator = { "d", "c" };
        String[] resultsFromUpgrade = inupgrade.Split(separator, 2,StringSplitOptions.RemoveEmptyEntries);

        int passedInAmount = Int32.Parse(resultsFromUpgrade[1]); // Grabs the cost

        string warningUpgrade = ""; // This is for diagnosing why a player could not perform an upgrade
                                    // also set desired credits here
        if (inupgrade[0] <= inplayer.rank)
        {
            warningUpgrade = "you are equal or greater than that rank.";
        }
        if (bUsingDollars)
        {
            if (inplayer.dollars < passedInAmount)
            {
                warningUpgrade = "you do not have enough dollars for that.";
            }
            else
            {
                AddPlayerDollars(-passedInAmount);
                inplayer.UpgradePlayer(rankNumber);
                bUpgradeSuccessful = true;
            }
        }
        else
        {
            if (inplayer.credits < passedInAmount)
            {
                warningUpgrade = "you do not have enough credits for that.";
            }
            else
            {
                AddPlayerCredits(-passedInAmount);
                inplayer.UpgradePlayer(rankNumber);
                bUpgradeSuccessful = true;

            }
        }
        if(bUpgradeSuccessful)
        {
            //view.PromptUpgradeResults(true, "");
        }
        else
        {
            //view.PromptUpgradeResults(false, warningUpgrade);
        }

    }

    public HashSet<string> GetPlayerActions()
    {
        return gameState.currentPlayer.GetPlayerActions();
    }

    public List<string> GetPlayerAdjacentLocations()
    {
        return gameState.currentPlayer.currentLocation.adjacentLocations;
    }

    public HashSet<Tuple<int, int, int>> GetPlayerUpgrades()
    {
        HashSet<Tuple<int, int, int>> upgrades = CastingOffice.GetInstance().GetUpgrades();
        return upgrades;
    }

    public int GetPlayerRank()
    {
        return gameState.currentPlayer.GetPlayerRank();
    }
    public int GetPlayerDollars()
    {
        return gameState.currentPlayer.GetPlayerDollars();
    }
    public int GetPlayerCredits()
    {
        return gameState.currentPlayer.GetPlayerCredits();
    }
    public void AddPlayerDollars(int inamount)
    {
        gameState.currentPlayer.AddDollars(inamount);
    }
    public void AddPlayerCredits(int inamount)
    {
        gameState.currentPlayer.AddCredits(inamount);
    }

    public HashSet<Tuple<String, int>> GetPlayerRoles()
    {
        HashSet<Tuple<String, int>> ret = new HashSet<Tuple<String, int>>();
        Location l = gameState.currentPlayer.currentLocation;
        if(l.GetType() != typeof(MovieSet))
        {
            return null;
        }
        foreach(Tuple<String,int> t in ((MovieSet)l).getAllAvailableRoleTuples())
        {
            ret.Add(t);
        }
        return ret;
    }

    public Tuple<String,int,int,int> getPlayerStats() // order is: name, rank, dollars, credits
    {
        return new Tuple<String, int, int, int>(gameState.currentPlayer.playerName, gameState.currentPlayer.rank, gameState.currentPlayer.dollars, gameState.currentPlayer.credits);
    }

    public String getPlayerLocationName()
    {
        return gameState.currentPlayer.currentLocation.name;
    }

    public List<Tuple<String, String>> getAllPlayerLocationNames() // order is: player, location
    {
        List<Tuple<String, String>> playerlocations = new List<Tuple<String, String>>();
        Player curr = gameState.currentPlayer;
        for (int i = 0; i < gameState.numOfPlayers; i++)
        {
            playerlocations.Add(new Tuple<String, String>(curr.playerName, curr.currentLocation.name));
            curr = curr.nextPlayer;
        }
        return playerlocations;
    }

    public String getPlayerCurrentRole()
    {
        if(gameState.currentPlayer.currentRole != null)
        {
            return gameState.currentPlayer.currentRole.name;
        }
        else
        {
            return null;
        }
    }

    /*public void printWrapRewards(List<Tuple<String, int>> rewards)
    {
        view.displayWrapRewards(rewards);
    }*/
}