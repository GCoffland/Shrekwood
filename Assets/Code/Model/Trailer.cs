using System;
using System.Collections.Generic;

namespace DeadWood
{
    // Responsibilities: Exist as starting position for players
    public class Trailer : Location // SINGLETON
    {
        private static Trailer trailerInstance;
        private Trailer(List<String> inadjecentlocations) : base("Swamp", inadjecentlocations)
        {

        }
        public static Trailer GetInstance(List<String> inadjecentlocations)
        {
            if (trailerInstance == null)
            {
                trailerInstance = new Trailer(inadjecentlocations);
                return trailerInstance;
            }
            else
            {
                return trailerInstance;
            }
        }
        //This class serves as the starting point for the players at the start of each round
        // It currently has no specific implementation
    }
}