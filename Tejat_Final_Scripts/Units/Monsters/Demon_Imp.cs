using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon_Imp : Monster
{
    int attackDamage;

    public GameObject fireball;
    public GameObject projectilePos;

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

    public override void CleanUpChild()
    {
        
        Vector3 thisObj = this.gameObject.transform.position;
        thisObj.y -= 1;
        this.gameObject.transform.position = thisObj;
        
    }

    public void Attack()
    {
        //Start posiiton for the particle effect to begin at
        Vector3 firePos = new Vector3(projectilePos.transform.position.x, projectilePos.transform.position.y, projectilePos.transform.position.z);

        //A new hit point that has the y value higher to be level with the character
        //Stops the projectile from hitting the ground
        Vector3 newHitPoint = player.transform.position;
        newHitPoint.y = firePos.y;

        //Instantiate the particle projectile at the fire position location.
        GameObject projectile = Instantiate(fireball, firePos, Quaternion.identity) as GameObject;

        Monster_Projectiles projSrc = projectile.GetComponent<Monster_Projectiles>();
        projSrc.SetDamage(attackDamage);

        //Rotate projectile to look at attack point                        
        projectile.transform.LookAt(newHitPoint);

        //Add a rigid body and add force to move it towards the location
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 500);


        //Destroy the particle after a certain amount of time
        //Make sure it doesn't go on forever if it never hits an object
        Destroy(projectile, 7f);
    }


}
