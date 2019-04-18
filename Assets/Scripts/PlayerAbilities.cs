using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public GameObject whistlePrefab;
    private Animator anim;
    public bool whistleFired = false;
    public AudioSource whistle;
    public GameObject trumpetProjectile;
    private Quaternion rot;
    private Vector3 pos;

    void Awake()
    {
        anim = whistlePrefab.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Whistle or trumpet
        if (Input.GetButtonDown("Fire1"))
        {
            if (PlayerPrefs.GetInt("trumpet", 0) == 1)
            {
                if (transform.lossyScale.x == 1)
                {
                    rot = Quaternion.Euler(0, 180, 0);
                    pos = transform.position + -(Vector3.right * 2) + (Vector3.up * 1.5f);
                }
                else
                {
                    rot = Quaternion.Euler(0, 0, 0);
                    pos = transform.position + (Vector3.right * 2) + (Vector3.up * 1.5f);
                }
                //Add Trumpet triggers animations
                GameObject.Instantiate(trumpetProjectile, pos, rot);
            }
            else
            {
                anim.SetTrigger("usingWhistle");
                whistleFired = true;
                whistle.Play();
            }
        }
        else
        {
           whistleFired = false; 
        }
    }
}
