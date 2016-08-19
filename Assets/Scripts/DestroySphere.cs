using UnityEngine;
using System.Collections;

public class DestroySphere : MonoBehaviour {

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "Pots") {
			Destroy (this.gameObject);
		}
	}
}
