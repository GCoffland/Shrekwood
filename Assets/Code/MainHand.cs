using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainHand : MonoBehaviour
{
    [SerializeField]
    private AudioClip ahh;

    [SerializeField]
    private AudioSource audioSource;

    private bool bWasSet = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            if(!bWasSet)
            {
                StartCoroutine(AudioCoroutine());
            }
        }
    }

    IEnumerator AudioCoroutine()
    {
        bWasSet = true;
        audioSource.PlayOneShot(ahh, 0.7F);
        //yield on a new YieldInstruction that waits for 1seconds.
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
