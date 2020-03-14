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
    private bool focusingOnLocations = false;
    private bool actuallyFocusingOnRoles = false;

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

    [SerializeField]
    private Text PlayerRole;

    // Locations AND ROLES?
    [SerializeField]
    private GameObject locationHolder;

    [SerializeField]
    private GameObject dummyTransform;   //DRAGON THIS ON IN TO SET transform

    [SerializeField]
    private GameObject prefabLocation;   //THIS ONE IS A PREFAB TO BE COPIED

    [SerializeField]
    private Text SecondMirrorText;

    private GameObject SelectedLocation;    //currently highlighted location

    private List<GameObject> listOfLocations = new List<GameObject>();

    private int maximumLocation = 0;   //This is for counting the amount of locations

    private int currentLocationOption = 0;  //This is for iterating through the location with arrow keys

    // Start is called before the first frame update
    void Awake()
    {
        cont.ProgramStartup();
        cont.GameStartUp();
        for (int i = 0; i < cont.amountOfPlayers; i++)
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

    void SelectLocation(int inloc)
    {
        // Reset the old option!
        SelectedLocation.GetComponent<Text>().color = Color.white;
        SelectedLocation.GetComponent<Outline>().effectColor = Color.black;
        // Set the new option
        SelectedLocation = listOfLocations[inloc];
        SelectedLocation.GetComponent<Text>().fontStyle = FontStyle.Bold;
        SelectedLocation.GetComponent<Text>().color = Color.black;
        SelectedLocation.GetComponent<Outline>().effectColor = Color.white;
        audioSource.PlayOneShot(tok, 1f);
    }

    void UpdateLocations()
    {
        SecondMirrorText.text = "LOCATIONS";//REEE
        maximumLocation = 0;
        listOfLocations.Clear();
        List<string> locations = Controller.cont.GetPlayerAdjacentLocations();
        float offset = 0;
        foreach (string loc in locations)
        {
            // Create the new sample locations
            maximumLocation++;
            GameObject Temp = GameObject.Instantiate<GameObject>(prefabLocation, locationHolder.transform);
            listOfLocations.Add(Temp);
            Temp.GetComponent<Text>().text = loc;
            Temp.GetComponent<Transform>().transform.position = dummyTransform.transform.position + new Vector3(0, offset, 0);
            offset -= 20f;
        }

    }

    void UpdateRoles()
    {
        //Get roles
        SecondMirrorText.text = "ROLES";
        maximumLocation = 0;
        listOfLocations.Clear();
        HashSet<Tuple<String, int>> roles = Controller.cont.GetPlayerRoles();
        float offset = 0;
        foreach (Tuple<String, int> rol in roles)
        {
            // Create the new sample roles
            maximumLocation++;
            GameObject Temp = GameObject.Instantiate<GameObject>(prefabLocation, locationHolder.transform);
            listOfLocations.Add(Temp);
            string rolelevel = (rol.Item2).ToString();
            Temp.GetComponent<Text>().text = rol.Item1 + " " + rolelevel;
            Temp.GetComponent<Transform>().transform.position = dummyTransform.transform.position + new Vector3(0, offset, 0);
            offset -= 20f;
        }
    }
    void DoCurrentOption()
    {
        List<string> incommand = new List<string>(); 
        switch (actionLocation)
        {
            case 0:
                Debug.Log("move");
                incommand.Add("move");// Figure this out!!
                UpdateLocations();
                SelectedLocation = listOfLocations[0];
                SelectLocation(0);
                focusingOnLocations = true; //Dont let us touch the main menu
                //Cant call this yet
                //Controller.cont.ProcessPlayerCommand(incommand);
                break;
            case 1:
                Debug.Log("take role");
                incommand.Add("take role"); // Figure this out!!
                UpdateRoles();
                SelectedLocation = listOfLocations[0];
                SelectLocation(0);
                focusingOnLocations = true; //Dont let us touch the main menu
                actuallyFocusingOnRoles = true;
                //Controller.cont.ProcessPlayerCommand(incommand);  //dont do this yet
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
        Move.fontStyle = FontStyle.Normal;
        Role.fontStyle = FontStyle.Normal;
        Act.fontStyle = FontStyle.Normal;
        Rehearse.fontStyle = FontStyle.Normal;
        Upgrade.fontStyle = FontStyle.Normal;
        End.fontStyle = FontStyle.Normal;
        HashSet<string> actions = cont.GetPlayerActions();
        for (int i = 0; i < possibleactionsArray.Length; i++)
        {
            possibleactionsArray[i] = false;
        }
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
        PlayerRole.text = cont.getPlayerCurrentRole();
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

        if (!focusingOnLocations)
        {
            if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0) //Arrow down
            {
                if (actionLocation < 5)
                {
                    if (possibleactionsArray[actionLocation + 1]) //Does the player have access to this action?
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
                        //Check if the 
                        if(actionLocation - 1 > 0)
                        {
                            while (!possibleactionsArray[actionLocation - 1])
                            {
                                actionLocation--;
                            }
                            SelectAction(--actionLocation);
                        }

                    }
                }
                else
                {
                    audioSource.PlayOneShot(tok, 0.2f);
                }
            }
            if (Input.GetButtonDown("Button_A"))
            {
                DoCurrentOption();
                audioSource.PlayOneShot(ahh, 1.0f);
            }
        }
        else //focusing on the location menu
        {
            if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0) //Arrow down
            {
                if (currentLocationOption < listOfLocations.Count-1)
                {
                    SelectLocation(++currentLocationOption);
                }
                else
                {
                    audioSource.PlayOneShot(tok, 0.2f);
                }
            }
            if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0) //Arrow up
            {
                if (currentLocationOption > 0)
                {
                    SelectLocation(--currentLocationOption);
                }
                else
                {
                    audioSource.PlayOneShot(tok, 0.2f);
                }
            }
            if (Input.GetButtonDown("Button_A"))
            {
                if(!actuallyFocusingOnRoles)
                {
                    List<string> incommand = new List<string>();
                    incommand.Add("move");
                    incommand.Add(listOfLocations[currentLocationOption].GetComponent<Text>().text);// Figure this out!!
                    Controller.cont.ProcessPlayerCommand(incommand);
                    currentLocationOption = 0;
                    audioSource.PlayOneShot(ahh, 1.0f);
                    focusingOnLocations = false;    //REPLACE WITH ENUM REEEEEEEEEEEEE
                    SelectedLocation.GetComponent<Text>().color = Color.white;
                    SelectedLocation.GetComponent<Outline>().effectColor = Color.black;
                    foreach (GameObject g in listOfLocations)
                    {
                        Destroy(g);
                    }
                }
                else //on roles
                {
                    List<string> incommand = new List<string>();
                    incommand.Add("take role");
                    string t = listOfLocations[currentLocationOption].GetComponent<Text>().text;
                    t = t.Substring(0, t.Length - 2);
                    incommand.Add(t);// Figure this out!!
                    Controller.cont.ProcessPlayerCommand(incommand);
                    currentLocationOption = 0;
                    audioSource.PlayOneShot(ahh, 1.0f);
                    focusingOnLocations = false;    //REPLACE WITH ENUM REEEEEEEEEEEEE
                    actuallyFocusingOnRoles = false;
                    SelectedLocation.GetComponent<Text>().color = Color.white;
                    SelectedLocation.GetComponent<Outline>().effectColor = Color.black;
                    foreach (GameObject g in listOfLocations)
                    {
                        Destroy(g);
                    }
                }
                
            }
        }
    }
}
