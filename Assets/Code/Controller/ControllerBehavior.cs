using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBehavior : MonoBehaviour
{
    Controller cont = Controller.cont;
    public string currentPlayer;
    public string currentPlayerLocation;


    // Start is called before the first frame update
    void Awake()
    {
        cont.ProgramStartup();
        cont.GameStartUp();
        for(int i = 0; i < cont.amountOfPlayers; i++)
        {
            GameObject temp = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Player" + i));
            temp.GetComponent<PlayerObserver>().loadPlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        cont.GameUpdate();
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            cont.Move(cont.gameState.currentPlayer, "Castle");
            
        }
        currentPlayer = cont.gameState.currentPlayer.playerName;
        currentPlayerLocation = cont.gameState.currentPlayer.currentLocation.name;
    }
}
