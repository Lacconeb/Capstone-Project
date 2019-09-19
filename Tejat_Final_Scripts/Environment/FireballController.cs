using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{

    public Transform fireballEmitter;
    public GameObject fireball;

    private Vector3 playerPos;
    private Vector3 playerNormal;
    private RaycastHit playerRayHit;
    [SerializeField] private int damage;

    // Start is called before the first frame update
    void Start()
    {
        
        // ProjectilesEnv fireballScript = fireball.GetComponent<ProjectilesEnv>();
        // fireballScript.projectileDamage = damage;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("entered trigger outer");

        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player entered trigger");
            playerPos = other.transform.position;
            Physics.Raycast(Camera.main.ScreenPointToRay(playerPos), out RaycastHit hit, 200f);
            playerRayHit = hit;
            deployFireball();
            gameObject.GetComponent<Collider>().enabled = false;
            
        }
    }

        public void deployFireball()
    {
      
        //Start posiiton for the particle effect to begin at
        Vector3 startPos = fireballEmitter.position;

        //Instantiate the particle projectile at the fire position location and set damage.
        GameObject projectile = Instantiate(fireball, startPos, Quaternion.identity) as GameObject;
        projectile.GetComponent<ProjectilesEnv>().projectileDamage = damage;
        playerPos.y = playerPos.y + 2;
        //Rotate projectile to look at attack point                        
        projectile.transform.LookAt(playerPos);
        //Add a rigid body and add force to move it towards the location
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 1000);
        //Impact Normal is a setting of the store asset and will determine the size of impact
        projectile.GetComponent<ProjectilesEnv>().impactNormal = playerRayHit.normal;
        //Destroy the particle after a certain amount of time
        //Make sure it doesn't go on forever if it never hits an object
        Destroy(projectile, 7f);

    }

}
