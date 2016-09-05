using UnityEngine;
using System.Collections;

public class InstantTerrainNew : MonoBehaviour {

	private GameObject pot;

	//variables for x axis
	public GameObject terrain01;
	private GameObject[] generated;

	//variables for z axis
	//public GameObject terrain01_z;
	//private GameObject[] generated2;

	void Start() {
		pot = GameObject.Find ("potv2");
	}

	void Update () {
		//horizontal
		generated = GameObject.FindGameObjectsWithTag ("Terrain");
		//if ((pot.transform.position.x > transform.position.x) && (pot.GetComponent<Player>().generatedX1 == false) && (pot.GetComponent<Player>().generatedX2key == false) && (pot.GetComponent<Player>().generatedX1key == true)) {
		if ((pot.transform.position.x > transform.position.x) && (pot.GetComponent<Player>().generatedX1 == false) && (generated.Length == 1)) {
			Instantiate (terrain01, new Vector3 ((transform.position.x + transform.localScale.x), -0.12f, transform.position.z), transform.rotation);
			pot.GetComponent<Player>().generatedX1 = true;
		}
		if (pot.transform.position.x > (transform.position.x + transform.localScale.x)) {
			pot.GetComponent<Player> ().generatedX1 = false;
			Destroy (this.gameObject);
		}
		//if ((pot.transform.position.x < transform.position.x) && (pot.GetComponent<Player> ().generatedX2 == false) && (pot.GetComponent<Player>().generatedX1key == false) && (pot.GetComponent<Player>().generatedX2key == true)) {
		if ((pot.transform.position.x < transform.position.x) && (pot.GetComponent<Player> ().generatedX2 == false) && (generated.Length == 1)) {
			Instantiate (terrain01, new Vector3 ((transform.position.x - transform.localScale.x), -0.12f, transform.position.z), transform.rotation);
			pot.GetComponent<Player> ().generatedX2 = true;
		}
		if (pot.transform.position.x < (transform.position.x - transform.localScale.x)) {
			pot.GetComponent<Player> ().generatedX2 = false;
			Destroy (this.gameObject);
		}

		//vertical
		/*generated2 = GameObject.FindGameObjectsWithTag ("TerrainZ");
		if (pot.transform.position.z > transform.position.z) {
			Instantiate (terrain01_z, new Vector3 (pot.transform.position.x, -0.12f, transform.position.z + transform.localScale.z), transform.rotation);
			pot.GetComponent<Player> ().generatedZ1 = true;
		}
		if (pot.transform.position.z > (transform.position.z + transform.localScale.z)) {
			pot.GetComponent<Player> ().generatedZ1 = false;
			pot.GetComponent<Player> ().generatedX1 = false;
			pot.GetComponent<Player> ().generatedX2 = false;
			Destroy (this.gameObject);
		}*/
	}
}
