using UnityEngine;
using System.Collections;

public class InstantTerrainNew : MonoBehaviour {

	private GameObject pot;
	public GameObject terrain01;
	private GameObject[] generated;

	void Start() {
		pot = GameObject.Find ("potv2");
	}

	void Update () {
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
	}
}
