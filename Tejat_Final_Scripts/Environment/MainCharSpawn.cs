using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharSpawn : MonoBehaviour
{

    public Character_Loader characterLoader;

    // Start is called before the first frame update
    void Awake()
    {
        //find character loader
        characterLoader = GameObject.FindWithTag("Character_Loader").GetComponent<Character_Loader>();

        //instatiate mainchar    
        Instantiate(characterLoader.Main_Char, transform.position, Quaternion.identity);

    }


}
