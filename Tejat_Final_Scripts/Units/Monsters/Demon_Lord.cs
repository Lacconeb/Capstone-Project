using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon_Lord : Monster
{
    int attackDamage;

    public GameObject attackObj;

    public override void SetStats()
    {
        health = 500;
        speed = 3f;
        SetSpeed();
        lookRadius = 20f;
        attackRadius = 10f;
        attackCooldown = 3f;
        attackDamage = 20;

    }

    public void Attack()
    {
        Quaternion rot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        GameObject skillObject = Instantiate(attackObj, transform.position, rot) as GameObject;

        Monster_Particle_Collider attackScr = skillObject.GetComponent<Monster_Particle_Collider>();
        attackScr.SetUpAttack(attackDamage);

        Destroy(skillObject, 0.7f);

    }
}
