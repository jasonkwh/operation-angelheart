﻿using UnityEngine;
using System.Collections;

public class AiBear : AiDuck {

	protected override void FixedUpdate() {
		time += Time.deltaTime;
		dist = Vector3.Distance (potTransform.position, transform.position);

		if(dist < maxDist) {
			duckRotate();

			if (potTransform.GetComponent<Player> ().pushing == false) {
				if (stopRanTime == false) {
						backupTime = time;
						randomTime = ranTime (minRandomTime, maxRandomTime);
						stopRanTime = true;
				}
				if (time < (backupTime + randomTime)) {
					anim.SetInteger("BearState", 1); //walk
					speedAcceleration ();
				} else if (time > (backupTime + randomTime)) {
					moveSpeed = 1.0f;
					stopRanTime = false;
				}
			}
		} else {
			anim.SetInteger("BearState", 0); //stay
		}

		if (dist < bounceRange) {
			anim.SetInteger("BearState", 2); //attack
			potTransform.GetComponent<Player> ().pX = potTransform.position.x - transform.position.x;
			potTransform.GetComponent<Player> ().pZ = potTransform.position.z - transform.position.z;
			potTransform.GetComponent<Player> ().pushing = true;
			potTransform.GetComponent<Player> ().stopBackup = false;
			eBar.valueCurrent = eBar.valueCurrent - damage; //set energy bar value
			backupTime = time;
		}
	}
}