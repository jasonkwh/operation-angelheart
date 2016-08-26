using UnityEngine;
using System.Collections;

public class InstantSpider : MonoBehaviour {

	public GameObject spiderPrefab;

	//variables for time
	public float minTime;
	public float maxTime;
	private float randomTime;
	private float time;

	//variables for position
	public float minPositionZ;
	public float maxPositionZ;
	private float randomPositionZ;
	public float minPositionX;
	public float maxPositionX;
	private float randomPositionX;


	// Use this for initialization
	void Start () {
		setRandomTime ();
		time = minTime;
		randomPositionX = setRandomPosition (minPositionX, maxPositionZ);
		randomPositionZ = setRandomPosition (minPositionZ, maxPositionZ);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		time += Time.deltaTime;

		if (time >= randomTime) {
			spawnSpiders ();
			setRandomTime ();
			randomPositionX = setRandomPosition (minPositionX, maxPositionZ);
			randomPositionZ = setRandomPosition (minPositionZ, maxPositionZ);
		}

		Debug.Log (time);
	}

	void spawnSpiders() {
		time = 0;
		Instantiate (spiderPrefab, new Vector3(randomPositionX,randomPositionZ,0), spiderPrefab.transform.rotation);
	}

	void setRandomTime() {
		randomTime = Random.Range (minTime, maxTime);
	}

	float setRandomPosition(float min, float max) {
		float randomNum = 0f;
		randomNum = Random.Range (minPositionZ, maxPositionZ);
		return randomNum;
	}
}
