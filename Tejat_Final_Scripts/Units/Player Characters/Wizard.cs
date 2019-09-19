using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Player
{

    private int skill4DamageReduction = 30;
    private float skill4DamageReductionTime = 5f;

    public override void SetStats()
	{

        charType = "range";

        maxHealth = 100;
        health = maxHealth;
        speed = 6;

        maxMana = 100;
        mana = maxMana;

        manaRegenAmount = 4;
        manaRegenRate = 0.5f;
        manaRegenCooldownTimeLeft = 0;

        healthPotionAmountHealed = 50;
        healthPotionCooldown = 5f;
        healthPotionCooldownTimeLeft = 0;

        skill1Damage = 50;
        skill1Cooldown = 1f;

        skill2Damage = 100;
        skill2ManaCost = 50;
        skill2Cooldown = 1f;

        skill3ManaCost = 10;
        skill3Cooldown = 5f;

        skill4ManaCost = 20;
        skill4Cooldown = 10f;

    }

    public override void Skill1()
    {
        //set skill1 cooldown time
        skill1TimeLeft = Time.time + skill1Cooldown;

        Vector3 newHitPoint = attackRaycastHit.point;
        newHitPoint.y = transform.position.y;

        //Stop character movement in navmesh and animation
        Stop();
        
        //rotate player to face the attack point
        transform.LookAt(attackRaycastHit.point);

        //trigger skill1 animation
        anim.SetTrigger("Skill1");

    }

    public void FireSkill1()
    {
        //Find "Projectile Position" on character prefab
        //This will change once animation callbacks are implimented
        GameObject projectilePos = GameObject.FindGameObjectWithTag("Projectile_Position");

        //Start posiiton for the particle effect to begin at
        Vector3 firePos = new Vector3(projectilePos.transform.position.x, projectilePos.transform.position.y, projectilePos.transform.position.z);

        //A new hit point that has the y value higher to be level with the character
        //Stops the projectile from hitting the ground
        Vector3 newHitPoint = attackRaycastHit.point;
        newHitPoint.y = firePos.y;

        //Instantiate the particle projectile at the fire position location.
        GameObject projectile = Instantiate(skill1, firePos, Quaternion.identity) as GameObject;

        //Rotate projectile to look at attack point                        
        projectile.transform.LookAt(newHitPoint);
        //Add a rigid body and add force to move it towards the location
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 1000);
        //Impact Normal is a setting of the store asset and will determine the size of impact
        projectile.GetComponent<Projectiles>().impactNormal = attackRaycastHit.normal;
        //Destroy the particle after a certain amount of time
        //Make sure it doesn't go on forever if it never hits an object
        Destroy(projectile, 7f);

    }

    public override void Skill2()
    {
        //set skill2 cooldown time
        skill2TimeLeft = Time.time + skill2Cooldown;

        UseMana(skill2ManaCost);

        //Stop character movement in navmesh and animation
        Stop();

        //rotate player to face the attack point
        transform.LookAt(attackRaycastHit.point);

        //trigger skill1 animation
        anim.SetTrigger("Skill2");

    }

    public void FireSkill2()
    {

        Vector3 newHitPoint = attackRaycastHit.point;
        newHitPoint.y += 0.2f;

        Quaternion rot = Quaternion.Euler(-89.98f,0,0);

        //Instantiate the particle at click location
        GameObject skill2Object = Instantiate(skill2, newHitPoint, rot) as GameObject;
        Destroy(skill2Object, 0.7f);
    }

    public override void Skill3()
    {
        //set skill3 cooldown time
        skill3TimeLeft = Time.time + skill3Cooldown;

        UseMana(skill3ManaCost);

        Stop();

        transform.LookAt(attackRaycastHit.point);

        Quaternion rot = Quaternion.Euler(-90f, 0, 0);

        GameObject skill3Start = Instantiate(skill3, transform.position, rot) as GameObject;
        Destroy(skill3Start, 2f);

        transform.position += transform.forward * 10;

        GameObject skill3End = Instantiate(skill3, transform.position, rot) as GameObject;
        Destroy(skill3End, 2f);
    }

    public override void Skill4()
    {
       
        //set skill4 cooldown time
        skill4TimeLeft = Time.time + skill4Cooldown;
        UseMana(skill4ManaCost);

        float timeReduct = Time.time + skill4DamageReductionTime;
        SetDamageReduction(skill4DamageReduction, timeReduct);

        Vector3 newPos = transform.position;
        newPos.y += 1.2f;

        GameObject skill4Object = Instantiate(skill4, newPos, Quaternion.identity) as GameObject;
        skill4Object.transform.parent = transform;
        Destroy(skill4Object, skill4DamageReductionTime);
    }
}

