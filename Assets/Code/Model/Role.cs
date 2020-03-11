using System;
using System.Collections.Generic;

// Responsibilities: Give players a certain task to perform in a scene
public class Role
{
    public string name;
    public bool leadRole;
    public int rank;
    public string text;
    public Player currentPlayer;

    public Role(string name, int rank, string text, bool lead)
    {
        this.name = name;
        this.rank = rank;
        this.text = text;
        leadRole = lead;
    }

    public override string ToString()
    {
        return name + " - " + rank + " - " + text;
    }

    public Tuple<String, int> ToTuple()
    {
        return new Tuple<String, int>(name,rank);
    }
        
}