using UnityEngine;
using System.Collections;

public class AiBear : MonoBehaviour {
//public class AiBear : AiDuck {

	private float dist;
	private Transform potTransform;
	private Animator anim;
	public float bounceRange;
	public int damage;
	private EnergyBar eBar;
	public float maxDist;
	public float rotateSpeed;
	public float frontAngle;

	void Start() {
		potTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		anim = gameObject.GetComponent<Animator>();
		eBar = GameObject.FindGameObjectWithTag("energyBar").GetComponent<EnergyBar>();
	}

	void Update() {
	//protected override void FixedUpdate() {
		//time += Time.deltaTime;
		dist = Vector3.Distance (potTransform.position, transform.position);

		/*if(dist < maxDist) {
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
		}*/

		//determine if front
		Vector3 directionToTarget = transform.position - potTransform.position;
		float angle = Vector3.Angle(transform.forward, directionToTarget);
		float distance = directionToTarget.magnitude;

		//animation control
		if ((Mathf.Abs(angle) > frontAngle) && (dist < maxDist)) {
			anim.SetInteger("BearState", 2); //attack
			bearRotate();
		} else {
			anim.SetInteger("BearState", 0); //stay
		}

		//damages
		if ((Mathf.Abs(angle) > frontAngle) && (dist < bounceRange)) {
			potTransform.GetComponent<Player> ().pX = potTransform.position.x - transform.position.x;
			potTransform.GetComponent<Player> ().pZ = potTransform.position.z - transform.position.z;
			potTransform.GetComponent<Player> ().pushing = true;
			potTransform.GetComponent<Player> ().stopBackup = false;
			eBar.valueCurrent = eBar.valueCurrent - damage; //set energy bar value
			//backupTime = time;
		}
	}

	void bearRotate() {
		//Look at me please!
		Quaternion rotation = Quaternion.LookRotation (potTransform.position - transform.position);
		rotation.x = 0.0f; //freeze x axis
		rotation.z = 0.0f; //freeze z axis
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, (rotateSpeed * Time.deltaTime));
	}
}
