using UnityEngine;
using System.Collections;

public class AiCatNew : AiDuck {
	protected override void FixedUpdate() {
		time += Time.deltaTime;
		dist = Vector3.Distance (potTransform.position, transform.position);

		if ((dist < maxDist) && (foundPot == false)) {
			foundPot = true;
		}
		if(foundPot == true) {
			duckRotate();

			if (potTransform.GetComponent<Player> ().pushing == false) {
				if (stopRanTime == false) {
						backupTime = time;
						randomTime = ranTime (minRandomTime, maxRandomTime);
						stayTime = ranTime (minStayTime, maxStayTime);
						stopRanTime = true;
				}

				if (time < (backupTime + randomTime)) {
					//anim.SetInteger("DucklingState", 1); //run
					speedAcceleration ();
				} else if ((time > (backupTime + randomTime)) && (time < (backupTime + randomTime + stayTime))) {
					//anim.SetInteger("DucklingState", 2); //roar
				} else if (time > (backupTime + randomTime + stayTime)) {
					moveSpeed = 1.0f;
					stopRanTime = false;
				}
			}
		}

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
