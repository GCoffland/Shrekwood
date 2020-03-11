using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBehavior : MonoBehaviour
{
    Controller cont = Controller.cont;

    // Start is called before the first frame update
    void Awake()
    {
        cont.ProgramStartup();
        cont.GameStartUp();
    }

    // Update is called once per frame
    void Update()
    {
        cont.GameUpdate();
    }
}
