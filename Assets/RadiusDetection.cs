using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusDetection : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            if( transform.GetChild(0).GetComponent<EnemyAI>() != null)
                transform.GetChild(0).GetComponent<EnemyAI>().frozen = false;
            else
            {
                 transform.GetChild(0).GetComponent<BossAI>().frozen = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            if( transform.GetChild(0).GetComponent<EnemyAI>() != null)
                transform.GetChild(0).GetComponent<EnemyAI>().frozen = true;
            else
            {
                 transform.GetChild(0).GetComponent<BossAI>().frozen = true;
            }
        }
    }
}
