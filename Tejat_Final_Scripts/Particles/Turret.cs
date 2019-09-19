using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private bool readyToFire;

    private GameObject skill;

    public GameObject firePosition;

    private int damage;

    private float cooldownLength = .5f;
    private float cooldownTimeLeft;


    private void Awake()
    {
        readyToFire = false;
        cooldownTimeLeft = Time.time + cooldownLength;
    }

    // Update is called once per frame
    void Update()
    {

        if (readyToFire == false)
            return;

        if (Time.time > cooldownTimeLeft)
        {
            FireSkill();
        }
    }


    public void SetUpSkill(GameObject newSkill, int dmg)
    {
        skill = newSkill;

        damage = dmg;

        readyToFire = true;
    }


    private void FireSkill()
    {
        //Start posiiton for the particle effect to begin at
        Vector3 firePos = new Vector3(firePosition.transform.position.x, firePosition.transform.position.y, firePosition.transform.position.z);

        Vector3 hitPoint = firePos;
        hitPoint.x += 1f;

        ArrowInstantiateHelper(firePos, hitPoint);

        hitPoint = firePos;
        hitPoint.x -= 1f;

        ArrowInstantiateHelper(firePos, hitPoint);

        hitPoint = firePos;
        hitPoint.z += 1f;

        ArrowInstantiateHelper(firePos, hitPoint);

        hitPoint = firePos;
        hitPoint.z -= 1f;

        ArrowInstantiateHelper(firePos, hitPoint);

        hitPoint = firePos;
        hitPoint.x += 1f;
        hitPoint.z += 1f;

        ArrowInstantiateHelper(firePos, hitPoint);

        hitPoint = firePos;
        hitPoint.x -= 1f;
        hitPoint.z -= 1f;

        ArrowInstantiateHelper(firePos, hitPoint);

        hitPoint = firePos;
        hitPoint.x -= 1f;
        hitPoint.z += 1f;

        ArrowInstantiateHelper(firePos, hitPoint);

        hitPoint = firePos;
        hitPoint.x += 1f;
        hitPoint.z -= 1f;

        ArrowInstantiateHelper(firePos, hitPoint);

        cooldownTimeLeft = Time.time + cooldownLength;
    }

    public void ArrowInstantiateHelper(Vector3 firePosition, Vector3 attackPoint)
    {

        //Instantiate the particle projectile at the fire position location.
        GameObject projectile = Instantiate(skill, firePosition, Quaternion.identity) as GameObject;

        //Rotate projectile to look at attack point                        
        projectile.transform.LookAt(attackPoint);

        //Add a rigid body and add force to move it towards the location
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 1000);

        //Set new projectile rotation because rotation is off
        float newNum = projectile.transform.eulerAngles.y + 92f;
        projectile.transform.eulerAngles = new Vector3(projectile.transform.eulerAngles.x, newNum, projectile.transform.eulerAngles.z);

        Player_Arrow_Collision arrowScr = projectile.GetComponent<Player_Arrow_Collision>();
        arrowScr.SetAttack(damage);

        Destroy(projectile, 4f);
    }
}
