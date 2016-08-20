using UnityEngine;
using System.Collections;

public class DestroySphere : MonoBehaviour {

	public float delays;
	public GameObject points;
	private int score = 5;

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "Pots") {
			GetComponent<MeshRenderer> ().enabled = false;
			GetComponent<Rigidbody> ().useGravity = false;
			GetComponent<Collider> ().enabled = false;
			score = PlayerPrefs.GetInt ("SCORE") + score; //add points to original score
			PlayerPrefs.SetInt ("SCORE", score); //store score value to system
			Destroy (this.gameObject, delays);
			points.GetComponent<GUIText>().text = "+ 5 pts.";
		}
	}
}
