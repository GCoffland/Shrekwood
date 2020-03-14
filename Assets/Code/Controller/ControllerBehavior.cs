using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ControllerBehavior : MonoBehaviour
{
    Controller cont = Controller.cont;
    public string currentPlayer;
    public string currentPlayerLocation;

    [SerializeField]
    private AudioClip ahh;

    [SerializeField]
    private AudioClip tok;

    [SerializeField]
    private AudioSource audioSource;

    // ***********Actions
    [SerializeField]
    private Text Move;

    [SerializeField]
    private Text Role;

    [SerializeField]
    private Text Act;

    [SerializeField]
    private Text Rehearse;

    [SerializeField]
    private Text Upgrade;

    [SerializeField]
    private Text End;

    private Text SelectedOption;

    private Text[] actionsArray = new Text[6];
    private bool[] possibleactionsArray = new bool[6];

    private int actionLocation = 0; //Start on move!

    // ***********Stats
    [SerializeField]
    private Text PlayerText;

    [SerializeField]
    private Text RankText;

    [SerializeField]
    private Text DollarsText;

    [SerializeField]
    private Text CreditsText;

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
        actionsArray[0] = Move;
        actionsArray[1] = Role;
        actionsArray[2] = Act;
        actionsArray[3] = Rehearse;
        actionsArray[4] = Upgrade;
        actionsArray[5] = End;

        SelectedOption = actionsArray[0];
        for (int i = 0; i < possibleactionsArray.Length; i++)
        {
            possibleactionsArray[i] = false;    //All actions to false so we can select the possible later
        }
    }

    void Start()
    {
        SelectAction(0);
    }

    void SelectAction(int location)
    {
        // Reset the old option!
        SelectedOption.color = Color.white;
        SelectedOption.GetComponent<Outline>().effectColor = Color.black;
        // Set the new option
        SelectedOption = actionsArray[location];
        SelectedOption.fontStyle = FontStyle.Bold;
        SelectedOption.color = Color.black;
        SelectedOption.GetComponent<Outline>().effectColor = Color.white;
        audioSource.PlayOneShot(tok, 1f);
    }

    void DoCurrentOption()
    {
        List<string> incommand = new List<string>(); 
        switch (actionLocation)
        {
            case 0:
                Debug.Log("move");
                incommand.Add("move");// Figure this out!!
                Controller.cont.ProcessPlayerCommand(incommand);
                break;
            case 1:
                Debug.Log("take role");
                Debug.Log("reeeeeeeeeeeeeeeeeee");
                incommand.Add("take role"); // Figure this out!!
                Controller.cont.ProcessPlayerCommand(incommand);
                break;
            case 2:
                Debug.Log("act");
                incommand.Add("act"); 
                Controller.cont.ProcessPlayerCommand(incommand);
                break;
            case 3:
                Debug.Log("rehearse");
                incommand.Add("rehearse");
                Controller.cont.ProcessPlayerCommand(incommand);
                break;
            case 4:
                Debug.Log("upgrade");
                incommand.Add("upgrade");
                Controller.cont.ProcessPlayerCommand(incommand);
                break;
            case 5:
                Debug.Log("end turn");
                incommand.Add("end turn");
                Controller.cont.ProcessPlayerCommand(incommand);
                break;
            default:
                Debug.Log("GABE HELP ME IM STUCK IN THE COMPUTER REEEEE");
                break;
        }
    }
    
    // Updates the text to reflect the current players possible actions
    void UpdateActions()
    {
        //Disable all actions then enable the correct ones 
        HashSet<string> actions = cont.GetPlayerActions();
        foreach (String action in actions)
        {
            switch(action)
            {
                case "move":
                    possibleactionsArray[0] = true;
                    Move.fontStyle = FontStyle.Bold;
                    break;
                case "take role":
                    possibleactionsArray[1] = true;
                    Role.fontStyle = FontStyle.Bold;
                    break;
                case "act":
                    possibleactionsArray[2] = true;
                    Act.fontStyle = FontStyle.Bold;
                    break;
                case "rehearse":
                    possibleactionsArray[3] = true;
                    Rehearse.fontStyle = FontStyle.Bold;
                    break;
                case "upgrade":
                    possibleactionsArray[4] = true;
                    Upgrade.fontStyle = FontStyle.Bold;
                    break;
                case "end turn":
                    possibleactionsArray[5] = true;
                    End.fontStyle = FontStyle.Bold;
                    break;  
            }
        }
    }

    // Updates the text to represent the current players location and stats!
    void UpdateStats()
    {
        Tuple<String, int, int, int> stats = cont.getPlayerStats();
        PlayerText.text = stats.Item1 + " is at the " + cont.getPlayerLocationName();
        RankText.text = "Rank: " + (stats.Item2).ToString();
        DollarsText.text = "Dollars: " + (stats.Item3).ToString();
        CreditsText.text = "Credits: " + (stats.Item4).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        cont.GameUpdate();
        UpdateStats();
        UpdateActions();
        /*
         * //This was for a test, instead this will control the menu options on the right
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            cont.Move(cont.gameState.currentPlayer, "Castle");
        }*/
        currentPlayer = cont.gameState.currentPlayer.playerName;
        currentPlayerLocation = cont.gameState.currentPlayer.currentLocation.name;

        if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0) //Arrow down
        {
            if (actionLocation < 5)
            {
                if(possibleactionsArray[actionLocation + 1]) //Does the player have access to this action?
                {
                    SelectAction(++actionLocation);
                }
                else // No access? Then increment until we find one!
                {
                    while (!possibleactionsArray[actionLocation + 1])
                    {
                        actionLocation++;
                    }
                    SelectAction(++actionLocation);
                }
            }
            else
            {
                audioSource.PlayOneShot(tok, 0.2f);
            }
        }
        if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0) //Arrow up
        {
            if (actionLocation > 0)
            {
                if (possibleactionsArray[actionLocation - 1]) //Does the player have access to this action?
                {
                    SelectAction(--actionLocation);
                }
                else // No access? Then increment until we find one!
                {
                    while (!possibleactionsArray[actionLocation - 1])
                    {
                        actionLocation--;
                    }
                    SelectAction(--actionLocation);
                }
            }
            else
            {
                audioSource.PlayOneShot(tok, 0.2f);
            }
        }
        if (Input.GetButtonDown("Start"))
        {
            DoCurrentOption();
            audioSource.PlayOneShot(ahh, 1.0f);
        }
    }
}
