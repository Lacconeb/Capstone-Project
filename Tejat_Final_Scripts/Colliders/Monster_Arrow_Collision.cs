using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Arrow_Collision : MonoBehaviour
{

    private int damage;

    public GameObject charAttacking;

    public void SetAttack(int dmg)
    {
        damage = dmg;
    }
   
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Destroy(gameObject);
            Player scriptComp = col.gameObject.GetComponent<Player>();
            scriptComp.GetHit(damage);
        }
        else if(col.tag == "Monster" || col.tag == "Skill")
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
        

    }

}
