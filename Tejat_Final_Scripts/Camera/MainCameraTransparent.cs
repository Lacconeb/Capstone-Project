using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraTransparent : MonoBehaviour
{
    //the object that comes between the player and camera
    public Transform Obstruction;
    public Transform PrevObst;
    public Transform CurrObst;

    //Main Character gameobject
    //Will be set from Character Loader
    private GameObject mainChar;
    private bool obstUnhidden = true;

    float camFOV = 30;

    //Variables to set distance rotation of camera from the Main Character's current position
    [SerializeField] float xDistance = 15;
    [SerializeField] float yDistance = 20;
    [SerializeField] float zDistance = -15;
    float xRotation = 45;
    float yRotation = -45;

    void Start()
    {
        Vector3 camRot = new Vector3(xRotation, yRotation, 0);
        transform.rotation = Quaternion.Euler(camRot);

        Camera.main.fieldOfView = camFOV;

        mainChar = GameObject.FindWithTag("Player");

        CurrObst = mainChar.transform;

        PrevObst = mainChar.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = new Vector3(mainChar.transform.position.x + xDistance,
                                mainChar.transform.position.y + yDistance,
                                mainChar.transform.position.z + zDistance);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, newPos, 0.1f);

        //transform.LookAt(mainChar.transform);

        ViewObstructed();
        
    }

    //obstruced view code courtesy of: https://www.youtube.com/watch?v=wWyx7_cIxP8
    //coroutine assistance from https://answers.unity.com/questions/897095/workaround-for-using-invoke-for-methods-with-param.html
    void ViewObstructed()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, mainChar.transform.position - transform.position, out hit))
        {

            Obstruction = hit.transform;

            //hide obstruction raycast hit if it's in between the player and camera
            if (hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "Monster" 
                && hit.collider.gameObject.tag != "NotObstacle")
            {
                

                //hide obstructing object if it has a mesh renderer
                if(Obstruction.gameObject.GetComponent<MeshRenderer>()){
                    Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                    //and then unhide obstructing object after a given period of time
                    StartCoroutine(UnHide(Obstruction, 1f));
                }
            }
        }
    }

    IEnumerator UnHide(Transform obstacle, float delay)
    {
        yield return new WaitForSeconds(delay);

        obstacle.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }



}







    // void ViewObstructed()
    // {
    //     RaycastHit hit;

    //     if (Physics.Raycast(transform.position, mainChar.transform.position - transform.position, out hit))
    //     {

    //         Obstruction = hit.transform;

    //         //hide obstruction raycast hit if it's in between the player and camera and not already 
    //         //marked as the current obstruction (in which case, it's already hidden)
    //         if (hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "Monster" 
    //             && hit.collider.gameObject.tag != "NotObstacle" && Obstruction != CurrObst)
    //         {
                
    //             PrevObst = CurrObst;
    //             //hide obstructing object
    //             if(Obstruction.gameObject.GetComponent<MeshRenderer>()){
    //                 Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
    //             }
    //             CurrObst = Obstruction;
                
    //         }
    //         //unhide "currObst" if it's no longer the actual obstruction after update
    //         else if (CurrObst && CurrObst != Obstruction && CurrObst.gameObject.tag != "Player" && CurrObst.gameObject.tag != "Monster")
    //         {   
    //             if(CurrObst.gameObject.GetComponent<MeshRenderer>()){
    //                 //CurrObst.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    //                 StartCoroutine(UnHide(CurrObst, 0.5f));
    //             }
    //         }
    //         //if "currObst" was not unhidden previously, unhide it now
    //         else if (PrevObst && PrevObst.gameObject.tag != "Player" && PrevObst.gameObject.tag != "Monster")
    //         {   
    //             if(PrevObst.gameObject.GetComponent<MeshRenderer>()){
    //                 //PrevObst.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    //                 StartCoroutine(UnHide(PrevObst, 0.5f));
    //             }
    //         }
    //     }
        
    // }