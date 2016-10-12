using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
//script for ui behaviors

	public GameObject pause;
	public GameObject menu;
	public GameObject controls;
    public GameObject List;


    void Start() {
		menu.active = false;
        List.active = false;
	}

	public void clickPause() {
		Time.timeScale = 0f;
		pause.active = false;
		controls.active = false;
		menu.active = true;
        List.active = true;

    }

	public void clickResume() {
		Time.timeScale = 1f;
		menu.active = false;
		pause.active = true;
		controls.active = true;
        List.active = false;
    }

	public void clickRestart() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("PotWalkv2", LoadSceneMode.Single);
	}
}
