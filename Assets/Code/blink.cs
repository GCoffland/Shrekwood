using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class blink : MonoBehaviour
{
    public Text MyText;
    public float quanta;
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(Helper());
    }

    IEnumerator Helper()
    {
        while (true)
        {
            MyText.color = new Color(MyText.color.r, MyText.color.g, MyText.color.b, 0);
            yield return new WaitForSeconds(quanta);
            MyText.color = Color.white;
            yield return new WaitForSeconds(quanta);
        }
    }
}
