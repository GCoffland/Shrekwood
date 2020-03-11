using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailerObserver : MonoBehaviour
{

    public Text titleBox;

    // Start is called before the first frame update
    private void Start()
    {
        titleBox.text = Controller.cont.getLocationByName("Swamp").name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
