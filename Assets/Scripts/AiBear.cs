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
	//public float maxDist;
	public float rotateSpeed;
	public float frontAngle;
	private float time;
	private float backupTime;
	private bool backup = false;
	public float staticTime = 1f;

	void Start() {
		potTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		anim = gameObject.GetComponent<Animator>();
		eBar = GameObject.FindGameObjectWithTag("energyBar").GetComponent<EnergyBar>();
	}

	void Update() {
        //transform.LookAt(potTransform);
        bearRotate();
		time += Time.deltaTime;
		dist = Vector3.Distance (potTransform.position, transform.position);

		if(backup == false) {
			backupTime = time;
			backup = true;
		}

		if ((time < (backupTime + staticTime)) && (anim.GetCurrentAnimatorStateInfo(0).nameHash == -833002391)) {
			anim.SetInteger("BearState", 2); //attack
			bearAttack();
			//bearRotate();
		} else if ((time > (backupTime + staticTime)) && (anim.GetCurrentAnimatorStateInfo(0).nameHash != -833002391)) {
			anim.SetInteger("BearState", 0); //stay
			backup = false;
		}

		//animation control
		/*if ((Mathf.Abs(angle) > frontAngle) && (dist < maxDist)) {
			anim.SetInteger("BearState", 2); //attack
			bearRotate();
		} else {
			anim.SetInteger("BearState", 0); //stay
		}*/
	}

	void bearRotate() {
		//Look at me please!
		Quaternion rotation = Quaternion.LookRotation (potTransform.position - transform.position);
		rotation.x = 0.0f; //freeze x axis
		rotation.z = 0.0f; //freeze z axis
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, (rotateSpeed * Time.deltaTime));
	}

	void bearAttack() {
		//determine if front
		Vector3 directionToTarget = transform.position - potTransform.position;
		float angle = Vector3.Angle(transform.forward, directionToTarget);
		//print(angle);

		//damages
		if ((Mathf.Abs(angle) > frontAngle) && (dist < bounceRange)) {
			potTransform.GetComponent<Player> ().pX = potTransform.position.x - transform.position.x;
			potTransform.GetComponent<Player> ().pZ = potTransform.position.z - transform.position.z;
			potTransform.GetComponent<Player> ().pushing = true;
			potTransform.GetComponent<Player> ().stopBackup = false;
			eBar.valueCurrent = eBar.valueCurrent - damage; //set energy bar value
			/*if (Mathf.Abs(angle) > frontAngle) {
				eBar.valueCurrent = eBar.valueCurrent - damage; //set energy bar value
			} else {
				potTransform.GetComponent<Player> ().bearCollider = true;
			}*/
		}
	}
}
