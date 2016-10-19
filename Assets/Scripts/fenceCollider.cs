using UnityEngine;
using System.Collections;

public class fenceCollider : MonoBehaviour {
	
	private Vector3 tempPosition;
	private Quaternion tempRotation;

	void Start() {
		tempPosition = transform.position;
		tempRotation = transform.rotation;
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player") {
			transform.position = tempPosition;
			transform.rotation = tempRotation;
		}
	}
}
