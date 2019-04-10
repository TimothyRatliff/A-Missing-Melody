using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource ost;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MusicManager");
        if(objs.Length > 1)
        {
            Debug.Log("Found multiple MusicManagers, destroying this one");
            Destroy(objs[0]);
        }
        ost.Play();
        DontDestroyOnLoad(this.gameObject);
    }
}
