//inspiration for this script from https://www.youtube.com/watch?v=xHjwGJIbW60

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinSceneTrigger : MonoBehaviour
{

    //name of next scene/map to load when player enters trigger area
     [SerializeField]
     private string sceneName;
     private Character_Loader characterLoader;
     private GameObject mainChar;
     public GameObject boss;
     private Monster bossScript;

    // Start is called before the first frame update
    void Start()
    {
        //find character loader in current scene and get Main_Char
        // characterLoader = GameObject.FindWithTag("Character_Loader").GetComponent<Character_Loader>();
        // mainChar = characterLoader.Main_Char;
        bossScript = boss.GetComponent<Monster>();
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && bossScript.isDead == true)
        {
           //load new scene
           SceneManager.LoadScene(sceneName);
           //get character loader in new scene and set Main_Char
           
             
        }
    }

}
