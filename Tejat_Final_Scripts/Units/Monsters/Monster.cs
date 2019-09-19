using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Monster : MonoBehaviour
{

    protected int health;
    protected float speed;

    protected float lookRadius;
    protected float attackRadius;

    protected Animator anim;
    protected NavMeshAgent nav;

    public GameObject player;

    protected Monster_Weapon_Collision weaponCol;

    protected bool walking;
    protected bool canWalk = true;

    protected bool gettingHit = false;

    protected bool isDead = false;

    protected float attackCooldown;
    protected float attackTimeLeft = 0;



    // Start is called before the first frame update
    void Awake()
    {
        
        //Set animator from gameobject
        anim = GetComponent<Animator>();

        //Set NavMeshAgent from gameobject
        //used for movement
        nav = GetComponent<NavMeshAgent>();

        SetStats();

        SetScripts();

    }

    private void Start()
    {
        //Find and set the player in the scene
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead == true || gettingHit == true)
        {
            
            return;
        }

        anim.SetBool("IsWalking", walking);

        float distFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distFromPlayer <= lookRadius)
        {
            MoveAndAttack();
        }
    }


    public void MoveAndAttack()
    {

        if (canWalk == false)
            return;

        nav.destination = player.transform.position;

        if (!nav.pathPending && nav.remainingDistance > attackRadius)
        {

            
            //move towards target
            nav.isStopped = false;
            walking = true;
           
        }
        else if (!nav.pathPending && nav.remainingDistance <= attackRadius)
        {

            //attack target

            nav.isStopped = true;
            walking = false;
            anim.SetBool("IsWalking", walking);

            if (Time.time > attackTimeLeft)
            {
                attackTimeLeft = Time.time + attackCooldown;

                SetUpAttack();

                transform.LookAt(player.transform);
                anim.SetTrigger("Attack");

                
            }

        }
    }


    public virtual void SetStats()
    {
        health = 100;
    }


    public virtual void SetScripts(){
        //Scripts are on some derived classes
    }

    public virtual void SetUpAttack()
    {
        //Extra attack setups are needed for some derived classes
    }

    public void AllowWalking()
    {
        canWalk = true;
        //Debug.Log("Allow Walk");
    }

    public void NoWalking()
    {
        canWalk = false;
        nav.isStopped = true;
        //Debug.Log("No Walk");
    }


    public void GetHit(int damage)
    {
        HitBegin();

        Debug.Log("*******************");
        Debug.Log("Health Before = " + health);

        health -= damage;

        Debug.Log("Monster was hit = " + damage);
        Debug.Log("Health After= " + health);
        Debug.Log("*******************");

        if (health <= 0)
        {
            Die();
        }
        else
        {
            anim.SetTrigger("GetHit");
        }

    }

    public void HitBegin()
    {
        //Debug.Log("HIT start");
        gettingHit = true;
        nav.isStopped = true;
        anim.SetBool("IsWalking", false);
    }

    public void HitEnd()
    {
        //Debug.Log("HIT end");
        gettingHit = false;
    }

    private void Die()
    {
        nav.isStopped = true;
        isDead = true;
        anim.SetBool("IsDead", true);
        anim.SetBool("IsWalking", false);
        CleanUp();
        CleanUpChild();
    }

    private void CleanUp()
    {
        NavMeshAgent navComp = this.gameObject.GetComponent < NavMeshAgent >();
        navComp.enabled = !navComp.enabled;

        BoxCollider boxComp = this.gameObject.GetComponent<BoxCollider>();
        boxComp.enabled = !boxComp.enabled;
    }

    public virtual void CleanUpChild()
    {
        //Needed for monsters that need an extra cleanup stage
    }

    public void SetSpeed()
    {
        nav.speed = speed;
    }

}
