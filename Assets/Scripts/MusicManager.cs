using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager2 : MonoBehaviour
{
    public AudioSource ost;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MusicManager2");
        if(objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        ost.Play();
        DontDestroyOnLoad(this.gameObject);

    }
}
