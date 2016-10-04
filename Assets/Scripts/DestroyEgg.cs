using UnityEngine;
using System.Collections;

public class DestroyEgg : DestroySphere {

	protected override void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player") {
			GetComponent<MeshRenderer> ().enabled = false;
			GetComponent<Rigidbody> ().useGravity = false;
			GetComponent<Collider> ().enabled = false;
			if (smoke.activeSelf == false) {
				pot.GetComponent<Player> ().ate = true;
			}
			Destroy(transform.parent.gameObject); //delete nest
			Destroy (this.gameObject);
		}
	}
}
