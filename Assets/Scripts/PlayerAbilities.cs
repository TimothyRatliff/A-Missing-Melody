using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public GameObject whistlePrefab;
    private Animator animWhistle;
    private Animator anim;
    public bool whistleFired = false;
    public AudioSource whistle;
    public GameObject trumpetProjectile;
    private Quaternion rot;
    private Vector3 pos;
    public bool resetSave;
    private float cooldown;

    void Awake()
    {
        if (resetSave)
        {
            PlayerPrefs.SetInt("trumpet", 0);
            resetSave = false;
        }
        animWhistle = whistlePrefab.GetComponent<Animator>();
        anim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Whistle or trumpet
        if (Input.GetButtonDown("Fire1"))
        {
            if (PlayerPrefs.GetInt("trumpet", 0) == 1)
            {
                if (cooldown < 0)
                {
                    if (PlayerPrefs.GetInt("trumpet", 0) == 1)
                    {
                        StartCoroutine(shoot());
                        cooldown = 0.6f;
                    }
                    else
                    {
                        cooldown = 1f;
                        anim.SetTrigger("usingWhistle");
                        whistleFired = true;
                        whistle.Play();
                    }
                }
            }
            else
            {
                animWhistle.SetTrigger("usingWhistle");
                whistleFired = true;
                whistle.Play();
            }
        }
        else
        {
           whistleFired = false;
           cooldown -= Time.deltaTime;
        }
    }

    private IEnumerator shoot()
    {
        if (transform.lossyScale.x == 1)
        {
            rot = Quaternion.Euler(0, 180, 0);
            pos = transform.position + -(Vector3.right * 2.4f) + (Vector3.up * 1.35f);
        }
        else
        {
            rot = Quaternion.Euler(0, 0, 0);
            pos = transform.position + (Vector3.right * 2.4f) + (Vector3.up * 1.35f);
        }
        //Add Trumpet triggers animations
        anim.SetTrigger("isTrumpeting");

        yield return new WaitForSeconds(0.2f);
        GameObject.Instantiate(trumpetProjectile, pos, rot);
    }
}
