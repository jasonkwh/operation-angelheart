using UnityEngine;
using System.Collections;

public class AiLittleDuck : AiDuck {

	Quaternion rotation;
	private int ducklingNum;
	private string prevDucklingName;
	AudioSource audio;

	protected override void Start() {
		//same initialization as AiDuck.cs
		potTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		bounceTime = potTransform.GetComponent<Player> ().stayTime;
		eBar = GameObject.FindGameObjectWithTag("energyBar").GetComponent<EnergyBar>();
		anim = gameObject.GetComponent<Animator>();
		audio = gameObject.GetComponent<AudioSource>();

		//grab transform name,
		string[] ducklingName= transform.name.Split ('_');
		ducklingNum = int.Parse(ducklingName[1]);
		if(ducklingNum > 0) {
			prevDucklingName = "smallDuck_" + (ducklingNum - 1).ToString();
		}
	}

	protected override void FixedUpdate() {
		time = GameObject.FindGameObjectWithTag("duck").GetComponent<AiDuck>().time;
		backupTime = GameObject.FindGameObjectWithTag("duck").GetComponent<AiDuck>().backupTime;
		randomTime = GameObject.FindGameObjectWithTag("duck").GetComponent<AiDuck>().randomTime;
		stayTime = GameObject.FindGameObjectWithTag("duck").GetComponent<AiDuck>().stayTime;
		dist = Vector3.Distance (potTransform.position, transform.position);
		duckRotate();

		if (potTransform.GetComponent<Player> ().pushing == false) {
			if (time < (backupTime + randomTime)) {
				anim.SetInteger("DucklingState", 1); //run
				speedAcceleration ();
			} else if ((time > (backupTime + randomTime)) && (time < (backupTime + randomTime + stayTime))) {
				anim.SetInteger("DucklingState", 2); //roar
				if(audio.isPlaying == false) {
					audio.PlayDelayed(0.35f);
				}
			} else if (time > (backupTime + randomTime + stayTime)) {
				moveSpeed = 1.0f;
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
	}

	protected override void duckRotate() {
		//Look at previous duck please!
		if(ducklingNum == 0) {
			rotation = Quaternion.LookRotation (GameObject.FindGameObjectWithTag("duck").transform.position - transform.position);
		} else {
			rotation = Quaternion.LookRotation (GameObject.Find(prevDucklingName).transform.position - transform.position);
		}
		rotation.x = 0.0f; //freeze x axis
		rotation.z = 0.0f; //freeze z axis
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, (rotateSpeed * Time.deltaTime));
	}

}
