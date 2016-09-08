using UnityEngine;
using System.Collections;

public class InstantSpider : MonoBehaviour {

	public GameObject spiderPrefab;

	//variables for time
	public float minTime;
	public float maxTime;
	private float randomTime;
	private float time;
	private float backupTime;
	private bool backup = false;

	//variables for position
	public GameObject pot;
	public float fixedPositionY;
	public float maxRangePositionZ;
	public float minRangePositionZ;
	private float minPositionZ;
	private float maxPositionZ;
	private float randomPositionZ;
	public float rangePositionX;
	private float minPositionX;
	private float maxPositionX;
	private float randomPositionX;

	//Limits
	public int limits;
	public int currentSpiderNum = 0;

	//to destroy spiders...
	public float destroyRangeX;
	public float destroyRangeMaxZ;
	public float destroyRangeMinZ;

	// Use this for initialization
	void Start () {
		setRandomTime ();
		//time = minTime;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		time += Time.deltaTime;

		if ((pot.GetComponent<Player> ().ate == true) && (backup == false)) {
			backupTime = time;
			backup = true;
		}

		if ((time >= (randomTime + backupTime)) && (currentSpiderNum < limits) && (pot.GetComponent<Player>().ate == true)) {
			spawnSpiders ();
			setRandomTime ();
		}
	}

	void spawnSpiders() {
		time = 0;
		minPositionX = pot.transform.position.x - rangePositionX;
		maxPositionX = pot.transform.position.x + rangePositionX;
		minPositionZ = pot.transform.position.z - minRangePositionZ;
		maxPositionZ = pot.transform.position.z + maxRangePositionZ;
		randomPositionX = setRandomPosition (minPositionX, maxPositionX);
		randomPositionZ = setRandomPosition (minPositionZ, maxPositionZ);
		Instantiate (spiderPrefab, new Vector3(randomPositionX,fixedPositionY,randomPositionZ), spiderPrefab.transform.rotation);
		currentSpiderNum++;
		backup = false;
	}

	void setRandomTime() {
		randomTime = Random.Range (minTime, maxTime);
	}

	float setRandomPosition(float min, float max) {
		float randomNum = 0f;
		randomNum = Random.Range (min, max);
		return randomNum;
	}
}
