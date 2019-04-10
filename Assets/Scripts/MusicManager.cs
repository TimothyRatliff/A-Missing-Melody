using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource ost;
    private AudioLowPassFilter FX;

    void Awake()
    {
        FX = ost.GetComponent<AudioLowPassFilter>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MusicManager");
        if(objs.Length > 1)
        {
            Debug.Log("Found multiple MusicManagers, destroying this one");
            Destroy(objs[0]);
        }
        ost.Play();
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Update()
    {
        if (Time.timeScale == 0f)
        {
            FX.enabled = true;
        }
        else
            FX.enabled = false;
    }

}
