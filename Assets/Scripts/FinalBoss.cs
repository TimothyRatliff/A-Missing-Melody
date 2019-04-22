using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public float speed;
    private float defaultSpeed;
    private GameObject target;
    public bool stunned;
    public float stunTime = .05f;
    private float defaultStunTime;
    public float attackCooldown = 1;
    private float defaultAttackCooldown;
    private Animator anim;

    public int health = 40;
    Rigidbody2D myBody;
    Transform myTrans;
    private SpriteRenderer mySprite;
    private Color transparent = new Color(1, 1, 1, .8f);
    private bool onepass;
    private bool dead;
    public bool frozen = false;
    public GameObject gateToUnlock;
    public GameObject orbProjectile;
    public GameObject beamProjectile;

    private bool attack;
    private bool hitByProjectile;
    private Transform targetTransform;
    private bool playerInAttack;
    private Vector3 currentRotation;
    private Random rnd;

    void Awake()
    {
        rnd = new Random();
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

            if (hitByProjectile)
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
                    currentRotation = myTrans.eulerAngles;
                    currentRotation.y = 0;
                    myTrans.eulerAngles = currentRotation;
                    myTrans.position = Vector2.MoveTowards(myTrans.position, targetTransform.position, speed * Time.deltaTime);
                }
                else if (myTrans.position.x > targetTransform.position.x)
                {
                    //anim.SetTrigger("isWalking");
                    currentRotation = myTrans.eulerAngles;
                    currentRotation.y = 180;
                    myTrans.eulerAngles = currentRotation;
                    myTrans.position = Vector2.MoveTowards(myTrans.position, targetTransform.position, speed * Time.deltaTime);
                }
                else
                {
                    anim.SetTrigger("isStopped");
                }
                if (!attack)
                {
                    //anim.SetTrigger("isAttacking");
                    if (Random.Range(0, 2) == 0)
                    {
                        GameObject orb = GameObject.Instantiate(orbProjectile, transform.position + (Vector3.up * -1.5f), Quaternion.Euler(currentRotation)); //Needs to face down
                        Vector3 rot = orb.transform.eulerAngles;
                        rot.z = -90;
                        orb.transform.eulerAngles = rot;
                    }
                    else
                        GameObject.Instantiate(beamProjectile, transform.position + (Vector3.up * -1.5f), Quaternion.Euler(currentRotation));
                    attack = true;
                }
                attackCooldown -= Time.deltaTime;
                if (attackCooldown < 0)
                {
                    attack = false;
                    attackCooldown = defaultAttackCooldown;
                }
            }
            //else
            //{
            //    anim.SetTrigger("isStopped");
            //}

            if (health <= 0)
            {
                transform.gameObject.SetActive(false);
                dead = true;
            }
        }
    }

    private void OnDisable()
    {
        if (gateToUnlock != null)
            gateToUnlock.SetActive(false);
    }

    //in Boss projectile code (follows player)
    //StartCoroutine(target.GetComponent<PlayerPlatformerController>().killPlayer());

    private IEnumerator killPlayer()
    {
        yield return new WaitForSeconds(0.3f);
        if (playerInAttack) //Player dies
        {
            StartCoroutine(target.GetComponent<PlayerPlatformerController>().killPlayer());
        }
        else
        {
            speed = defaultSpeed;
            attackCooldown = 1000f;
        }
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

    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            playerInAttack = true;
            StartCoroutine(killPlayer());
            speed = 0;
        }
        if (obj.gameObject.tag == "Projectile")
        {
            hitByProjectile = true;
            Destroy(obj.gameObject);
            StartCoroutine(damageEnemyHealth(mySprite));
        }
    }
    void OnCollisionExit2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            playerInAttack = false;
        }
    }
    }
