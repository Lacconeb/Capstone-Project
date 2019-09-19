using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Fighter : Monster
{

    int attackDamage;

    public override void SetStats()
    {
        health = 100;
        speed = 3f;
        SetSpeed();
        lookRadius = 20f;
        attackRadius = 2f;
        attackCooldown = 3f;
        attackDamage = 10;

    }

    public override void SetScripts()
    {
        //GameObject parent = this.gameObject;
        weaponCol = this.gameObject.GetComponentInChildren<Monster_Weapon_Collision>();
    }

    public override void SetUpAttack()
    {
        weaponCol.SetAttack(attackDamage);
    }

}
