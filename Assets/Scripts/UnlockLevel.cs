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
		}
		if(PlayerPrefs.GetInt("level") == 11) {
			level11.active = true;
		}
		if(PlayerPrefs.GetInt("level") == 12) {
			level11.active = true;
			level12.active = true;
		}
		if(PlayerPrefs.GetInt("level") == 13) {
			level11.active = true;
			level12.active = true;
			level13.active = true;
		}
		if(PlayerPrefs.GetInt("level") == 21) {
			level11.active = true;
			level12.active = true;
			level13.active = true;
			farmTitle.active = true;
			level21.active = true;
		}
		if(PlayerPrefs.GetInt("level") == 22) {
			level11.active = true;
			level12.active = true;
			level13.active = true;
			farmTitle.active = true;
			level21.active = true;
			level22.active = true;
		}
		if(PlayerPrefs.GetInt("level") == 23) {
			level11.active = true;
			level12.active = true;
			level13.active = true;
			farmTitle.active = true;
			level21.active = true;
			level22.active = true;
			level23.active = true;
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
		}
	}
}
