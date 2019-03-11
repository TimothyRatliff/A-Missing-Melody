using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public LayerMask enemyMask;
    public float speed;
    private Transform target;
    Rigidbody2D myBody;
    Transform myTrans;

    void Start()
    {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (myTrans.position.x < target.position.x)
        {
            Vector3 currentRotation = myTrans.eulerAngles;
            currentRotation.y = 0;
            myTrans.eulerAngles = currentRotation;
            myTrans.position = Vector2.MoveTowards(myTrans.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            Vector3 currentRotation = myTrans.eulerAngles;
            currentRotation.y = 180;
            myTrans.eulerAngles = currentRotation;
            myTrans.position = Vector2.MoveTowards(myTrans.position, target.position, speed * Time.deltaTime);
        }
    }

}