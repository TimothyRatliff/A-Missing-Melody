using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    private float defaultSpeed;
    public float headHight = 1f;
    private Transform target;
    public bool stunned, attacking;
    public float stunTime = 10;
    private float defaultStunTime;
    public float attackCooldown = 3;
    private float defaultAttackCooldown;
    private Animator anim;

    public int health = 1;
    Rigidbody2D myBody;
    Transform myTrans;
    public GameObject itemToSpawn;

    private bool playerInAttack = false;
    private bool dead;
    public bool frozen;

    private SpriteRenderer mySprite;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        defaultSpeed = speed;
        defaultStunTime = stunTime;
        defaultAttackCooldown = attackCooldown;
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        mySprite = this.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>().fired)
            {
                stunned = true;
            }

            //stun//

            if (stunned)
            {
                anim.SetTrigger("isStunned");
                stunTime -= Time.deltaTime;
                speed = 0;
                if (stunTime < 0)
                {
                    anim.ResetTrigger("isStunned");
                    stunned = false;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>().fired = false;
                    speed = defaultSpeed;
                    stunTime = defaultStunTime;
                }

            } else if (!stunned && !frozen) {
                if (myTrans.position.x < target.position.x)
                {
                    anim.SetTrigger("isWalking");
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
                if (attacking && !stunned)
                {
                    anim.SetTrigger("isAttacking");
                    attackCooldown -= Time.deltaTime;
                    speed = 0;
                    if (attackCooldown < 0)
                    {
                        attacking = false;
                        speed = defaultSpeed;
                        attackCooldown = defaultAttackCooldown;
                    }
                }
            }

            if (health <= 0)
            {
                transform.gameObject.SetActive(false);
                dead = true;
            }
        }
    }

    private void OnDisable()
    {
        SpawnItem(itemToSpawn);
    }

    // Spawns GameObject upwards
    private void SpawnItem(GameObject item)
    {
        if (item != null)
        {
            Instantiate(item, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
        }
    }


        // get the direction of the collision
        //jumping on enemy
        void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            Vector3 direction = transform.position - obj.gameObject.transform.position;
            if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y) && stunned) {
                if (Mathf.Abs(direction.y) > headHight && Mathf.Abs(direction.x) < headHight / 2)
                {
                    StartCoroutine(damageHealth(mySprite));
                }
            }
        }
    }

    private IEnumerator damageHealth(SpriteRenderer sprite)
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.6f);
        health--;
        if (health <= 0)
        {
            transform.gameObject.SetActive(false);
            dead = true;
        }
        sprite.color = Color.white;
    }

    void OnTriggerEnter2D(Collider2D obj)
     {  
        if (obj.gameObject.tag == "Player")
        {
            playerInAttack = true;
        }
     }

    void OnTriggerStay2D(Collider2D obj)
     {  
        if (obj.gameObject.tag == "Player")
        {
            if (!stunned)
            {
                attacking = true;
            }
        }
     }

     void OnTriggerExit2D(Collider2D obj)
     {  
        if (obj.gameObject.tag == "Player")
        {
            playerInAttack = false;
        }
     }
}
