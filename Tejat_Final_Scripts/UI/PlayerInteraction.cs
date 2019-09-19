using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Character_Loader charLoader;
    public Player playerObject; 
    UIManager uiManager;   
    public GameObject wizardActionBar, archerActionBar, warriorActionBar;    
    GameObject Wizard, Archer, Warrior;    
    public Slider healthSlider, manaSlider;
    public Image healthGlobe, manaGlobe;
    public InputField charName;
    public GameObject wizardHealDisabled, archerHealDisabled, warriorHealDisabled, speedBuff, damageBuff;
    GameObject wizardSkill2Disabled, wizardSkill3Disabled, wizardSkill4Disabled, archerSkill2Disabled, archerSkill3Disabled, archerSkill4Disabled, warriorSkill2Disabled, warriorSkill3Disabled, warriorSkill4Disabled;    
    
    void Start() {
        charLoader = GameObject.FindGameObjectWithTag("Character_Loader").GetComponent<Character_Loader>();
        playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        wizardActionBar = GameObject.FindGameObjectWithTag("Wizard_ActionBar");
        archerActionBar = GameObject.FindGameObjectWithTag("Archer_ActionBar");
        warriorActionBar = GameObject.FindGameObjectWithTag("Warrior_ActionBar");
        wizardSkill2Disabled = GameObject.FindGameObjectWithTag("Wizard_Skill2_Disabled");
        wizardSkill3Disabled = GameObject.FindGameObjectWithTag("Wizard_Skill3_Disabled");
        wizardSkill4Disabled = GameObject.FindGameObjectWithTag("Wizard_Skill4_Disabled");
        archerSkill2Disabled = GameObject.FindGameObjectWithTag("Archer_Skill2_Disabled");
        archerSkill3Disabled = GameObject.FindGameObjectWithTag("Archer_Skill3_Disabled");
        archerSkill4Disabled = GameObject.FindGameObjectWithTag("Archer_Skill4_Disabled");
        warriorSkill2Disabled = GameObject.FindGameObjectWithTag("Warrior_Skill2_Disabled");
        warriorSkill3Disabled = GameObject.FindGameObjectWithTag("Warrior_Skill3_Disabled");
        warriorSkill4Disabled = GameObject.FindGameObjectWithTag("Warrior_Skill4_Disabled");
        wizardHealDisabled = GameObject.FindGameObjectWithTag("Wizard_Heal_Disabled");
        archerHealDisabled = GameObject.FindGameObjectWithTag("Archer_Heal_Disabled");
        warriorHealDisabled = GameObject.FindGameObjectWithTag("Warrior_Heal_Disabled");
        healthSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        manaSlider = GameObject.FindGameObjectWithTag("ManaSlider").GetComponent<Slider>();
        healthGlobe = GameObject.FindGameObjectWithTag("HealthGlobe").GetComponent<Image>();
        manaGlobe = GameObject.FindGameObjectWithTag("ManaGlobe").GetComponent<Image>();
        charName = GameObject.FindGameObjectWithTag("CharName").GetComponent<InputField>();
        speedBuff = GameObject.FindGameObjectWithTag("Speed_Buff");
        damageBuff = GameObject.FindGameObjectWithTag("Dmg_Buff");

        charName.text = SelectedCharacter.Name;        

        if (SelectedCharacter.Character == 1) {
            wizardActionBar.SetActive(true);
            archerActionBar.SetActive(false);
            warriorActionBar.SetActive(false);
            
        } else if (SelectedCharacter.Character == 2) {
            warriorActionBar.SetActive(true);
            wizardActionBar.SetActive(false);
            archerActionBar.SetActive(false);
           
        } else if (SelectedCharacter.Character == 3) {            
            archerActionBar.SetActive(true);
            warriorActionBar.SetActive(false);
            wizardActionBar.SetActive(false);
        }

        buffVisible("damage", false);
        buffVisible("speed", false);
        showHealCooldown(false);
    }

    void Update() {        
        healthSlider.value = playerObject.GetHealth();
        manaSlider.value = playerObject.GetMana();
        healthGlobe.fillAmount = ((float)playerObject.GetHealth()) / 100;
        manaGlobe.fillAmount = ((float)playerObject.GetMana()) / 100;

        if (playerObject.isPlayerDead()) {
            uiManager.showDeath();
        } else {
            uiManager.hideDeath();
        }

    }

    public void buffVisible(string buffType, bool isActive) {
        if (buffType == "damage") {
            if (isActive) {
                damageBuff.SetActive(true);
            } else {
                damageBuff.SetActive(false);
            }
        }
        if (buffType == "speed") {
            if (isActive) {
                speedBuff.SetActive(true);
            } else {
                speedBuff.SetActive(false);
            }
        }
    }

    // this is controlling the overlay that grays out the skill icon if mana is too low to use a skill
    public void showSkillLowMana(string skillName, bool isDisabled) {
        switch(SelectedCharacter.Character) {
            case 1: {
                if (skillName == "skill2") {
                    if (isDisabled) {
                        wizardSkill2Disabled.SetActive(true);
                    } else {
                        wizardSkill2Disabled.SetActive(false);
                    }
                }
                if (skillName == "skill3") {
                    if (isDisabled) {            
                        wizardSkill3Disabled.SetActive(true);
                    } else {
                        wizardSkill3Disabled.SetActive(false);
                    }
                }
                if (skillName == "skill4") {
                    if (isDisabled) {            
                        wizardSkill4Disabled.SetActive(true);
                    } else {
                        wizardSkill4Disabled.SetActive(false);
                    }
                }
                break;
            }
            case 2: {
                if (skillName == "skill2") {
                    if (isDisabled) {
                        warriorSkill2Disabled.SetActive(true);
                    } else {
                        warriorSkill2Disabled.SetActive(false);
                    }
                }
                if (skillName == "skill3") {
                    if (isDisabled) {            
                        warriorSkill3Disabled.SetActive(true);
                    } else {
                        warriorSkill3Disabled.SetActive(false);
                    }
                }
                if (skillName == "skill4") {
                    if (isDisabled) {            
                        warriorSkill4Disabled.SetActive(true);
                    } else {
                        warriorSkill4Disabled.SetActive(false);
                    }
                }
                break;
            }
            case 3: {
                if (skillName == "skill2") {
                    if (isDisabled) {
                        archerSkill2Disabled.SetActive(true);
                    } else {
                        archerSkill2Disabled.SetActive(false);
                    }
                }
                if (skillName == "skill3") {
                    if (isDisabled) {            
                        archerSkill3Disabled.SetActive(true);
                    } else {
                        archerSkill3Disabled.SetActive(false);
                    }
                }
                if (skillName == "skill4") {
                    if (isDisabled) {            
                        archerSkill4Disabled.SetActive(true);
                    } else {
                        archerSkill4Disabled.SetActive(false);
                    }
                }
                break;
            }
        }
    }

    public void showHealCooldown(bool isOnCooldown) {
        switch(SelectedCharacter.Character) {
            case 1: {
                if (isOnCooldown) {
                    wizardHealDisabled.SetActive(true);
                } else {
                    wizardHealDisabled.SetActive(false);
                }
                break;
            }
            case 2: {
                if (isOnCooldown) {
                    warriorHealDisabled.SetActive(true);
                } else {
                    warriorHealDisabled.SetActive(false);
                }
                break;
            }
            case 3: {
                if (isOnCooldown) {
                    archerHealDisabled.SetActive(true);
                } else {
                    archerHealDisabled.SetActive(false);
                }
                break;
            }
        }
    }
}
