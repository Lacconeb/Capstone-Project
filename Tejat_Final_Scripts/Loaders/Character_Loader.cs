using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Loader : MonoBehaviour
{
    public static Character_Loader control;

    public string Char_Name;

    public GameObject Main_Char;

    public GameObject Wizard;
    public GameObject Warrior;
    public GameObject Archer;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            control = this;
        }
    }

    public void SetMainCharacter(int selection)
    {

        if(selection == 1)
        {
            Main_Char = Wizard;
        }
        else if(selection == 2)
        {
            Main_Char = Warrior;
        }
        else if(selection == 3)
        {
            Main_Char = Archer;
        }
        else
        {
            Debug.Log("Error! Main Character was not set properly from Character_Loader");
        }

    }

}
