using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Collision : MonoBehaviour
{

    Player playerScript;

    // Start is called before the first frame update
    void Awake()
    {
        //Find and set the player in the scene
        GameObject player = GameObject.FindWithTag("Player");

        playerScript = player.GetComponent<Player>();
    }

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(col.tag);
        if (col.tag == "Monster")
        {
            Monster enemyScript = col.GetComponent<Monster>();

            enemyScript.GetHit(playerScript.GetCurrentSkillDamage());
        }
    }

}
