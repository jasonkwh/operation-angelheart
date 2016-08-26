using UnityEngine;
using System.Collections;

public class DestroySphere : MonoBehaviour {

	//public float delays;
	//public GameObject points;
	//private int score = 5;

	public GameObject smoke;
	public GameObject pot;
	private Player playScript;

	void Start() {
		playScript = pot.GetComponent<Player> ();
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "potv2") {
			GetComponent<MeshRenderer> ().enabled = false;
			GetComponent<Rigidbody> ().useGravity = false;
			GetComponent<Collider> ().enabled = false;
			//score = PlayerPrefs.GetInt ("SCORE") + score; //add points to original score
			//PlayerPrefs.SetInt ("SCORE", score); //store score value to system
			if (smoke.activeSelf == false) {
				playScript.ate = true;
			}
			Destroy (this.gameObject);
			//points.GetComponent<GUIText>().text = "+ 5 pts.";
		}
	}
}
