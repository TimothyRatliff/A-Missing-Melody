using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class MusicManager : MonoBehaviour 
{
    // public GameObject gameMenuManager;
    private static MusicManager _instance;
    public AudioSource ost;
    public int currentLevel = 1;

    public static MusicManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MusicManager>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake() 
    {
        if(_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
            ost.Play();
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if(this != _instance)
                Destroy(this.gameObject);
        }
        ost.Play();
        
        
    }

    // void Update()
    // {
    //     if(gameMenuManager.GetComponent<GameMenuManager>().paused)
    //         isPaused();
    // }

    public void isPaused()
    {
        Debug.Log("game is paused, applying music fx");
    }

    void Update()
    {
        
    }
}
