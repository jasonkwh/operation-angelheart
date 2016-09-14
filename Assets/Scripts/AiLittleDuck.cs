using UnityEngine;
using System.Collections;

public class AiLittleDuck : AiDuck {

	protected override void FixedUpdate() {
		time += Time.deltaTime;
		duckRotate();

		if (dist < bounceRange) {
			//potTransform.position += transform.forward * moveSpeed * Time.deltaTime;
			potTransform.GetComponent<Player> ().pX = potTransform.position.x - transform.position.x;
			potTransform.GetComponent<Player> ().pZ = potTransform.position.z - transform.position.z;
			potTransform.GetComponent<Player> ().pushing = true;
			potTransform.GetComponent<Player> ().stopBackup = false;
			eBar.valueCurrent = eBar.valueCurrent - damage; //set energy bar value
			backupTime = time;
		}
	}

}
