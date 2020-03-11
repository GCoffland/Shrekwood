using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DeadWood
{
    public class CastingOfficeObserver : Observer
    {

        // Update is called once per frame
        void Update()
        {
            HashSet<Tuple<int,int,int>> upgrades = Controller.cont.GetPlayerUpgrades();
            textBox.text = "";
            foreach(Tuple<int,int,int> t in upgrades)
            {
                textBox.text = textBox.text + t.Item1 + "\t" + t.Item2 + "\t" + t.Item3 + "\n";
            }
        }
    }
}