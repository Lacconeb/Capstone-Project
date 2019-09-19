using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    //Main Character gameobject
    //Will be set from Character Loader
    private GameObject mainChar;

    //Camera Field of View Variable
    private float camFOV = 30;

    //Variables to set distance and rotation of camera from the Main Character's current position
    private float xDistance = 10;
    private float yDistance = 15;
    private float zDistance = -10;
    private float xRotation = 45;
    private float yRotation = -45;

    void Start()
    {
        //set camera rotation to a fixed positon
        Vector3 camRot = new Vector3(xRotation, yRotation, 0);
        transform.rotation = Quaternion.Euler(camRot);

        //Set camera field of view
        Camera.main.fieldOfView = camFOV;

        //Find and set the main character in the scene
        mainChar = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        //Find the location of main character
        Vector3 newPos = new Vector3(mainChar.transform.position.x + xDistance, 
                                    mainChar.transform.position.y + yDistance, 
                                    mainChar.transform.position.z + zDistance);

        //Move the camera to follow the main character
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, newPos, 0.1f);

        
    }
}
