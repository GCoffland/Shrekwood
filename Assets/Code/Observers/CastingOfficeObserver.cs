using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace DeadWood
{
    public class CastingOfficeObserver : MonoBehaviour, Selectable
    {

        public Text textBox;
        public Text titleBox;


        public GameObject botRightObject;
        public GameObject topLeftObject;

        private void Start()
        {
            titleBox.text = Controller.cont.getLocationByName("Castle").name;
        }

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

        public void select(STATE s)
        {
            if (s == STATE.PLAY)
            {
                botRightObject.transform.position = new Vector3(botRightObject.transform.position.x, botRightObject.transform.position.y, 0);
                topLeftObject.transform.position = new Vector3(topLeftObject.transform.position.x, topLeftObject.transform.position.y, 0);
                botRightObject.GetComponent<Ocillate>().play = true;
                topLeftObject.GetComponent<Ocillate>().play = true;
            }
            else if (s == STATE.PAUSE)
            {
                botRightObject.transform.position = new Vector3(botRightObject.transform.position.x, botRightObject.transform.position.y, 0);
                topLeftObject.transform.position = new Vector3(topLeftObject.transform.position.x, topLeftObject.transform.position.y, 0);
                botRightObject.GetComponent<Ocillate>().play = false;
                topLeftObject.GetComponent<Ocillate>().play = false;
            }
            else if (s == STATE.INVISIBLE)
            {
                botRightObject.transform.position = new Vector3(botRightObject.transform.position.x, botRightObject.transform.position.y, -100);
                topLeftObject.transform.position = new Vector3(topLeftObject.transform.position.x, topLeftObject.transform.position.y, -100);
                botRightObject.GetComponent<Ocillate>().play = false;
                topLeftObject.GetComponent<Ocillate>().play = false;
            }
        }
    }
}