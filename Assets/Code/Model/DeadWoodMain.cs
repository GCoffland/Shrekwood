using System;
using System.Collections.Generic;

namespace DeadWood
{
    public class DeadWoodMain
    {


        static void Main(string[] args)
        {
            Controller cont = new Controller();
            while (true)
            {
                switch (cont.state)
                {
                    case (Controller.PROGRAMSTATE.PROGRAMSTARTUP):
                        cont.ProgramStartup();
                        cont.state++;
                        break;
                    case (Controller.PROGRAMSTATE.GAMESTARTUP):
                        cont.GameStartUp();
                        cont.state++;
                        break;
                    case (Controller.PROGRAMSTATE.GAMEINPROGRESS):
                        cont.GameUpdate();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
