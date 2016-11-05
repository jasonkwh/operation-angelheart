using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class achievement : MonoBehaviour {

	public GameObject achievement1;
	public GameObject achievement2;
	public GameObject achievement3;
	public GameObject achievement4;

	// Use this for initialization
	void Start () {

	}

	public void reset(){
		achievement1.GetComponent<Toggle>().isOn = false;
		achievement2.GetComponent<Toggle>().isOn = false;
		achievement3.GetComponent<Toggle>().isOn = false;
		achievement4.GetComponent<Toggle>().isOn = false;
	}
		
	
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetInt("level") > 13) { //complete all forest levels
			achievement1.GetComponent<Toggle>().isOn = true;
		} else if(PlayerPrefs.GetInt("level") > 23) { //complete all farm levels
			achievement2.GetComponent<Toggle>().isOn = true;
		} else if(PlayerPrefs.GetInt("level") > 33) { //complete the whole game
			achievement3.GetComponent<Toggle>().isOn = true;
		}
		if(PlayerPrefs.GetInt("playerBfs00") + PlayerPrefs.GetInt("playerBfs11") + PlayerPrefs.GetInt("playerBfs12") + PlayerPrefs.GetInt("playerBfs13") + PlayerPrefs.GetInt("playerBfs21") + PlayerPrefs.GetInt("playerBfs22") + PlayerPrefs.GetInt("playerBfs23") + PlayerPrefs.GetInt("playerBfs31") + PlayerPrefs.GetInt("playerBfs32") + PlayerPrefs.GetInt("playerBfs33") == 30) {
			achievement4.GetComponent<Toggle>().isOn = true;
		}
	}
}
