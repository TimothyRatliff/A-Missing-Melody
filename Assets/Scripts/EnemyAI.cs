using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool flipX;
    public float speed;
    private float defaultSpeed;
    public float headHight = 1f;
    private Transform targetTransform;
    public bool stunned, canAttack;
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
    private bool attack;
    private GameObject target;
    public float distance = 5f;
    private bool hitByProjectile;

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
            target = GameObject.FindGameObjectWithTag("Player");
            targetTransform = target.GetComponent<Transform>();

            if (target.GetComponent<PlayerAbilities>().whistleFired || hitByProjectile)
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
                    hitByProjectile = false;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>().whistleFired = false;
                    speed = defaultSpeed;
                    stunTime = defaultStunTime;
                }

            } else if (!stunned && !frozen) {
                if (myTrans.position.x < targetTransform.position.x && Vector2.Distance(myTrans.position, targetTransform.position) > distance)
                {
                    anim.SetTrigger("isWalking");
                    Vector3 currentRotation = myTrans.eulerAngles;
                    if (flipX)
                        currentRotation.y = 0;
                    else
                        currentRotation.y = 180;
                    myTrans.eulerAngles = currentRotation;
                    myTrans.position = Vector2.MoveTowards(myTrans.position, targetTransform.position, speed * Time.deltaTime);
                }
                else if (myTrans.position.x > targetTransform.position.x && Vector2.Distance(myTrans.position, targetTransform.position) > distance)
                {
                    anim.SetTrigger("isWalking");
                    Vector3 currentRotation = myTrans.eulerAngles;
                    if (flipX)
                        currentRotation.y = 180;
                    else
                        currentRotation.y = 0;
                    myTrans.eulerAngles = currentRotation;
                    myTrans.position = Vector2.MoveTowards(myTrans.position, targetTransform.position, speed * Time.deltaTime);
                }
                else
                {
                    anim.SetTrigger("isStopped");
                }
                if (canAttack)
                {
                    if (!attack)
                    {
                        anim.SetTrigger("isAttacking");
                        attack = true;
                        StartCoroutine(killPlayer());
                        speed = 0;
                    }
                    attackCooldown -= Time.deltaTime;
                    if (attackCooldown < 0)
                    {
                        attack = false;
                        canAttack = false;
                        speed = defaultSpeed;
                        attackCooldown = defaultAttackCooldown;
                    }
                }
            }
            else
            {
                anim.SetTrigger("isStopped");
            }

            if (health <= 0)
            {
                SpawnItem(itemToSpawn);
                transform.gameObject.SetActive(false);
                dead = true;
            }
        }
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
                if (Mathf.Abs(direction.y) > headHight && Mathf.Abs(direction.x) < headHight)
                {
                    StartCoroutine(damageEnemyHealth(mySprite));
                }
            }
        }
    }

    private IEnumerator killPlayer()
    {
        yield return new WaitForSeconds(0.4f);
        if (playerInAttack) //Player dies
            StartCoroutine(target.GetComponent<PlayerPlatformerController>().killPlayer());
    }

    private IEnumerator damageEnemyHealth(SpriteRenderer sprite)
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.6f);
        health--;
        if (health <= 0)
        {
            SpawnItem(itemToSpawn);
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
        if (obj.gameObject.tag == "Projectile")
        {
            hitByProjectile = true;
            Destroy(obj.gameObject);
            StartCoroutine(damageEnemyHealth(mySprite));
        }
    }

    void OnTriggerStay2D(Collider2D obj)
     {  
        if (obj.gameObject.tag == "Player")
        {
            if (!stunned)
            {
                canAttack = true;
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
