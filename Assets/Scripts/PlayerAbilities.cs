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
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("usingWhistle");
            Shoot();
            whistle.Play();
            //grunt.GetComponent<EnemyAI>().Stunned();
        }

    }

    public bool Shoot()
    {
        return fired = true;
    }
    
}
