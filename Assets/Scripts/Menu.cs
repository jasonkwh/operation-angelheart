using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
//script for ui behaviors

	public GameObject pause;
	public GameObject menu;

	void Start() {
		menu.active = false;
	}

	public void clickPause() {
		Time.timeScale = 0f;
		pause.active = false;
		menu.active = true;
	}

	public void clickResume() {
		Time.timeScale = 1f;
		menu.active = false;
		pause.active = true;
	}

	public void clickRestart() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("PotWalkv2", LoadSceneMode.Single);
	}
}
