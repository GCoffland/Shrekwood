using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class winscreenbehavior : MonoBehaviour
{
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        List<Tuple<string, int>> scores = Controller.cont.gameState.CalculateScores();
        text.text = "";
        int maxscore = 0;
        string maxname = "";
        foreach(Tuple<string, int> t in scores)
        {
            text.text += t.Item1 + " has " + t.Item2 + " points\n";
            if(t.Item2 >= maxscore)
            {
                maxscore = t.Item2;
                maxname = t.Item1;
            }
        }
        text.text += maxname + " Wins";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
