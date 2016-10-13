using UnityEngine;
using System.Collections;

public class treeCollider : MonoBehaviour {

	private Transform potTransform;
	private float dist;
	public float bounceRange;

	void Start() {
		potTransform = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	void Update () {
		dist = Vector3.Distance (potTransform.position, transform.position);

		if (dist < bounceRange) {
			potTransform.GetComponent<Player> ().pX = potTransform.position.x - transform.position.x;
			potTransform.GetComponent<Player> ().pZ = potTransform.position.z - transform.position.z;
			potTransform.GetComponent<Player> ().pushing = true; //enable bounce animation
			potTransform.GetComponent<Player> ().stopBackup = false;
			potTransform.GetComponent<Player> ().bearCollider = true;
		}
	}
}
