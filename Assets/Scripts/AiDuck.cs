using UnityEngine;
using System.Collections;

public class AiDuck : MonoBehaviour {

	protected float time;
	protected float backupTime;
	protected bool stopRanTime = false;
	public float minStayTime;
	public float maxStayTime;
	protected float stayTime;
	public float maxSpeed;
	public float accelerate;
	protected float moveSpeed = 1.0f;
	public float rotateSpeed;
	public float maxRandomTime;
	public float minRandomTime;
	protected float randomTime = 0;
	protected Transform potTransform;
	protected float dist;
	public float maxDist;

	//for bouncing...
	public float bounceRange;
	protected float bounceTime;

	//for energy system 
	protected EnergyBar eBar;
	public int damage;

	//animation control
	protected Animator anim;
	public float standStillTime;
	public float standRoarTime;
	protected float backupTimeStand;
	protected bool finishStandBackup = false;
	public bool finishStand = false;
	protected bool foundPot = false;

	protected void Start() {
		potTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		bounceTime = potTransform.GetComponent<Player> ().stayTime;
		eBar = GameObject.FindGameObjectWithTag("energyBar").GetComponent<EnergyBar>();
		anim = gameObject.GetComponent<Animator>();
	}

	protected virtual void FixedUpdate () {
		time += Time.deltaTime;
		dist = Vector3.Distance (potTransform.position, transform.position);

		if ((dist < maxDist) && (foundPot == false)) {
			foundPot = true;
		}
		if(foundPot == true) {
			if(finishStandBackup == false) {
				backupTimeStand = time;
				finishStandBackup = true;
			}

			if(finishStand == false) {
				if(time < (backupTimeStand + standStillTime)) {
					anim.SetInteger("DuckState", 2); //stand
				} else if ((time > (backupTimeStand + standStillTime)) && (time < (backupTimeStand + standStillTime + standRoarTime))) {
					duckRotate ();
					anim.SetInteger("DuckState", 4); //roar
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
						anim.SetInteger("DuckState", 3); //run
						speedAcceleration ();
					} else if ((time > (backupTime + randomTime)) && (time < (backupTime + randomTime + stayTime))) {
						anim.SetInteger("DuckState", 4); //roar
					} else if (time > (backupTime + randomTime + stayTime)) {
						moveSpeed = 1.0f;
						stopRanTime = false;
					}
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

	protected void speedAcceleration() {
		//Come babe,
		if (moveSpeed < maxSpeed) {
			moveSpeed = moveSpeed + accelerate;
		}
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
	}

	protected void duckRotate() {
		//Look at me please!
		Quaternion rotation = Quaternion.LookRotation (potTransform.position - transform.position);
		rotation.x = 0.0f; //freeze x axis
		rotation.z = 0.0f; //freeze z axis
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, (rotateSpeed * Time.deltaTime));
	}

	protected float ranTime(float min, float max) {
		return Random.Range (min, max);
	}
}
