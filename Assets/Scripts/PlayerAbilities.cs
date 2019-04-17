using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public Transform firePoint;
    public GameObject whistlePrefab;
    private Animator anim;
    public bool fired = false;
    public AudioSource whistle;

    void Awake()
    {
        anim = whistlePrefab.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Whistle
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("usingWhistle");
            fired = true;
            whistle.Play();
        }
        else
        {
           fired = false; 
        }
    }

    private void OnTrigger2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DrumSticks")
        {
            //DestroyObject(EnemyAI itemToSpawn, 1);
        }
    }

}
