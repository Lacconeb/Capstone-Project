using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Player : MonoBehaviour
{
    protected string charType;

    protected int health;
    protected int maxHealth;
    protected int speed;

    protected int mana;
    protected int maxMana;
    protected int manaRegenAmount;
    protected float manaRegenRate;
    protected float manaRegenCooldownTimeLeft;

    protected int healthPotionAmountHealed;
    protected float healthPotionCooldown;
    protected float healthPotionCooldownTimeLeft;

    protected int damageReduction;
    protected float damageReductionCooldownTimeLeft;

    protected Animator anim;
    protected NavMeshAgent nav;

    protected bool canWalk = true;
    private bool isDead = false;
    protected bool gettingHit = false;

    public GameObject skill1, skill2, skill3, skill4;
    PlayerInteraction UI;

    private int currentSkill;

    protected float skill1Cooldown = 1f;
    protected float skill1TimeLeft = 0;
    protected int skill1Damage;

    protected float skill2Cooldown = 1f;
    protected float skill2TimeLeft = 0;
    protected int skill2Damage;
    protected int skill2ManaCost;

    protected float skill3Cooldown = 1f;
    protected float skill3TimeLeft = 0;
    protected int skill3Damage;
    protected int skill3ManaCost;

    protected float skill4Cooldown = 1f;
    protected float skill4TimeLeft = 0;
    protected int skill4Damage;
    protected int skill4ManaCost;

    protected RaycastHit attackRaycastHit;
    
    protected bool buffActive;
    protected float buffUpTime = 10f;
    protected float buffTimeLeft = 0;

    protected int buffDamageModifier = 0;
    protected float buffSpeedModifier = 0;


    // Start is called before the first frame update
    void Awake()
    {

        //Set animator from gameobject
		anim = GetComponent<Animator> ();

        //Set NavMeshAgent from gameobject
        //used for movement
        nav = GetComponent<NavMeshAgent> ();

        SetStats();

        UI = GameObject.FindGameObjectWithTag("Player_Interaction").GetComponent<PlayerInteraction>();
    }


    // Update is called once per frame
    void Update()
    {
        if (isDead == true || gettingHit == true)
        {
            return;
        }

        ManaRegen();
        
        UISkillsAvailable();
        
        isHealReady();

        //takes care of the controls for the main character
        Controls();

        //stops the player movement if player is close enough to the destination
        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            Stop();

        }

        BuffCountDown();

        DamageReductionCountdown();

    }

    //A function for setting the controls of the main character
    //Controls include walking and skill1-skill4.
    private void Controls()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Heal();
        }

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 200f))
        {
            //Left click
            if (Input.GetMouseButton(0))   
            {
                //Left Shift - will stop movement if moving
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    //If skill1 is off cooldown then proceed to use skill1
                    if(Time.time > skill1TimeLeft)
                    {

                        attackRaycastHit = hit;

                        currentSkill = 1;

                        Skill1();
                        
                    }
                    else
                    {
                        //float foo = skill1TimeLeft - Time.time;
                        //Debug.Log("Cooldown left: " + foo);
                    }


                }
                else if(hit.collider.tag == "Monster")
                {
                    //If skill1 is off cooldown then proceed to use skill1
                    if (Time.time > skill1TimeLeft)
                    {

                        attackRaycastHit = hit;

                        currentSkill = 1;

                        Skill1();

                    }
                }
                else
                {
                    //Debug.Log("X: " + (int)hit.point.x + " / Z: " + (int)hit.point.x);
                    Walk(hit);
                }

            }//Right click
            else if (Input.GetMouseButton(1))
            {
                
                //If skill1 is off cooldown then proceed to use skill2
                if (Time.time > skill2TimeLeft)
                {
                    if (mana - skill2ManaCost >= 0)
                    {

                        attackRaycastHit = hit;

                        currentSkill = 2;

                        Skill2();
                    }
                }

            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                
                if (Time.time > skill3TimeLeft)
                {
                    if (mana - skill3ManaCost >= 0)
                    {

                        attackRaycastHit = hit;

                        currentSkill = 3;

                        Skill3();
                    }

                }
                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (Time.time > skill4TimeLeft)
                {
                    if (mana - skill4ManaCost >= 0)
                    {

                        attackRaycastHit = hit;

                        currentSkill = 4;

                        Skill4();
                    }
                }
            }

        }


    }


    private void ManaRegen()
    {
        
        if (mana >= maxMana)
            return;

        
        if (Time.time > manaRegenCooldownTimeLeft)
        {
            mana += manaRegenAmount;
            
            if(mana > maxMana)
            {
                mana = maxMana;
            }

            manaRegenCooldownTimeLeft = Time.time + manaRegenRate;
        }

        Debug.Log("Mana = " + mana);

    }


    public void UISkillsAvailable()
    {
        //skill2
        if (skill2ManaCost <= mana)
        {
            UI.showSkillLowMana("skill2", false);
            //Debug.Log("Send Skill2 True = " + mana);
        }
        else
        {
            UI.showSkillLowMana("skill2", true);
            //Debug.Log("Send Skill2 False = " + mana);
        }
        
        //skill3
        if (skill3ManaCost <= mana)
        {
            UI.showSkillLowMana("skill3", false);
            //Debug.Log("Send Skill3 True = " + mana);
        }
        else
        {
            UI.showSkillLowMana("skill3", true);
            //Debug.Log("Send Skill3 False = " + mana);
        }

        //skill4
        if (skill4ManaCost <= mana)
        {
            UI.showSkillLowMana("skill4", false);
            //Debug.Log("Send Skill4 True = " + mana);
        }
        else
        {
            UI.showSkillLowMana("skill4", true);
            //Debug.Log("Send Skill4 False = " + mana);
        }

    }
    

    public void UseMana(int manaAmount)
    {
        mana -= manaAmount;
    }

    public void Heal()
    {

        if (health == maxHealth)
            return;
        
        if (isHealReady())
        {
            int healthTemp;

            healthTemp = health + healthPotionAmountHealed;

            if(healthTemp > maxHealth)
            {
                healthTemp = maxHealth;
            }

            health = healthTemp;

            Debug.Log("Health = " + health);

            healthPotionCooldownTimeLeft = Time.time + healthPotionCooldown;

        }
        
    }

    
    public bool isHealReady() {
        // heal is ready
        if (Time.time > healthPotionCooldownTimeLeft) {
            UI.showHealCooldown(false);
            return true;
        // heal is on cooldown
        } else {
            UI.showHealCooldown(true);
            return false;
            Debug.Log("Health potion is on cooldown");
        }
    }
    
    public void SetDamageReduction(int amount, float coolDownTime)
    {
        
        damageReduction = amount;
        damageReductionCooldownTimeLeft = coolDownTime;
    }

    private void DamageReductionCountdown()
    {
        if (damageReduction == 0)
            return;

        if (Time.time > damageReductionCooldownTimeLeft)
        {
            damageReduction = 0;
        }
    }
    public virtual void SetStats()
    {
        health = 500;
    }

    /*
     * ************************************************************* 
     *                     Movement Functions
     * ************************************************************* 
    */
    public void Walk(RaycastHit hit)
    {
        if (canWalk == false)
            return;

        anim.SetBool("IsWalking", true);
        nav.isStopped = false;
        nav.destination = hit.point;
    }

    public void Stop()
    {
        nav.isStopped = true;
        anim.SetBool("IsWalking", false);
    }

    public void AllowWalking()
    {
        canWalk = true;
        //Debug.Log("Walking = " + walking);
    }

    public void NoWalking()
    {
        canWalk = false;
        //Debug.Log("Walking = " + walking);
    }

    /*
    * ************************************************************* 
    *                     Skill Functions
    * ************************************************************* 
   */

    public virtual void Skill1()
    {
        //Skills are on children
    }

    public virtual void Skill2()
    {
        //Skills are on children
    }

    public virtual void Skill3()
    {
        //Skills are on children
    }

    public virtual void Skill4()
    {
        //Skills are on children
    }

    public int GetCurrentSkillDamage()
    {
        if (currentSkill == 1)
            return skill1Damage + buffDamageModifier;
        else if (currentSkill == 2)
            return skill2Damage + buffDamageModifier;
        else if (currentSkill == 3)
            return skill3Damage + buffDamageModifier;
        else if (currentSkill == 4)
            return skill4Damage + buffDamageModifier;
        else
        {
            Debug.Log("ERROR!!!! In Player.GetCurrentSkillDamage()");
            return 25;
        }

    }


    /*
    * ************************************************************* 
    *                    Taking Damage Functions
    * ************************************************************* 
   */

    public void GetHit(int damage)
    {
        int newDamage = damage - damageReduction;
        if (newDamage < 0)
            newDamage = 0;

        health -= newDamage;

        if (health <= 0)
        {
            
            Debug.Log("Player is Dead!!!!");
            Die();
        }
        else
        {

            if (canWalk == true && charType == "range")
            {
                HitBegin();
                anim.SetTrigger("GetHit");
            }
            

            Debug.Log("Player got hit for " + newDamage + "/// Health = " + health);
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
        isDead = true;
        anim.SetBool("IsDead", true);
        anim.SetBool("IsWalking", false);
    }


    /*
    * ************************************************************* 
    *                       Buff Functions
    * ************************************************************* 
    */

    public void BuffPlayer(string buffType, float amount, float upTime)
    {

        //cancel any other buff that is currently on
        EndBuff();

        buffActive = true;
        UI.buffVisible(buffType, true);

        if (buffType == "damage")
        {
            buffDamageModifier = (int)amount;            
        }
        else if(buffType == "speed")
        {
            buffSpeedModifier = amount;
            SetSpeed();            
        }
        else
        {
            Debug.Log("Player.BuffPlayer call didn't work properly");
        }

        buffUpTime = upTime;

        buffTimeLeft = Time.time + buffUpTime;
    }

    private void BuffCountDown()
    {
        if (buffActive == false)
            return;

        if(Time.time > buffTimeLeft)
        {
            EndBuff();
        }

    }

    private void EndBuff()
    {
        buffDamageModifier = 0;

        buffSpeedModifier = 0;
        SetSpeed();

        buffActive = false;
        UI.buffVisible("damage", false);
        UI.buffVisible("speed", false);
        
    }

    private void SetSpeed()
    {
        nav.speed = speed + buffSpeedModifier;
    }

    /*
    * ************************************************************* 
    *                       UI Helper Functions
    * ************************************************************* 
    */

    public int GetHealth()
    {
        return health;
    }

    public int GetMana()
    {
        return mana;
    }

    public bool isPlayerDead() {
        return isDead;
    }

}
