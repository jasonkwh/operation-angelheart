using UnityEngine;
using System.Collections;

public class InstantFlyDuck : InstantSpider {

	public bool generateFlyDuck = false;

	protected override void Start () {
		setRandomTime ();
	}
	protected override void FixedUpdate () {
		time += Time.deltaTime;

		if (generateFlyDuck == true) {
			if(backup == false) {
				backupTime = time;
				backup = true;
			}

			if ((currentSpiderNum < limits) && (time >= (randomTime + backupTime))) {
				spawnSpiders ();
				setRandomTime();
			}
		}
	}
	protected override void spawnSpiders() {
		time = 0;
		int positionChance = duckPositionChance();
		minPositionX = this.gameObject.transform.position.x - rangePositionX;
		maxPositionX = this.gameObject.transform.position.x + rangePositionX;
		minPositionZ = this.gameObject.transform.position.z - minRangePositionZ;
		maxPositionZ = this.gameObject.transform.position.z + maxRangePositionZ;
		randomPositionX = setRandomPosition (minPositionX, maxPositionX);
		randomPositionZ = setRandomPosition (minPositionZ, maxPositionZ);

		if(positionChance == 1) {
			instantDuck(new Vector3(maxPositionX,fixedPositionY,randomPositionZ));
		} else if(positionChance == 2) {
			instantDuck(new Vector3(minPositionX,fixedPositionY,randomPositionZ));
		} else if(positionChance == 3) {
			instantDuck(new Vector3(randomPositionX,fixedPositionY,maxPositionZ));
		} else if(positionChance == 4) {
			instantDuck(new Vector3(randomPositionX,fixedPositionY,minPositionZ));
		}
		currentSpiderNum++;
		backup = false;
	}

	private int duckPositionChance() {
		return (int)(Random.Range(1,4));
	}

	private void instantDuck(Vector3 duckPosition) {
		GameObject smallDuck;
		smallDuck = (GameObject)Instantiate (spiderPrefab, duckPosition, spiderPrefab.transform.rotation);
	}
}
