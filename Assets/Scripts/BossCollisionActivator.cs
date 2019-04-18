using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollisionActivator : MonoBehaviour
{
    public GameObject boss;
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            boss.GetComponent<BossAI>().enableCollision();
        }
    }
}
