using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterVisibility : MonoBehaviour
{
    public Toggle char1Select;
    public Toggle char2Select;
    public Toggle char3Select;
    public GameObject UI_Wizard;
    public GameObject UI_Archer;
    public GameObject UI_Warrior;
    public Character_Loader charLoader;
    public InputField enteredCharName;
    public string playerName;
    // Start is called before the first frame update
    void Start() {        
        char1Select = GameObject.FindGameObjectWithTag("Char1Toggle").GetComponent<Toggle>();
        char2Select = GameObject.FindGameObjectWithTag("Char2Toggle").GetComponent<Toggle>();
        char3Select = GameObject.FindGameObjectWithTag("Char3Toggle").GetComponent<Toggle>();
        UI_Wizard = GameObject.FindGameObjectWithTag("UI_Wizard");
        UI_Archer = GameObject.FindGameObjectWithTag("UI_Archer");
        UI_Warrior = GameObject.FindGameObjectWithTag("UI_Warrior");
        charLoader = GameObject.FindGameObjectWithTag("Character_Loader").GetComponent<Character_Loader>();
        enteredCharName = GameObject.FindGameObjectWithTag("CharNameText").GetComponent<InputField>();
        playerName = "";                        
    }

    // Update is called once per frame
    void Update() {
        if (char1Select.isOn) {            
            UI_Wizard.SetActive(true);
            UI_Archer.SetActive(false);
            UI_Warrior.SetActive(false);
            charLoader.SetMainCharacter(1);
            SelectedCharacter.Character = 1;
        } else {
            UI_Wizard.SetActive(false);
        }
        if (char2Select.isOn) {
            UI_Archer.SetActive(true);
            UI_Wizard.SetActive(false);
            UI_Warrior.SetActive(false);
            charLoader.SetMainCharacter(3);
            SelectedCharacter.Character = 3;
        } else {
            UI_Archer.SetActive(false);
        }
        if (char3Select.isOn) {
            UI_Warrior.SetActive(true);
            UI_Archer.SetActive(false);
            UI_Wizard.SetActive(false);
            charLoader.SetMainCharacter(2);
            SelectedCharacter.Character = 2;
        } else {
            UI_Warrior.SetActive(false);
        }
    }

    public void setCharName() {
        if (string.IsNullOrEmpty(enteredCharName.text)) {
            switch(SelectedCharacter.Character) {
                case 1: {
                    playerName = "Wizard";
                    break;
                }
                case 2: {
                    playerName = "Warrior";
                    break;
                }
                case 3: {
                    playerName = "Archer";
                    break;
                }
                default: {
                    playerName = "Player";
                    break;
                }
            }
        } else {
            playerName = enteredCharName.text.ToString();
        }
        SelectedCharacter.Name = playerName;
    }
}
