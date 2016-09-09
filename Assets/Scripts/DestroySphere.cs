using UnityEngine;
using System.Collections;

public class DestroySphere : MonoBehaviour {

	//public float delays;
	//public GameObject points;
	//private int score = 5;
	
	public GameObject smoke;
	public GameObject pot;

	protected void Start() {
		Physics.IgnoreLayerCollision (8,9); //ignore collision of enemies
	}

	protected virtual void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "potv2") {
			GetComponent<MeshRenderer> ().enabled = false;
			GetComponent<Rigidbody> ().useGravity = false;
			GetComponent<Collider> ().enabled = false;
			if (smoke.activeSelf == false) {
				pot.GetComponent<Player> ().ate = true;
			}
			Destroy (this.gameObject);
			//score = PlayerPrefs.GetInt ("SCORE") + score; //add points to original score
			//PlayerPrefs.SetInt ("SCORE", score); //store score value to system
			//points.GetComponent<GUIText>().text = "+ 5 pts.";
		}
	}
}
