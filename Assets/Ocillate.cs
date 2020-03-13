using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocillate : MonoBehaviour
{
    public bool invert;
    public bool play;

    private Vector2 origPos;

    private readonly Vector2 downTarget = new Vector2(1, -1);
    private readonly Vector2 upTarget = new Vector2(-1, 1);
    private float iterator;

    // Start is called before the first frame update
    void Start()
    {
        origPos = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (invert)
        {
            Vector2.Lerp(origPos, downTarget, 0.1f);
        }
        else
        {

        }
        
    }
}
