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
            else if (transform.GetChild(0).GetComponent<BossAI>() != null)
            {
                 transform.GetChild(0).GetComponent<BossAI>().frozen = false;
            }
            else if (transform.GetChild(0).GetComponent<FinalBoss>() != null)
            {
                transform.GetChild(0).GetComponent<FinalBoss>().frozen = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            if( transform.GetChild(0).GetComponent<EnemyAI>() != null)
                transform.GetChild(0).GetComponent<EnemyAI>().frozen = true;
            else if (transform.GetChild(0).GetComponent<BossAI>() != null)
            {
                transform.GetChild(0).GetComponent<BossAI>().frozen = false;
            }
            else if (transform.GetChild(0).GetComponent<FinalBoss>() != null)
            {
                transform.GetChild(0).GetComponent<FinalBoss>().frozen = false;
            }
        }
    }
}
