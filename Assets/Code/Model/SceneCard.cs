using System;
using System.Collections.Generic;

// Responsibilities: Abstract class that allows players to occupy their space
public class SceneCard
{
    private string name;
    public int budget;
    public List<Role> roles;

    public SceneCard(string inname, int inbudget, List<Role> inroles)
    {
        name = inname;
        budget = inbudget;
        roles = inroles;
    }

    public override string ToString()
    {
        string s = name + ", " + budget;
        for(int i = 0; i < roles.Count; i++)
        {
            s = s + " | " + roles[i].ToString();
        }

        return s;
    }

    public void SortRoles()
    {
        roles.Sort(CompareByRoleRank);
    }

    private int CompareByRoleRank(Role one, Role two)
    {
        return one.rank - two.rank;
    }
}