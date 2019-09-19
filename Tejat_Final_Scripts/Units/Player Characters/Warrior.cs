using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Player
{

    public GameObject weaponObject;
    private Player_Weapon_Collider weaponCollider;

    public GameObject skill2Position;
    public GameObject skill4Position;

    public GameObject skill4Impact;

    public override void SetStats()
    {

        charType = "melee";

        maxHealth = 100;
        health = maxHealth;
        speed = 6;

        maxMana = 100;
        mana = maxMana;

        manaRegenAmount = 2;
        manaRegenRate = 0.5f;
        manaRegenCooldownTimeLeft = 0;

        healthPotionAmountHealed = 100;
        healthPotionCooldown = 2.5f;
        healthPotionCooldownTimeLeft = 0;

        skill1Damage = 50;
        skill1Cooldown = 0.5f;

        skill2Damage = 50;
        skill2ManaCost = 10;
        skill2Cooldown = 1f;

        skill3Damage = 75;
        skill3ManaCost = 20;
        skill3Cooldown = 1f;

        skill4Damage = 150;
        skill4ManaCost = 50;
        skill4Cooldown = 1f;

        weaponCollider = weaponObject.GetComponent<Player_Weapon_Collider>();
    }

    public void TurnWeaponOn()
    {
        weaponCollider.SetAttack(GetCurrentSkillDamage());
    }

    public void TurnWeaponOff()
    {
        weaponCollider.StopAttack();
    }

    public override void Skill1()
    {

        //set skill1 cooldown time
        skill1TimeLeft = Time.time + skill1Cooldown;

        //Stop character movement in navmesh and animation
        Stop();

        //rotate player to face the attack point
        transform.LookAt(attackRaycastHit.point);

        //trigger skill1 animation
        anim.SetTrigger("Skill1");

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

        Vector3 newHitPoint = new Vector3(skill2Position.transform.position.x, skill2Position.transform.position.y, skill2Position.transform.position.z);

        Quaternion rot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 180f);

        //Instantiate the particle at click location
        GameObject skill2Object = Instantiate(skill2, newHitPoint, rot) as GameObject;
        //Rotate projectile to look at attack point                        
        skill2Object.transform.LookAt(newHitPoint);

        Destroy(skill2Object, 0.5f);
    }

    public override void Skill3()
    {
        //set skill4 cooldown time
        skill3TimeLeft = Time.time + skill3Cooldown;

        UseMana(skill3ManaCost);

        //Stop character movement in navmesh and animation
        Stop();

        //rotate player to face the attack point
        transform.LookAt(attackRaycastHit.point);

        //trigger skill1 animation
        anim.SetTrigger("Skill3");

    }

    public override void Skill4()
    {
        //set skill3 cooldown time
        skill4TimeLeft = Time.time + skill3Cooldown;

        UseMana(skill4ManaCost);

        //Stop character movement in navmesh and animation
        Stop();

        //rotate player to face the attack point
        transform.LookAt(attackRaycastHit.point);

        //trigger skill1 animation
        anim.SetTrigger("Skill4");

    }

    public void FireSkill4()
    {

        Vector3 newHitPoint = new Vector3(skill4Position.transform.position.x, skill4Position.transform.position.y, skill4Position.transform.position.z);
        
        Quaternion rot = Quaternion.Euler(-90f, 180f, transform.eulerAngles.z);

        //Instantiate the particle at click location
        GameObject skill4Object = Instantiate(skill4Impact, newHitPoint, rot) as GameObject;
        //Rotate projectile to look at attack point                        
        skill4Object.transform.LookAt(newHitPoint);

        Destroy(skill4Object, 2f);

        rot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        //Instantiate the particle at click location
        GameObject skill4Object2 = Instantiate(skill4, newHitPoint, rot) as GameObject;
        //Rotate projectile to look at attack point                        
        //skill3Object2.transform.LookAt(newHitPoint);

        Destroy(skill4Object2, 2f);
    }

    
}
