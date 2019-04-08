﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class MusicManager : MonoBehaviour 
{
    private static MusicManager _instance;
    public AudioSource ost;
    public string[] level1;
    public string[] level2;
    public string[] MainMenu;
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
    }

    void Update()
    {
        level1 = new string[7]{"Level 1-1", "Level 1-2", "Level 1-3", "Level 1-4", "Level 1-5", "Level 1-6", "Level 1-7"};
        Scene currentScene = SceneManager.GetActiveScene();
        if(level1.Contains(currentScene.name))
        {
            currentLevel = 1;
        }
    }
}
