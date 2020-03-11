using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace DeadWood
{
    public class CastingOfficeObserver : MonoBehaviour
    {

        public Text textBox;

        // Update is called once per frame
        void Update()
        {
            HashSet<Tuple<int,int,int>> upgrades = Controller.cont.GetPlayerUpgrades();
            textBox.text = "Rank:" + "\t" + "Dollars:" + "\t" + "Credits:" + "\n";
            foreach(Tuple<int,int,int> t in upgrades)
            {
                textBox.text = textBox.text + t.Item1 + "\t\t\t" + t.Item2 + "\t\t\t" + (t.Item1==2?"\t":"") + t.Item3 + "\n";
            }
        }
    }
}