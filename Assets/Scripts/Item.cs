using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float thrust = 10.0f;
    public float turnMult = 2;
    private GameObject wallBlock;
    public bool isTrumpet;

    void Start()
    {
        wallBlock = GameObject.Find("Wall Block"); 
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.AddForce(transform.up * thrust);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * turnMult, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (wallBlock != null)
                wallBlock.SetActive(false);
            if (isTrumpet)
                PlayerPrefs.SetInt("trumpet", 1);
            transform.gameObject.SetActive(false);
        }
    }
}
