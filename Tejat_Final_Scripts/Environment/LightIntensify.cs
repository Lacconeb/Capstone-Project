using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inspired by https://stackoverflow.com/questions/46662998/rotate-a-door-in-both-directions-in-unity
//and https://answers.unity.com/questions/1007121/changing-light-intensity-over-time-via-mathflerp.html

public class LightIntensify : MonoBehaviour
{

    private  Light lt; // this game object's light

    [SerializeField]
    private float startIntensity; 

    [SerializeField]
    private float endIntensity; 

    [SerializeField]
    private float duration; // speed of intensification


    private void Start()
    {
        lt = GetComponent<Light>();

    }

    private IEnumerator ChangeIntensity()
    {


        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            LerpLight(startIntensity, endIntensity, counter); // open the door
            yield return null;
        }


    }

    private void LerpLight(float startIntensity, float endIntensity, float counter)
    {
        Debug.Log("lerping light");
        lt.intensity = Mathf.Lerp(startIntensity, endIntensity, counter / duration);
    }


    public void Interact() // start the rotation
    {
        StartCoroutine(ChangeIntensity());
    }
}
