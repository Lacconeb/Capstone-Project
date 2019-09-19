using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    //select
    public string buffType;
    public float buffAmount;
    public float buffUpTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            //get player script
            Player scr = col.GetComponent<Player>();

            //call relevant player method with buff type
            //string buffType = "speed";
            scr.BuffPlayer(buffType, buffAmount, buffUpTime);

            //destroy game object
            Destroy(gameObject);


        }
    }
}
