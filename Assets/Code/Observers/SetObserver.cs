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
    private BoxCollider2D col;
    private Vector2 topleft;
    private Vector2 bottomright;


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
        col = GetComponent<BoxCollider2D>();
        topleft = new Vector2(col.bounds.min.x, col.bounds.max.y);
        bottomright = new Vector2(col.bounds.max.x, col.bounds.min.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void select(bool b)
    {
        //Instantiate<GameObject>();
    }

    public void highlight(bool b)
    {

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
