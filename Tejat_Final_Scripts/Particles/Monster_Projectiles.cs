﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Projectiles : MonoBehaviour
{
    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject muzzleParticle;
    public GameObject[] trailParticles;
    [HideInInspector]
    public Vector3 impactNormal; //Used to rotate impactparticle.

    private bool hasCollided = false;

    //public Player playerScr;

    private int projectileDamage = 0;

    void Start()
    {
        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
        projectileParticle.transform.parent = transform;
        if (muzzleParticle)
        {
            muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
            Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
        }

        //playerScr = GameObject.FindWithTag("Player").GetComponent<Player>();
        //projectileDamage = playerScr.GetCurrentSkillDamage();

    }

    public void SetDamage(int dmg)
    {
        projectileDamage = dmg;
    }

    void OnCollisionEnter(Collision hit)
    {
        /*
        if (hit.gameObject.GetComponent<Monster>())
            return;
    */
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


            //If the object that the particle collided with 
            if (hit.gameObject.GetComponent<Player>())
            {
                //Debug.Log("HIT A MONSTER!!!!");

                Player scriptComp = hit.gameObject.GetComponent<Player>();
                scriptComp.GetHit(projectileDamage);
            }
            else
            {
                //Debug.Log("HIT OBJECT!!!!");
            }


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
