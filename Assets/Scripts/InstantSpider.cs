using UnityEngine;
using System.Collections;

public class InstantSpider : MonoBehaviour {

	public GameObject spiderPrefab;

	//variables for time
	public float minTime;
	public float maxTime;
	protected float randomTime;
	protected float time;
	protected float backupTime;
	protected bool backup = false;

	//variables for position
	public float fixedPositionY;
	public float maxRangePositionZ;
	public float minRangePositionZ;
	protected float minPositionZ;
	protected float maxPositionZ;
	protected float randomPositionZ;
	public float rangePositionX;
	protected float minPositionX;
	protected float maxPositionX;
	protected float randomPositionX;

	//Limits
	public int limits;
	public int currentSpiderNum = 0;

	//to destroy spiders...
	public float destroyRangeX;
	public float destroyRangeMaxZ;
	public float destroyRangeMinZ;

	// Use this for initialization
	protected virtual void Start () {
		setRandomTime ();
		//time = minTime;
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
		time += Time.deltaTime;

		if ((this.gameObject.GetComponent<Player> ().ate == true) && (backup == false)) {
			backupTime = time;
			backup = true;
		}

		if ((time >= (randomTime + backupTime)) && (currentSpiderNum < limits) && (this.gameObject.GetComponent<Player>().ate == true)) {
			spawnSpiders ();
			setRandomTime ();
		}
	}

	protected virtual void spawnSpiders() {
		time = 0;
		minPositionX = this.gameObject.transform.position.x - rangePositionX;
		maxPositionX = this.gameObject.transform.position.x + rangePositionX;
		minPositionZ = this.gameObject.transform.position.z - minRangePositionZ;
		maxPositionZ = this.gameObject.transform.position.z + maxRangePositionZ;
		randomPositionX = setRandomPosition (minPositionX, maxPositionX);
		randomPositionZ = setRandomPosition (minPositionZ, maxPositionZ);
		Instantiate (spiderPrefab, new Vector3(randomPositionX,fixedPositionY,randomPositionZ), spiderPrefab.transform.rotation);
		currentSpiderNum++;
		backup = false;
	}

	protected void setRandomTime() {
		randomTime = Random.Range (minTime, maxTime);
	}

	protected float setRandomPosition(float min, float max) {
		float randomNum = 0f;
		randomNum = Random.Range (min, max);
		return randomNum;
	}
}
