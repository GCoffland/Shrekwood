using System.Collections;
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
    private AudioSource audioSource;

    [SerializeField]
    private Text Move;

    [SerializeField]
    private Text Act;

    [SerializeField]
    private Text Rehearse;

    [SerializeField]
    private Text End;

    private Text SelectedOption;

    private Text[] optionsArray = new Text[4];
    private int optionLocation = 0; //Start on move!

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
        optionsArray[0] = Move;
        optionsArray[1] = Act;
        optionsArray[2] = Rehearse;
        optionsArray[3] = End;

        SelectedOption = optionsArray[0];

    }

    void Start()
    {
        SelectOption(0);
    }

    void SelectOption(int location)
    {
        // Reset the old option!
        SelectedOption.color = Color.white;
        SelectedOption.GetComponent<Outline>().effectColor = Color.black;
        // Set the new option
        SelectedOption = optionsArray[location];
        SelectedOption.fontStyle = FontStyle.Bold;
        SelectedOption.color = Color.black;
        SelectedOption.GetComponent<Outline>().effectColor = Color.white;
        audioSource.PlayOneShot(ahh, 1f);
    }

    void DoCurrentOption()
    {
        switch(optionLocation)
        {
            case 0:
                Debug.Log("MOVE");
                break;
            case 1:
                Debug.Log("ACT");
                break;
            case 2:
                Debug.Log("REEEEE");
                break;
            case 3:
                Debug.Log("END");
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        cont.GameUpdate();
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
            if (optionLocation < 3)
            {
                optionLocation++;
                SelectOption(optionLocation);
            }
            else
            {
                audioSource.PlayOneShot(ahh, 0.2f);
            }
        }
        if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0) //Arrow up
        {
            if (optionLocation > 0)
            {
                optionLocation--;
                SelectOption(optionLocation);
            }
            else
            {
                audioSource.PlayOneShot(ahh, 0.2f);
            }
        }
        if (Input.GetButtonDown("Start"))
        {
            DoCurrentOption();
        }
    }
}
