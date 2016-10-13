using UnityEngine;
using System.Collections;

public class AiSpider : MonoBehaviour {

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
	private float dist;
	//public float maxDist;

	//for AI use
	private Transform potTransform;
	public float moveSpeed;
	public float speedUpDown;
	private bool randomCheck = false;
	private float randomTime;
	private float backupTime;
	public float maxRandomTime;
	private bool stopRanTime = false;
	private bool stopRanCheck = false;

	//for bouncing...
	public float bounceRange;

	//to display spider net
	public GameObject net;
	private bool netDestroy = false;

	//to destroy spiders
	private InstantSpider potInstant;

	//for energy system 
	private EnergyBar eBar;
	public int damage;

	//animator
	private Animator anim;
	private Animation ani;

	void Start() {
		accelerateBackup = accelerateBasic;
		potTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		potInstant = GameObject.FindGameObjectWithTag ("Player").GetComponent<InstantSpider> ();
		eBar = GameObject.FindGameObjectWithTag("energyBar").GetComponent<EnergyBar>();
		anim = gameObject.GetComponentInChildren<Animator>();
		Physics.IgnoreLayerCollision (9,9); //ignore collision of enemies
	}

	void FixedUpdate() {
		time += Time.deltaTime;
		dist = Vector3.Distance (potTransform.position, transform.position);

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
			//if ((dist < maxDist) && (potTransform.GetComponent<Player> ().pushing == false))  {
			if (potTransform.GetComponent<Player> ().pushing == false)  {
				if (stopRanTime == false) {
					ranTime ();
				}
				if (time < (backupTime + randomTime)) {
					//anim.SetInteger("AnimPar", 1); //stay for a while
					if (stopRanCheck == false) {
						randomCheck = ranCheck ();
						stopRanCheck = true;
					}
					moveXorZ (randomCheck);
				} else {
					stopRanTime = false;
					stopRanCheck = false;
				}
			}

			if (dist < bounceRange) {
				//potTransform.position += transform.forward * moveSpeed * Time.deltaTime;
				potTransform.GetComponent<Player> ().pX = potTransform.position.x - transform.position.x;
				potTransform.GetComponent<Player> ().pZ = potTransform.position.z - transform.position.z;
				potTransform.GetComponent<Player> ().pushing = true; //enable bounce animation
				potTransform.GetComponent<Player> ().stopBackup = false;
				eBar.valueCurrent = eBar.valueCurrent - damage; //set energy bar value
				backupTime = time;
			}
		}

		//destroy spider net
		if ((time > end) && (netDestroy == false)) {
			Destroy (net);
			netDestroy = true;
		}
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

		//destroy spider
		if ((transform.position.z > (potTransform.position.z + potInstant.destroyRangeMaxZ)) || (transform.position.z < (potTransform.position.z - potInstant.destroyRangeMinZ)) || (transform.position.x > (potTransform.position.x + potInstant.destroyRangeX)) || (transform.position.x < (potTransform.position.x - potInstant.destroyRangeX))) {
			potInstant.currentSpiderNum--;
			Destroy (this.gameObject);
		}
	}

	void ranTime () {
		backupTime = time;
		randomTime = Random.Range (1.0f, maxRandomTime);
		stopRanTime = true;
	}

	bool ranCheck() {
		float fRand = Random.Range (0.0f, 1.0f);

		if (fRand < 0.5f) {
			return true;
		} else {
			return false;
		}
	}
}
