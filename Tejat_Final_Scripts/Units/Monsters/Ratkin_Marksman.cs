using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratkin_Marksman : Monster
{

    int attackDamage;

    public GameObject bullet;
    public GameObject projectilePos;

    private Vector3 playerPos;

    public override void SetStats()
    {
        health = 100;
        speed = 3f;
        SetSpeed();
        lookRadius = 20f;
        attackRadius = 7f;
        attackCooldown = 3f;
        attackDamage = 10;

    }

    public override void SetUpAttack()
    {
        playerPos = player.transform.position;
    }

    public void Attack()
    {

        //Start posiiton for the particle effect to begin at
        Vector3 firePos = new Vector3(projectilePos.transform.position.x, projectilePos.transform.position.y, projectilePos.transform.position.z);

        //A new hit point that has the y value higher to be level with the character
        //Stops the projectile from hitting the ground
        Vector3 newHitPoint = playerPos;
        newHitPoint.y = firePos.y;

        //Instantiate the particle projectile at the fire position location.
        GameObject projectile = Instantiate(bullet, firePos, Quaternion.identity) as GameObject;


        //Rotate projectile to look at attack point                        
        projectile.transform.LookAt(newHitPoint);

        //Add a rigid body and add force to move it towards the location
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 500);
      

        Monster_Arrow_Collision arrowScr = projectile.GetComponent<Monster_Arrow_Collision>();
        arrowScr.SetAttack(attackDamage);
        //Destroy the particle after a certain amount of time
        //Make sure it doesn't go on forever if it never hits an object
        Destroy(projectile, 7f);
    }
}
