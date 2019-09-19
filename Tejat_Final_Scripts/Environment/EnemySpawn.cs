//inspiration for this script from https://www.youtube.com/watch?v=xHjwGJIbW60

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private int triggerDamage;
    public GameObject enemy;
    public Transform enemyPos = null;
    public Transform enemyPos2 = null;
    public Transform enemyPos3 = null;
    public Transform enemyPos4 = null;
    public Transform enemyPos5 = null;
    public Transform enemyPos6 = null;
    private int collisionCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("enemy spawn script started");
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("entered trigger outer");

        if(other.gameObject.tag == "Player")
        {

            Debug.Log("Entered trigger");

            collisionCount++; //count collisions to limit spawning

            //do damage to player if triggerDamage set
            if(triggerDamage > 0)
            {
                Debug.Log("Get Hit");
                Player playerScript = other.gameObject.GetComponent<Player>();
                playerScript.GetHit(triggerDamage);
            }

            //only spawn on first collision
            if(collisionCount < 2) {enemySpawner();}

            //gameObject.GetComponent<Collider>().enabled = false;
            
        }
    }

    void enemySpawner()
    {
        if(enemyPos){Instantiate(enemy, enemyPos.position, enemyPos.rotation);}
        if(enemyPos2){Instantiate(enemy, enemyPos2.position, enemyPos2.rotation);}
        if(enemyPos3){Instantiate(enemy, enemyPos3.position, enemyPos3.rotation);}
        if(enemyPos4){Instantiate(enemy, enemyPos4.position, enemyPos4.rotation);}
        if(enemyPos5){Instantiate(enemy, enemyPos5.position, enemyPos5.rotation);}
        if(enemyPos6){Instantiate(enemy, enemyPos6.position, enemyPos6.rotation);}
    }
}
