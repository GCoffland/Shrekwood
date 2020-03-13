using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObserver : MonoBehaviour
{
    public int playerNum;
    private Player player;
    public string currentLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GameObject.Find(player.currentLocation.name + "Point").transform.position + new Vector3(playerNum * 20 - ((int)(playerNum / 4) * 80), -((int)(playerNum / 4) * 20), 0);
        currentLocation = Controller.cont.getPlayerByNumber(playerNum).currentLocation.name;
    }

    public void loadPlayer()
    {
        player = Controller.cont.getPlayerByNumber(playerNum);
    }
}
