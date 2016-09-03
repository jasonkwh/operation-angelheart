using UnityEngine;
using System.Collections;

public class AiDuck : MonoBehaviour {

	private float time;
	private float backupTime;
	private bool stopRanTime = false;
	public float minStayTime;
	public float maxStayTime;
	private float stayTime;
	public float maxSpeed;
	public float accelerate;
	private float moveSpeed = 1.0f;
	public float rotateSpeed;
	public float maxRandomTime;
	public float minRandomTime;
	private float randomTime = 0;
	private Transform potTransform;
	private float dist;
	public float maxDist;

	//for bouncing...
	public float bounceRange;
	private float bounceTime;

	void Start() {
		potTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		bounceTime = potTransform.GetComponent<Player> ().stayTime;
	}

	void FixedUpdate () {
		time += Time.deltaTime;
		dist = Vector3.Distance (potTransform.position, transform.position);

		if (dist < maxDist) {
			duckRotate ();

			if (potTransform.GetComponent<Player> ().pushing == false) {
				if (stopRanTime == false) {
					backupTime = time;
					randomTime = ranTime (minRandomTime, maxRandomTime);
					stayTime = ranTime (minStayTime, maxStayTime);
					stopRanTime = true;
				}

				if (time < (backupTime + randomTime)) {
					speedAcceleration ();
				} else if (time > (backupTime + randomTime + stayTime)) {
					moveSpeed = 1.0f;
					stopRanTime = false;
				}
			} else {
				if (time > (backupTime + bounceTime)) {
					potTransform.GetComponent<Player> ().pushing = false;
				}
			}
		}

		if (dist < bounceRange) {
			//potTransform.position += transform.forward * moveSpeed * Time.deltaTime;
			potTransform.GetComponent<Player> ().pushing = true;
			potTransform.GetComponent<Player> ().stopBackup = false;
			backupTime = time;
		}
	}

	void speedAcceleration() {
		//Come babe,
		if (moveSpeed < maxSpeed) {
			moveSpeed = moveSpeed + accelerate;
		}
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
	}

	void duckRotate() {
		//Look at me please!
		Quaternion rotation = Quaternion.LookRotation (potTransform.position - transform.position);
		rotation.x = 0.0f; //freeze x axis
		rotation.z = 0.0f; //freeze z axis
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, (rotateSpeed * Time.deltaTime));
	}

	float ranTime(float min, float max) {
		return Random.Range (min, max);
	}
}
