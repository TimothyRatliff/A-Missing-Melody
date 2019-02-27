﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public Transform firePoint;
    public GameObject whistlePrefab;
    private Animator anim;
    bool fired = false;
    public AudioSource whistle;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("isWhistle");
            Shoot();
            whistle.Play();
        }

    }

    void Shoot()
    {
        //whistle
        Instantiate(whistlePrefab, firePoint.position, firePoint.rotation);
    }
}
