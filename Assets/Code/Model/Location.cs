using System;
using System.Collections.Generic;

// Responsibilities: Abstract class that allows players to occupy their space
public class Location
{   // TODO: ADD A LOCATION TYPE, MAKE A VECTOR TYPE?
    public string name;
    public List<string> adjacentLocations
    {
        get;
        private set;
    }
    public List<Player> playersAtLocation
    {
        get;
        private set;
    }

    public Location(string inname, List<string> inajecentlocations)
    {
        name = inname;
        adjacentLocations = inajecentlocations;
        playersAtLocation = new List<Player>();
    }
}