using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailerObserver : MonoBehaviour, Selectable
{

    public Text titleBox;


    public GameObject botRightObject;
    public GameObject topLeftObject;

    // Start is called before the first frame update
    private void Start()
    {
        titleBox.text = Controller.cont.getLocationByName("Swamp").name;
    }

    // Update is called once per frame
    void Update()
    {
        
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
