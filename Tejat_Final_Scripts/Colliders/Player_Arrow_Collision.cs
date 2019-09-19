using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Arrow_Collision : MonoBehaviour
{

    private int damage;


    public void SetAttack(int dmg)
    {
        damage = dmg;
    }
 
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Monster")
        {
            Destroy(gameObject);
            Monster scriptComp = col.gameObject.GetComponent<Monster>();
            scriptComp.GetHit(damage);
        }
        else if(col.tag == "Player" || col.tag == "Skill")
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
        

    }

}
