using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource ost;
    public bool paused;
    private AudioLowPassFilter FX;

    void Awake()
    {
        GameObject menu = GameObject.Find("Game Menu Manager");
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MusicManager");
        if(objs.Length > 1)
        {
            Debug.Log("Found multiple MusicManagers, destroying this one");
            Destroy(objs[0]);
        }
        ost.Play();
        DontDestroyOnLoad(this.gameObject);
    }
    
    // void Update()
    // {
    //     bool paused = menu.GetComponent<GameMenuManager>().paused;
    //     if (paused)
    //     {
    //         isPaused();
    //     }
    // }

    // void isPaused()
    // {
    //     FX = ost.GetComponent<AudioLowPassFilter>();
    //     FX.enabled = true;
    // }
}
