//inspiration for this script from https://www.youtube.com/watch?v=xHjwGJIbW60

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorTrigger : MonoBehaviour
{
    public GameObject door;
    public GameObject lt;
    private RotateDoor doorScript;
    private LightIntensify ltScript;

    //add keyboard input string to set open command; otherwise leave blank for
    //proximity trigger open
     [SerializeField]
     private string openCommand;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("enemy spawn script started");
        doorScript = door.GetComponent<RotateDoor>();
        ltScript = lt.GetComponent<LightIntensify>();
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //openCommand key is specified and entered or no command is specified; 
             if((openCommand != "" && Input.GetKey(openCommand)) || openCommand == ""){
                     doorScript.Interact(); //open door on RotateDoor script
                     gameObject.GetComponent<Collider>().enabled = false; //disable triger 
                     ltScript.Interact();  
                     
             }

        }
    }

}
