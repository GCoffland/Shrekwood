using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetObserver : Observer, Selectable
{
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private List<Text> movieSetTitles;
    [SerializeField]
    private List<Text> movieSetTexts;
    [SerializeField]
    private List<Text> movieSetRanks;
    [SerializeField]
    private List<Text> sceneCardTitles;
    [SerializeField]
    private List<Text> sceneCardTexts;
    [SerializeField]
    private List<Text> sceneCardRanks;
    [SerializeField]
    private Text shotCounters;
    private MovieSet movieSet;

    public GameObject botRightObject;
    public GameObject topLeftObject;

    // Start is called before the first frame update
    void Start()
    {
        movieSet = (MovieSet)Controller.cont.getLocationByName(gameObject.name);
        titleText.text = gameObject.name;
        clearAllRoleTexts();
        for(int i = 0; i < movieSet.roles.Count; i++)
        {
            movieSetTitles[i].text = movieSet.roles[i].name;
            movieSetTexts[i].text = "\"" + movieSet.roles[i].text + "\"";
            movieSetRanks[i].text = ""+movieSet.roles[i].rank;
        }
        assignSceneCardRoles();
        shotCounters.text = "" + movieSet.shotsRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        shotCounters.text = "" + movieSet.shotsRemaining;
        assignSceneCardRoles();
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
        else if(s == STATE.PAUSE)
        {
            botRightObject.transform.position = new Vector3(botRightObject.transform.position.x, botRightObject.transform.position.y, 0);
            topLeftObject.transform.position = new Vector3(topLeftObject.transform.position.x, topLeftObject.transform.position.y, 0);
            botRightObject.GetComponent<Ocillate>().play = false;
            topLeftObject.GetComponent<Ocillate>().play = false;
        }else if(s == STATE.INVISIBLE)
        {
            botRightObject.transform.position = new Vector3(botRightObject.transform.position.x, botRightObject.transform.position.y, -10000);
            topLeftObject.transform.position = new Vector3(topLeftObject.transform.position.x, topLeftObject.transform.position.y, -10000);
            botRightObject.GetComponent<Ocillate>().play = false;
            topLeftObject.GetComponent<Ocillate>().play = false;
        }
    }

    void assignSceneCardRoles()
    {
        for (int i = 0; i < movieSet.card.roles.Count; i++)
        {
            sceneCardTitles[i].text = movieSet.card.roles[i].name;
            sceneCardTexts[i].text = "\"" + movieSet.card.roles[i].text + "\"";
            sceneCardRanks[i].text = "" + movieSet.card.roles[i].rank;
        }
    }

    void clearAllRoleTexts()
    {
        for (int i = 0; i < movieSetTitles.Count; i++)
        {
            movieSetTitles[i].text = "";
            movieSetTexts[i].text = "";
            movieSetRanks[i].text = "";
        }
        for (int i = 0; i < sceneCardTitles.Count; i++)
        {
            sceneCardTitles[i].text = "";
            sceneCardTexts[i].text = "";
            sceneCardRanks[i].text = "";
        }
    }
}
