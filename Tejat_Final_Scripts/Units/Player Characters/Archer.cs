using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Player
{

    public GameObject arrow;

    public GameObject projectilePos;

    private float arrowYRotationOffset = 92f;

    public override void SetStats()
    {

        charType = "range";

        maxHealth = 100;
        health = maxHealth;
        speed = 8;

        maxMana = 100;
        mana = maxMana;

        manaRegenAmount = 2;
        manaRegenRate = 0.5f;
        manaRegenCooldownTimeLeft = 0;

        healthPotionAmountHealed = 50;
        healthPotionCooldown = 5f;
        healthPotionCooldownTimeLeft = 0;

        skill1Damage = 25;
        skill1Cooldown = 0.7f;

        skill2Damage = 50;
        skill2ManaCost = 10;
        skill2Cooldown = 1f;

        skill3Damage = 100;
        skill3ManaCost = 25;
        skill3Cooldown = 1f;

        skill4Damage = 50;
        skill4ManaCost = 75;
        skill4Cooldown = 1f;

    }

    public void HideArrow()
    {
        arrow.SetActive(false);
    }

    public void ShowArrow()
    {
        arrow.SetActive(true);
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

        //Set new projectile rotation because rotation is off
        float newNum = projectile.transform.eulerAngles.y + arrowYRotationOffset;
        projectile.transform.eulerAngles = new Vector3(projectile.transform.eulerAngles.x, newNum, projectile.transform.eulerAngles.z);

        Player_Arrow_Collision arrowScr = projectile.GetComponent<Player_Arrow_Collision>();
        arrowScr.SetAttack(skill1Damage);

        Destroy(projectile, 7f);
    }

    public override void Skill2()
    {

        //set skill1 cooldown time
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
        //Start posiiton for the particle effect to begin at
        Vector3 firePos = new Vector3(projectilePos.transform.position.x, projectilePos.transform.position.y, projectilePos.transform.position.z);


        //A new hit point that has the y value higher to be level with the character
        //Stops the projectile from hitting the ground
        Vector3 newHitPoint = attackRaycastHit.point;
        newHitPoint.y = firePos.y;

        //Instantiate the particle projectile at the fire position location.
        GameObject projectile = Instantiate(skill2, firePos, Quaternion.identity) as GameObject;

        //Rotate projectile to look at attack point                        
        projectile.transform.LookAt(newHitPoint);

        //Add a rigid body and add force to move it towards the location
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 1000);

        //Set new projectile rotation because rotation is off
        float newNum = projectile.transform.eulerAngles.y + arrowYRotationOffset;
        projectile.transform.eulerAngles = new Vector3(projectile.transform.eulerAngles.x, newNum, projectile.transform.eulerAngles.z);

        Player_Arrow_Pierce_Collision arrowScr = projectile.GetComponent<Player_Arrow_Pierce_Collision>();
        arrowScr.SetAttack(skill2Damage);

        Destroy(projectile, 7f);
    }

    public override void Skill3()
    {

        //set skill3 cooldown time
        skill3TimeLeft = Time.time + skill3Cooldown;

        UseMana(skill3ManaCost);

        //Stop character movement in navmesh and animation
        Stop();

        //rotate player to face the attack point
        transform.LookAt(attackRaycastHit.point);

        //trigger skill3 animation
        anim.SetTrigger("Skill3");

    }

    public void FireSkill3()
    {
        //Start posiiton for the particle effect to begin at
        Vector3 firePos = new Vector3(projectilePos.transform.position.x, projectilePos.transform.position.y, projectilePos.transform.position.z);

        //A new hit point that has the y value higher to be level with the character
        //Stops the projectile from hitting the ground
        Vector3 newHitPointTemp = attackRaycastHit.point;
        newHitPointTemp.y = firePos.y;

        Vector3 newHitPoint = newHitPointTemp;

        ArrowInstantiateHelper(skill3, firePos, newHitPoint, skill3Damage);
        Debug.Log("1 - " + newHitPoint);

        newHitPoint.x += 1f;
        newHitPoint.z += 1f;

        ArrowInstantiateHelper(skill3, firePos, newHitPoint, skill3Damage);
        Debug.Log("2 - " + newHitPoint);

        newHitPoint = newHitPointTemp;

        newHitPoint.x -= 1f;
        newHitPoint.z -= 1f;

        ArrowInstantiateHelper(skill3, firePos, newHitPoint, skill3Damage);
        Debug.Log("3 - " + newHitPoint);
    }

    public void ArrowInstantiateHelper(GameObject skillObject, Vector3 firePosition, Vector3 attackPoint, int skillDmg)
    {
               

        //Instantiate the particle projectile at the fire position location.
        GameObject projectile = Instantiate(skillObject, firePosition, Quaternion.identity) as GameObject;

        //Rotate projectile to look at attack point                        
        projectile.transform.LookAt(attackPoint);

        //Add a rigid body and add force to move it towards the location
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 1000);

        //Set new projectile rotation because rotation is off
        float newNum = projectile.transform.eulerAngles.y + arrowYRotationOffset;
        projectile.transform.eulerAngles = new Vector3(projectile.transform.eulerAngles.x, newNum, projectile.transform.eulerAngles.z);

        Player_Arrow_Collision arrowScr = projectile.GetComponent<Player_Arrow_Collision>();
        arrowScr.SetAttack(skillDmg);

        Destroy(projectile, 7f);
    }

    public override void Skill4()
    {
        //set skill4 cooldown time
        skill4TimeLeft = Time.time + skill4Cooldown;

        UseMana(skill4ManaCost);

        //Stop character movement in navmesh and animation
        Stop();

        //rotate player to face the attack point
        transform.LookAt(attackRaycastHit.point);

        //trigger skill4 animation
        //anim.SetTrigger("Skill3");

        Vector3 newHitPoint = attackRaycastHit.point;
        //newHitPoint.y += 0.2f;

        GameObject skillObject = Instantiate(skill4, newHitPoint, Quaternion.identity) as GameObject;

        Turret turretScr = skillObject.GetComponent<Turret>();
        turretScr.SetUpSkill(skill1, skill4Damage);

        Destroy(skillObject, 7f);
    }
}
