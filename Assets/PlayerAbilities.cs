using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public Transform firePoint;
    public GameObject whistlePrefab;
    bool fired = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }

    void Shoot()
    {
        //whistle
        Instantiate(whistlePrefab, firePoint.position, firePoint.rotation);
    }
}
