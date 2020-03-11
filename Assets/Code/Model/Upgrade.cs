using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Upgrade
{
    private int rankNumberToBuy;
    private int dollarsToBuy;
    private int creditsToBuy;

    public Upgrade(int inRank, int inDollars, int inCredits)
    {
        rankNumberToBuy = inRank;
        dollarsToBuy = inDollars;
        creditsToBuy = inCredits;
    }
    public override String ToString()   // Override the ToString implementation to give a better presentation of the upgrade
    {
        string upgradeValues = "Rank value: " + rankNumberToBuy + ", Dollars to buy: " + dollarsToBuy +
            ", Credits to buy: " + creditsToBuy;
        return upgradeValues;
    }
    public Tuple<int, int, int> ToTuple()   // Override the ToString implementation to give a better presentation of the upgrade
    {
        Tuple<int, int, int> valuesToReturn = new Tuple<int, int, int>(rankNumberToBuy,dollarsToBuy,creditsToBuy);
        return valuesToReturn;
    }
}
