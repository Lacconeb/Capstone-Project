using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon_Lord_Attack : MonoBehaviour
{

    private GameObject skill;

    private GameObject firePosition;

    private int damage;


    public void SetUpSkill(GameObject newSkill, int dmg, GameObject pos)
    {
        skill = newSkill;

        damage = dmg;

        firePosition = pos;

        FireSkill();

    }

    private void FireSkill()
    {
        //Start posiiton for the particle effect to begin at
        Vector3 firePos = new Vector3(firePosition.transform.position.x, firePosition.transform.position.y, firePosition.transform.position.z);

        Vector3 hitPoint = firePos;
        hitPoint.x += 1f;
        
        InstantiateHelper(firePos, hitPoint);
        
        hitPoint = firePos;
        hitPoint.x -= 1f;

        InstantiateHelper(firePos, hitPoint);

        hitPoint = firePos;
        hitPoint.z += 1f;

        InstantiateHelper(firePos, hitPoint);

        hitPoint = firePos;
        hitPoint.z -= 1f;

        InstantiateHelper(firePos, hitPoint);
        /*
        hitPoint = firePos;
        hitPoint.x += 1f;
        hitPoint.z += 1f;

        InstantiateHelper(firePos, hitPoint);

        hitPoint = firePos;
        hitPoint.x -= 1f;
        hitPoint.z -= 1f;

        InstantiateHelper(firePos, hitPoint);

        hitPoint = firePos;
        hitPoint.x -= 1f;
        hitPoint.z += 1f;

        InstantiateHelper(firePos, hitPoint);

        hitPoint = firePos;
        hitPoint.x += 1f;
        hitPoint.z -= 1f;

        InstantiateHelper(firePos, hitPoint);
        */
    }

    public void InstantiateHelper(Vector3 firePosition, Vector3 attackPoint)
    {

        //Instantiate the particle projectile at the fire position location.
        GameObject projectile = Instantiate(skill, firePosition, Quaternion.identity) as GameObject;

        //Rotate projectile to look at attack point                        
        projectile.transform.LookAt(attackPoint);

        //Add a rigid body and add force to move it towards the location
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 1000);

        Monster_Projectiles projScr = projectile.GetComponent<Monster_Projectiles>();
        projScr.SetDamage(damage);

        Destroy(projectile, 4f);
    }
}
