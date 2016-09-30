using UnityEngine;
using System.Collections;

public class AiFlyDuck : AiDuck {

	public float maxRandomTimeFly;
	public float minRandomTimeFly;
	private bool fly = false;
	public float flySpeed;
	public float flyTowardsSpeed;
	public float landForward = 12f;
	public float landTransY = 8f;
	private InstantFlyDuck potInstant;
	AudioSource audio;

	protected override void Start() {
		potTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		potInstant = GameObject.FindGameObjectWithTag ("Player").GetComponent<InstantFlyDuck>();
		bounceTime = potTransform.GetComponent<Player> ().stayTime;
		eBar = GameObject.FindGameObjectWithTag("energyBar").GetComponent<EnergyBar>();
		anim = gameObject.GetComponent<Animator>();
		audio = gameObject.GetComponent<AudioSource>();
	}

	protected override void FixedUpdate() {
		time += Time.deltaTime;
		dist = Vector3.Distance (potTransform.position, transform.position);

		//fly animations
		if(dist < maxDist) {
			if(fly == false) {
				backupTime = time;
				randomTime = ranTime (minRandomTimeFly, maxRandomTimeFly);
				fly = true;
			}
			if((time < (backupTime + randomTime)) && (foundPot == false) && (fly == true)) {
				flying();
			} else if ((time > (backupTime + randomTime)) && (foundPot == false) && (fly == true)) {
				foundPot = true;
			}
		} else {
			duckRotate();
			transform.position += transform.forward * flyTowardsSpeed * Time.deltaTime;
		}

		if(foundPot == true) {
			if(finishStandBackup == false) {
				backupTimeStand = time;
				finishStandBackup = true;
			}

			if(finishStand == false) {
				if(time < (backupTimeStand + standStillTime)) {
					anim.SetInteger("DucklingState", 2); //land
					duckRotate ();
					landing();
				} else if ((time > (backupTimeStand + standStillTime)) && (time < (backupTimeStand + standStillTime + standRoarTime))) {
					transform.position = new Vector3(transform.position.x, 0, transform.position.z);
					anim.SetInteger("DucklingState", 4); //roar
					if(audio.isPlaying == false) {
						audio.PlayDelayed(0.35f);
					}
				} else if (time > (backupTimeStand + standStillTime + standRoarTime)) {
					finishStand = true;
				}
			} else {
				duckRotate ();

				if (potTransform.GetComponent<Player> ().pushing == false) {
					if (stopRanTime == false) {
						backupTime = time;
						randomTime = ranTime (minRandomTime, maxRandomTime);
						stayTime = ranTime (minStayTime, maxStayTime);
						stopRanTime = true;
					}

					if (time < (backupTime + randomTime)) {
						anim.SetInteger("DucklingState", 3); //run
						speedAcceleration ();
					} else if ((time > (backupTime + randomTime)) && (time < (backupTime + randomTime + stayTime))) {
						anim.SetInteger("DucklingState", 4); //roar
						if(audio.isPlaying == false) {
							audio.PlayDelayed(0.35f);
						}
					} else if (time > (backupTime + randomTime + stayTime)) {
						moveSpeed = 1.0f;
						stopRanTime = false;
					}
				}
			}
		}

		if (dist < bounceRange) {
			potTransform.GetComponent<Player> ().pX = potTransform.position.x - transform.position.x;
			potTransform.GetComponent<Player> ().pZ = potTransform.position.z - transform.position.z;
			potTransform.GetComponent<Player> ().pushing = true;
			potTransform.GetComponent<Player> ().stopBackup = false;
			eBar.valueCurrent = eBar.valueCurrent - damage; //set energy bar value
			backupTime = time;
		}

		//destroy FLYING DUCKLING
		if ((transform.position.z > (potTransform.position.z + potInstant.destroyRangeMaxZ)) || (transform.position.z < (potTransform.position.z - potInstant.destroyRangeMinZ)) || (transform.position.x > (potTransform.position.x + potInstant.destroyRangeX)) || (transform.position.x < (potTransform.position.x - potInstant.destroyRangeX))) {
			potInstant.currentSpiderNum--;
			Destroy (this.gameObject);
		}
	}

	private void flying() {
 		transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.position - potTransform.position, Vector3.down), Vector3.up);
		transform.RotateAround(potTransform.position, Vector3.up, flySpeed * Time.deltaTime);
	}

	private void landing() {
		transform.position += transform.forward * landForward * Time.deltaTime;
		transform.Translate (0, -(landTransY * Time.deltaTime), 0);
	}
}
