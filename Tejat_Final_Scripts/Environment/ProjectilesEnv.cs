using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////////////////////////////////////////////////
/// Reference: https://assetstore.unity.com/packages/vfx/particles/spells/magic-arsenal-20869
//////////////////////////////////////////////////////////////////////////////////////////////////

public class ProjectilesEnv : MonoBehaviour
{
    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject muzzleParticle;
    public GameObject[] trailParticles;
    [HideInInspector]
    public Vector3 impactNormal; //Used to rotate impactparticle.
 
    private bool hasCollided = false;

    public Player playerScr;

    public int projectileDamage;
 
    void Start()
    {
        Debug.Log("ProjectilesEnv started");
        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
        projectileParticle.transform.parent = transform;
		if (muzzleParticle){
            muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
            Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
		}

        // playerScr = GameObject.FindWithTag("Player").GetComponent<Player>();
        // projectileDamage = playerScr.GetCurrentSkillDamage();
        // projectileDamage = 25;

    }
 
    void OnCollisionEnter(Collision hit)
    {
        if (!hasCollided)
        {
            hasCollided = true;
            //transform.DetachChildren();
            impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
            //Debug.DrawRay(hit.contacts[0].point, hit.contacts[0].normal * 1, Color.yellow);
 
            if (hit.gameObject.tag == "Destructible") // Projectile will destroy objects tagged as Destructible
            {
                Destroy(hit.gameObject);
            }


            //////////////////////////////////////////////////////////////////
            /// My Code
            //////////////////////////////////////////////////////////////////
            
            //If the object that the particle collided with 
            if (hit.gameObject.GetComponent<Player>())
            {
                Debug.Log("HIT THE PLAYER!!!!");

                Player scriptComp = hit.gameObject.GetComponent<Player>();
                scriptComp.GetHit(projectileDamage);
            }
            else
            {
                Debug.Log("HIT OBJECT!!!!");
            }

            //////////////////////////////////////////////////////////////////
            /// End My Code
            //////////////////////////////////////////////////////////////////

            //yield WaitForSeconds (0.05);
            foreach (GameObject trail in trailParticles)
            {
                GameObject curTrail = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
                curTrail.transform.parent = null;
                Destroy(curTrail, 3f);
            }
            Destroy(projectileParticle, 3f);
            Destroy(impactParticle, 5f);
            Destroy(gameObject);
            //projectileParticle.Stop();
			
			ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
            //Component at [0] is that of the parent i.e. this object (if there is any)
            for (int i = 1; i < trails.Length; i++)
            {
                ParticleSystem trail = trails[i];
                if (!trail.gameObject.name.Contains("Trail"))
                    continue;

                trail.transform.SetParent(null);
                Destroy(trail.gameObject, 2);
            }
        }
    }
}
