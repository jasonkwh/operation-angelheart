using UnityEngine;
using System.Collections;

public class InstantTerrain : MonoBehaviour {

	//public GameObject pot;
	public GameObject terrain01;
	//private bool generated = false;

	void Start() {
		Instantiate (terrain01, new Vector3(transform.position.x,-0.12f,transform.position.z), terrain01.transform.rotation);
	}

	/*void Update () {
		if ((transform.position.x > terrain01.transform.position.x) && (GetComponent<Player>().generatedX1 == false)) {
			Instantiate (terrain01, new Vector3((terrain01.transform.position.x + terrain01.transform.localScale.x),-0.12f,transform.position.z), terrain01.transform.rotation);
			GetComponent<Player>().generatedX1 = true;
		}
	}*/
}
