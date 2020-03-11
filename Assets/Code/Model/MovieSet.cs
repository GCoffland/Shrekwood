using System;
using System.Collections.Generic;

namespace DeadWood
{
    // Responsibilities: Holds roles and lets players act on them
    public class MovieSet : Location
    {
        private int internalShotsRemaining;
        public int shotsRemaining
        {
            get
            {
                return internalShotsRemaining;
            }
            set
            {
                internalShotsRemaining = value;
                if (internalShotsRemaining <= 0)
                {
                    SceneWrap();
                }
            }
        }
        public int maxShots;
        public List<Role> roles;
        public SceneCard card;

        public MovieSet(string name, List<Role> roles, List<String> inadjecentlocations, int takes) : base(name, inadjecentlocations)
        {
            this.roles = roles;
            internalShotsRemaining = takes;
            maxShots = takes;
        }

        struct rewards
        {
            public String playername;
            public int dollarreward;
        };

        public void SceneWrap()
        {
            List<rewards> rew = new List<rewards>();
            List<Player> oncards = new List<Player>();
            foreach (Player p in playersAtLocation)
            {
                if(p.currentRole != null)
                {
                    if (!p.currentRole.leadRole)
                    {
                        rewards temp = new rewards();
                        temp.playername = p.playerName;
                        temp.dollarreward = p.currentRole.rank;
                        rew.Add(temp);
                        p.dollars += p.currentRole.rank;
                    }
                }
            }
            card.SortRoles();
            Random ran = new Random();
            List<int> dicerolls = new List<int>();
            for(int i = 0; i < card.budget; i++)
            {
                dicerolls.Add(ran.Next(1, 7));
            }
            dicerolls.Sort();
            int j = dicerolls.Count-1;
            for(int i = 0; i < card.roles.Count; i++)
            {
                Player p = card.roles[card.roles.Count - i - 1].currentPlayer;
                if (p != null)
                {
                    rewards temp = new rewards();
                    temp.playername = p.playerName;
                    temp.dollarreward = dicerolls[j];
                    rew.Add(temp);
                    card.roles[card.roles.Count - i - 1].currentPlayer.dollars += dicerolls[j];
                }
                else
                {

                }
                j--;
                if (j < 0)
                {
                    break;
                }
                
            }
            RemoveSceneCard();
            List<Tuple<String, int>> ret = new List<Tuple<String, int>>();
            foreach(rewards r in rew)
            {
                ret.Add(new Tuple<String, int>(r.playername, r.dollarreward));
            }
            Controller.cont.printWrapRewards(ret);
        }

        public void resetShotCounters()
        {
            internalShotsRemaining = maxShots;
        }

        public override string ToString()
        {
            string s = name + " | ";
            for(int i = 0; i < roles.Count; i++)
            {
                s = s + roles[i].ToString() + ", ";
            }

            return s;
        }

        public List<Tuple<String, int>> getAllAvailableRoleTuples()
        {
            if(card == null)
            {
                return null;
            }
            List<Tuple<String, int>> ret = new List<Tuple<String, int>>();
            for (int i = 0; i < roles.Count; i++)
            {
                if(roles[i].currentPlayer == null)
                {
                    ret.Add(roles[i].ToTuple());
                }
            }
            for (int i = 0; i < card.roles.Count; i++)
            {
                if (card.roles[i].currentPlayer == null)
                {
                    ret.Add(card.roles[i].ToTuple());
                }
            }
            return ret;
        }

        public Role getRoleByName(String inname)
        {
            foreach(Role r in roles)
            {
                if(r.name == inname)
                {
                    return r;
                }
            }
            if (card == null)
            {
                return null;
            }
            foreach (Role r in card.roles)
            {
                if (r.name == inname)
                {
                    return r;
                }
            }
            return null;
        }

        public void RemoveSceneCard()
        {
            foreach(Player p in playersAtLocation)
            {
                if(p.currentRole != null)
                {
                    p.currentRole.currentPlayer = null;
                    p.currentRole = null;
                }
            }
            GameState.gameState.usedScenes.Add(card);
            card = null;
        }
    }
}
