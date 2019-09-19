using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inspired by https://stackoverflow.com/questions/46662998/rotate-a-door-in-both-directions-in-unity

public class RotateDoor : MonoBehaviour
{

    [SerializeField]
    private Vector3 targetRotation; // rotation angles

    [SerializeField]
    private float duration; // rotation speed

    // [SerializeField]
    // private Vector3 pivotPosition; // Vector3 of the pivot

    [SerializeField]
    private GameObject pivotMarker;


    Transform doorPivot; // the pivot point to rotate around

    private void Start()
    {
        doorPivot = new GameObject().transform; // create pivot
        //doorPivot.position = pivotPosition; 
        doorPivot.position = pivotMarker.transform.position; // place the pivot before parenting!
        transform.SetParent(doorPivot); // make the door being a child of the pivot

    }

    private IEnumerator DoorRotation()
    {


        float counter = 0;
        //Vector3 openRotation = transform.eulerAngles + targetRotation;
        Vector3 openRotation = targetRotation;
        Vector3 defaultAngles = doorPivot.eulerAngles;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            LerpDoor(defaultAngles, openRotation, counter); // open the door
            yield return null;
        }


    }

    private void LerpDoor(Vector3 defaultAngles, Vector3 targetRotation, float counter)
    {
        doorPivot.eulerAngles = Vector3.Lerp(defaultAngles, targetRotation, counter / duration);
    }


    public void Interact() // start the rotation
    {
        StartCoroutine(DoorRotation());
    }
}
