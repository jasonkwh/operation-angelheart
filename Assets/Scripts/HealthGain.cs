using UnityEngine;
using System.Collections;

public class HealthGain : DestroySphere {

	protected override void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "potv2") {
			GetComponent<MeshRenderer> ().enabled = false;
			GetComponent<Rigidbody> ().useGravity = false;
			GetComponent<Collider> ().enabled = false;
			pot.GetComponent<Player>().energyGain = true;
			if (smoke.activeSelf == false) {
				pot.GetComponent<Player> ().ate = true;
			}
			Destroy (this.gameObject);
		}
	}
}
