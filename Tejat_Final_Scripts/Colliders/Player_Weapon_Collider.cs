using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon_Collider : MonoBehaviour
{
    private bool canHit;
    private int damage;

    public void SetAttack(int dmg)
    {
        damage = dmg;
        canHit = true;
    }

    public void StopAttack()
    {
        damage = 0;
        canHit = false;
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Monster")
        {

            if (canHit == true)
            {
                
                Monster scr = col.GetComponent<Monster>();
                scr.GetHit(damage);

                
            }
        }
    }

}
