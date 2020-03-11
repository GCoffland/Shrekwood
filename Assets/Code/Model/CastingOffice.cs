using System;
using System.Collections.Generic;


// Responsibilities: Give players a chance to upgrade their rank in exchange of credits or cash
public class CastingOffice : Location // SINGLETON
{
    private static CastingOffice castingOfficeInstance;

    private List<Upgrade> theUpgrades; // All of the possible upgrades at the casting office

    private CastingOffice(List<Upgrade> inupgrades, List<String> inadjecentlocations) : base ("Castle", inadjecentlocations)
    {
        this.theUpgrades = inupgrades;
    }
    public static CastingOffice GetInstance(List<Upgrade> inupgrades, List<String> inadjecentlocations)
    {
        if (castingOfficeInstance == null)
        {
            castingOfficeInstance = new CastingOffice(inupgrades, inadjecentlocations);
            return castingOfficeInstance;
        }
        else
        {
            return castingOfficeInstance;
        }
    }

    public static CastingOffice GetInstance()
    {
        return castingOfficeInstance;
    }
    public HashSet<Tuple<int,int,int>> GetUpgrades()
    {
        HashSet<Tuple<int, int, int>> allUpgrades = new HashSet<Tuple<int, int, int>>();
        // Call the toString function on all of the possible upgrades
        for (int j = 0; j < theUpgrades.Count; j++)  // Go through the possible upgrades and print them out to the view
        {
            allUpgrades.Add(theUpgrades[j].ToTuple());
        }
        return allUpgrades;
    }

}

