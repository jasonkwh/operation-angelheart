using UnityEngine;
using System.Collections;

public class UnlockLevel : MonoBehaviour {
	
	public GameObject level11;
	public GameObject level12;
	public GameObject level13;
	public GameObject level21;
	public GameObject level22;
	public GameObject level23;
	public GameObject level31;
	public GameObject level32;
	public GameObject level33;
	public GameObject farmTitle;
	public GameObject caveTitle;
	GameObject[] star0;
	GameObject[] star11;
	GameObject[] star12;
	GameObject[] star13;
	GameObject[] star21;
	GameObject[] star22;
	GameObject[] star23;
	GameObject[] star31;
	GameObject[] star32;
	GameObject[] star33;

	void Start() {
		level11.active = false;
		level12.active = false;
		level13.active = false;
		level21.active = false;
		level22.active = false;
		level23.active = false;
		level31.active = false;
		level32.active = false;
		level33.active = false;
		farmTitle.active = false;
		caveTitle.active = false;
	}

	public void reset (){
		PlayerPrefs.DeleteAll ();
		Start ();
	}


	void Update () {
		star0 = GameObject.FindGameObjectsWithTag("star0");
		star11 = GameObject.FindGameObjectsWithTag("star11");
		star12 = GameObject.FindGameObjectsWithTag("star12");
		star13 = GameObject.FindGameObjectsWithTag("star13");
		star21 = GameObject.FindGameObjectsWithTag("star21");
		star22 = GameObject.FindGameObjectsWithTag("star22");
		star23 = GameObject.FindGameObjectsWithTag("star23");
		star21 = GameObject.FindGameObjectsWithTag("star31");
		star22 = GameObject.FindGameObjectsWithTag("star32");
		star23 = GameObject.FindGameObjectsWithTag("star33");

		if(PlayerPrefs.GetInt("level") == 0) {
			PlayerPrefs.SetInt("playerBfs00",0);
			PlayerPrefs.SetInt("playerBfs11",0);
			PlayerPrefs.SetInt("playerBfs12",0);
			PlayerPrefs.SetInt("playerBfs13",0);
			PlayerPrefs.SetInt("playerBfs21",0);
			PlayerPrefs.SetInt("playerBfs22",0);
			PlayerPrefs.SetInt("playerBfs23",0);
			PlayerPrefs.SetInt("playerBfs31",0);
			PlayerPrefs.SetInt("playerBfs32",0);
			PlayerPrefs.SetInt("playerBfs33",0);
			disableButterflies(star0);
		}
		if(PlayerPrefs.GetInt("level") == 11) {
			level11.active = true;
			displayButterflies(star0,"playerBfs00");
			disableButterflies(star11);
		}
		if(PlayerPrefs.GetInt("level") == 12) {
			level11.active = true;
			level12.active = true;
			displayButterflies(star0,"playerBfs00");
			displayButterflies(star11,"playerBfs11");
			disableButterflies(star12);
		}
		if(PlayerPrefs.GetInt("level") == 13) {
			level11.active = true;
			level12.active = true;
			level13.active = true;
			displayButterflies(star0,"playerBfs00");
			displayButterflies(star11,"playerBfs11");
			displayButterflies(star12,"playerBfs12");
			disableButterflies(star13);
		}
		if(PlayerPrefs.GetInt("level") == 21) {
			level11.active = true;
			level12.active = true;
			level13.active = true;
			farmTitle.active = true;
			level21.active = true;
			displayButterflies(star0,"playerBfs00");
			displayButterflies(star11,"playerBfs11");
			displayButterflies(star12,"playerBfs12");
			displayButterflies(star13,"playerBfs13");
			disableButterflies(star21);
		}
		if(PlayerPrefs.GetInt("level") == 22) {
			level11.active = true;
			level12.active = true;
			level13.active = true;
			farmTitle.active = true;
			level21.active = true;
			level22.active = true;
			displayButterflies(star0,"playerBfs00");
			displayButterflies(star11,"playerBfs11");
			displayButterflies(star12,"playerBfs12");
			displayButterflies(star13,"playerBfs13");
			displayButterflies(star21,"playerBfs21");
			disableButterflies(star22);
		}
		if(PlayerPrefs.GetInt("level") == 23) {
			level11.active = true;
			level12.active = true;
			level13.active = true;
			farmTitle.active = true;
			level21.active = true;
			level22.active = true;
			level23.active = true;
			displayButterflies(star0,"playerBfs00");
			displayButterflies(star11,"playerBfs11");
			displayButterflies(star12,"playerBfs12");
			displayButterflies(star13,"playerBfs13");
			displayButterflies(star21,"playerBfs21");
			displayButterflies(star22,"playerBfs22");
			disableButterflies(star23);
		}
		if(PlayerPrefs.GetInt("level") == 31) {
			level11.active = true;
			level12.active = true;
			level13.active = true;
			farmTitle.active = true;
			level21.active = true;
			level22.active = true;
			level23.active = true;
			caveTitle.active = true;
			level31.active = true;
			displayButterflies(star0,"playerBfs00");
			displayButterflies(star11,"playerBfs11");
			displayButterflies(star12,"playerBfs12");
			displayButterflies(star13,"playerBfs13");
			displayButterflies(star21,"playerBfs21");
			displayButterflies(star22,"playerBfs22");
			displayButterflies(star23,"playerBfs23");
			disableButterflies(star31);
		}
		if(PlayerPrefs.GetInt("level") == 32) {
			level11.active = true;
			level12.active = true;
			level13.active = true;
			farmTitle.active = true;
			level21.active = true;
			level22.active = true;
			level23.active = true;
			caveTitle.active = true;
			level31.active = true;
			level32.active = true;
			displayButterflies(star0,"playerBfs00");
			displayButterflies(star11,"playerBfs11");
			displayButterflies(star12,"playerBfs12");
			displayButterflies(star13,"playerBfs13");
			displayButterflies(star21,"playerBfs21");
			displayButterflies(star22,"playerBfs22");
			displayButterflies(star23,"playerBfs23");
			displayButterflies(star31,"playerBfs31");
			disableButterflies(star32);
		}
		if(PlayerPrefs.GetInt("level") == 33) {
			level11.active = true;
			level12.active = true;
			level13.active = true;
			farmTitle.active = true;
			level21.active = true;
			level22.active = true;
			level23.active = true;
			caveTitle.active = true;
			level31.active = true;
			level32.active = true;
			level33.active = true;
			displayButterflies(star0,"playerBfs00");
			displayButterflies(star11,"playerBfs11");
			displayButterflies(star12,"playerBfs12");
			displayButterflies(star13,"playerBfs13");
			displayButterflies(star21,"playerBfs21");
			displayButterflies(star22,"playerBfs22");
			displayButterflies(star23,"playerBfs23");
			displayButterflies(star31,"playerBfs31");
			displayButterflies(star32,"playerBfs32");
			disableButterflies(star33);
		}
		if(PlayerPrefs.GetInt("level") == 41) {
			level11.active = true;
			level12.active = true;
			level13.active = true;
			farmTitle.active = true;
			level21.active = true;
			level22.active = true;
			level23.active = true;
			caveTitle.active = true;
			level31.active = true;
			level32.active = true;
			level33.active = true;
			displayButterflies(star0,"playerBfs00");
			displayButterflies(star11,"playerBfs11");
			displayButterflies(star12,"playerBfs12");
			displayButterflies(star13,"playerBfs13");
			displayButterflies(star21,"playerBfs21");
			displayButterflies(star22,"playerBfs22");
			displayButterflies(star23,"playerBfs23");
			displayButterflies(star31,"playerBfs31");
			displayButterflies(star32,"playerBfs32");
			displayButterflies(star33,"playerBfs33");
		}
	}

	void displayButterflies(GameObject[] stars, string playPrefsBfs) {
		if (PlayerPrefs.GetInt(playPrefsBfs) == 1) {
			stars[1].SetActive(false);
			stars[2].SetActive(false);
		} else if (PlayerPrefs.GetInt(playPrefsBfs) == 2) {
			stars[2].SetActive(false);
		}
	}

	void disableButterflies(GameObject[] stars) {
		foreach(GameObject star in stars) {
			star.SetActive(false);
		}
	}
}
