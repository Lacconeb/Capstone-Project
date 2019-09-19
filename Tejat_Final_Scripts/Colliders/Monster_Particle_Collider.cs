using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Particle_Collider : MonoBehaviour
{

    private int attackDamage;
    private bool attackReady = false;

    public void SetUpAttack(int dmg)
    {
        attackDamage = dmg;
        attackReady = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (!attackReady)
            return;

        //Debug.Log(col.tag);
        if (col.tag == "Player")
        {
            Player playerScript = col.GetComponent<Player>();

            playerScript.GetHit(attackDamage);
        }
    }
}
