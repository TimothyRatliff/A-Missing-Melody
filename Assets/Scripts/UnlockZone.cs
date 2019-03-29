using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockZone : MonoBehaviour
{
    public bool unlockZone2;
    public bool unlockBoss;

    private void Start()
    {
        if (PlayerPrefs.GetInt("zone2", 0) == 1)
        {
            transform.GetChild(0).gameObject.active = false;
        }
        if (PlayerPrefs.GetInt("boss", 0) == 1)
        {
            transform.GetChild(1).gameObject.active = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (unlockZone2)
            {
                PlayerPrefs.SetInt("zone2", 1);
            }
            if (unlockBoss)
            {
                PlayerPrefs.SetInt("boss", 1);
            }
        }
    }
}
