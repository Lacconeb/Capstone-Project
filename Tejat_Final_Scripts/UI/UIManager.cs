using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {   

    GameObject[] pauseObjects;
	GameObject[] deathObjects;

	// Use this for initialization
	void Start() {
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		deathObjects = GameObject.FindGameObjectsWithTag("ShowOnDeath");
		hidePaused();
		hideDeath();
	}

	// Update is called once per frame
	void Update(){
		//uses the Esc key to pause and unpause the game
		if(Input.GetKeyDown(KeyCode.Escape)) {
			if(Time.timeScale == 1) {
				Time.timeScale = 0;
				showPaused();
			} else if (Time.timeScale == 0) {				
				Time.timeScale = 1;
				hidePaused();
			}
		}
	}

	//Reloads the Level
	public void Reload() {
		Application.LoadLevel(Application.loadedLevel);
	}

	//controls the pausing of the scene
	public void pauseControl() {
			if(Time.timeScale == 1) {
				Time.timeScale = 0;
				showPaused();
			} else if (Time.timeScale == 0) {
				Time.timeScale = 1;
				hidePaused();
			}
	}

	//shows objects with ShowOnPause tag
	public void showPaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
	}

	//shows objects with ShowOnDeath tag
	public void showDeath(){
		foreach(GameObject g in deathObjects){
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnPause tag
	public void hidePaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
	}

	//hides objects with ShowOnDeath tag
	public void hideDeath(){
		foreach(GameObject g in deathObjects){
			g.SetActive(false);
		}
	}

     public void LoadScene(string sceneName) {        
        SceneManager.LoadScene(sceneName);
    }
}
