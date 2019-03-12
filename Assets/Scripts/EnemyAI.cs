using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    private Transform target;
    public bool shot;
    public float stunTime = 3;
    public int health = 10;
    Rigidbody2D myBody;
    Transform myTrans;
    public GameObject itemToSpawn;

    void Start()
    {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        {
            shot = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>().fired;

            //stun//

            if (shot.Equals(true))
            {
                stunTime -= Time.deltaTime;
                speed = 0;
                if (stunTime < 0)
                {
                    shot = false;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>().fired = false;
                    speed = 3;
                    stunTime = 3;
                }

            }
        }

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

        if (health <= 0)
        {
            transform.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        SpawnItem(itemToSpawn);
    }

    private void SpawnItem(GameObject item)
    {
        if (item != null)
        {
            Instantiate(item, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
        }
    }
}
