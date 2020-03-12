using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainHand : MonoBehaviour
{
    [SerializeField]
    private AudioClip ahh;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Text PrePlayer;

    [SerializeField]
    private Text PostPlayer;

    [SerializeField]
    private Text counter;

    [SerializeField]
    private Text secondStart;

    private bool bWasSet = false;
    private bool bSelectingPlayers = false;
    private int counterVal = 2; // Always starts at 2 and never below 2 and never above 8
    private bool bDidWantToMoveOn = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PostPlayer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            if (!bWasSet)
            {
                StartCoroutine(AudioCoroutine());
            }
        }
        if (bWasSet) //This means we are selecting players!
        {
            if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0) //Arrow up
            {
                if (counterVal < 8)
                {
                    counterVal++;
                    counter.text = counterVal.ToString();
                    audioSource.PlayOneShot(ahh, 0.7F);
                }
                else
                {
                    audioSource.PlayOneShot(ahh,0.2f);
                }
            }
            if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0) //Arrow down
            {
                if (counterVal > 2)
                {
                    counterVal--;
                    counter.text = counterVal.ToString();
                    audioSource.PlayOneShot(ahh, 0.7F);
                }
                else
                {
                    audioSource.PlayOneShot(ahh, 0.2f);
                }
            }
            if(Input.GetButtonDown("Start"))
            {
                if(bDidWantToMoveOn)
                {
                    Controller.amountOfPlayers = counterVal;
                    SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
                }
                bDidWantToMoveOn = true;
            }
        }
    }

    IEnumerator AudioCoroutine()
    {
        bWasSet = true;
        audioSource.PlayOneShot(ahh, 0.7F);
        //yield on a new YieldInstruction that waits for 1seconds.
        yield return new WaitForSeconds(0.5f);
        //SceneManager.LoadScene("GameScene", LoadSceneMode.Single);    //Don't load the next scene just have them selected player #
        bSelectingPlayers = true;
        PrePlayer.gameObject.SetActive(false);
        PostPlayer.gameObject.SetActive(true);
        counter.gameObject.SetActive(true);
        counter.GetComponent<blink>().KickStart();
        secondStart.GetComponent<blink>().KickStart();
    }
}
