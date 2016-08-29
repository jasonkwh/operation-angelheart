using UnityEngine;
using System.Collections;

public class AiSpider : MonoBehaviour {

	//public float acceleration;
	private float time;
	public float accelerateBasic;
	public float accelerateRate;
	public float maxSpeed;
	public float gap;
	public float stay;
	public float end;
	public float stay2;
	private float accelerateBackup;
	private bool backupUsed = false;

	//for AI use
	private Transform potTransform;
	//public float rotateSpeed;
	public float moveSpeed;
	public float speedUpDown;
	private bool randomCheck = false;
	private float randomTime;
	private float backupTime;
	public float maxRandomTime;
	private bool stopRanTime = false;
	private bool stopRanCheck = false;

	void Start() {
		accelerateBackup = accelerateBasic;
		potTransform = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void FixedUpdate() {
		time += Time.deltaTime;
		if (time < gap) {
			transform.Translate (0, -(accelerateBasic * Time.deltaTime), 0);
			if (accelerateBasic < maxSpeed) {
				accelerateBasic += accelerateRate + 0.4f;
			}
		} else if ((time > (gap + stay)) && (time < end)) {
			if (backupUsed == false) {
				accelerateBasic = accelerateBackup;
				backupUsed = true;
			}
			transform.Translate (0, -(accelerateBasic * Time.deltaTime), 0);
			if (accelerateBasic < maxSpeed) {
				accelerateBasic += accelerateRate + 0.8f;
			}
		} else if (time > (end + stay2)) {
			//Look at me please!
			//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(potTransform.position - transform.position), (rotateSpeed * Time.deltaTime));

			//Come babe,
			//transform.position += transform.forward * moveSpeed * Time.deltaTime;
			if (stopRanTime == false) {
				ranTime ();
			}
			if (time < (backupTime + randomTime)) {
				if (stopRanCheck == false) {
					randomCheck = ranCheck ();
					print (randomCheck);
					stopRanCheck = true;
				}
				moveXorZ (randomCheck);
			} else {
				stopRanTime = false;
				stopRanCheck = false;
			}
		}
		//print (time);
	}

	void moveXorZ(bool check) {
		if ((check == false) && (Mathf.Abs (potTransform.position.x - transform.position.x) > 0)) {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (potTransform.position.x, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
		} else if ((check == false) && (Mathf.Abs (potTransform.position.x - transform.position.x) == 0)) {
			moveXorZ (true);
		}
		if ((check == true) && (Mathf.Abs (potTransform.position.z - transform.position.z) > 0)) {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, transform.position.y, potTransform.position.z), moveSpeed * Time.deltaTime * speedUpDown);
		} else if ((check == true) && (Mathf.Abs (potTransform.position.z - transform.position.z) == 0)) {
			moveXorZ (false);
		}
	}

	void ranTime () {
		backupTime = time;
		randomTime = Random.Range (1.0f, maxRandomTime);
		stopRanTime = true;
	}

	bool ranCheck() {
		float fRand = Random.Range (0.0f, 1.0f);
		print (fRand);
		if (fRand < 0.5f) {
			return true;
		} else {
			return false;
		}
	}
}
