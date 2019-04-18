using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float speed;
    private float defaultSpeed;
    public float headHight = 1f;
    private GameObject target;
    public bool stunned, canAttack;
    public float stunTime = 3;
    private float defaultStunTime;
    public float attackCooldown = 3;
    private float defaultAttackCooldown;
    private Animator anim;

    public int health = 1;
    Rigidbody2D myBody;
    Transform myTrans;
    public GameObject itemToSpawn;
    private SpriteRenderer mySprite;
    private Color transparent = new Color(1, 1, 1, .8f);
    private Rigidbody2D rb;
    private bool onepass;
    private bool dead;
    public bool frozen;
    public GameObject gateToUnlock;

    private bool playerInAttack = false;
    private bool attack;
    private bool hitByProjectile;
    private Transform targetTransform;

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
        rb = this.GetComponent<Rigidbody2D>();
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

            }
            else if (!stunned && !frozen)
            {
                if (myTrans.position.x < targetTransform.position.x)
                {
                    //anim.SetTrigger("isWalking");
                    Vector3 currentRotation = myTrans.eulerAngles;
                    currentRotation.y = 0;
                    myTrans.eulerAngles = currentRotation;
                    //myTrans.position = Vector2.MoveTowards(myTrans.position, targetTransform.position, speed * Time.deltaTime);
                }
                else if (myTrans.position.x > targetTransform.position.x)
                {
                    //anim.SetTrigger("isWalking");
                    Vector3 currentRotation = myTrans.eulerAngles;
                    currentRotation.y = 180;
                    myTrans.eulerAngles = currentRotation;
                    //myTrans.position = Vector2.MoveTowards(myTrans.position, targetTransform.position, speed * Time.deltaTime);
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
                transform.gameObject.SetActive(false);
                dead = true;
            }
        }
    }

    private void OnDisable()
    {
        //Item Spawner//
        SpawnItem(itemToSpawn);
        if (gateToUnlock != null)
            gateToUnlock.SetActive(false);
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
    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            Vector3 direction = transform.position - obj.gameObject.transform.position;
            if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y) && stunned)
            {
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

    //Used by BossCollisionActivator to allow player to walk through boss
    public void enableCollision()
    {
        rb.simulated = true;
    }
}
