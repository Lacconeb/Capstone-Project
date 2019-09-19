using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Weapon_Collision : MonoBehaviour
{

    private bool canHit;
    private int damage;

    public Monster monsterScript;
    

    public void SetAttack(int dmg)
    {
        damage = dmg;
        canHit = true;
    }

    public void NoAttack()
    {
        canHit = false;
    }

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(col.tag);
        if (col.tag == "Player")
        {
            if (canHit == true)
            {
                Player scr = col.GetComponent<Player>();
                scr.GetHit(damage);

                canHit = false;
            }
        }
    }



}
