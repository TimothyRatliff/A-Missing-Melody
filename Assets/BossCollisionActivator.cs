﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollisionActivator : MonoBehaviour
{
    public GameObject boss;
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            Debug.Log("works");
            boss.GetComponent<BossAI>().enableCollision();
        }
    }
}
