using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float thrust = 10.0f;
    public float turnMult = 2;

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.AddForce(transform.up * thrust);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * turnMult, 0);
    }
}
