using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratkin_Berserker : Monster
{
    int attackDamage;
    public GameObject weaponColliderObject;
    private Monster_Weapon_Collision weaponCollider;

    public override void SetStats()
    {
        health = 100;
        speed = 3f;
        SetSpeed();
        lookRadius = 20f;
        attackRadius = 2f;
        attackCooldown = 3f;
        attackDamage = 10;

        weaponCollider = weaponColliderObject.GetComponent<Monster_Weapon_Collision>();
    }

    public override void SetUpAttack()
    {
        weaponCollider.SetAttack(attackDamage);
    }

    public void NoAttack()
    {
        weaponCollider.NoAttack();
    }
}
