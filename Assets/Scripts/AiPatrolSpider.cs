using UnityEngine;
using System.Collections;

public class AiPatrolSpider : MonoBehaviour {

	public GameObject table;
	public float moveSpeed;
	public float speedUpDown;
	private Vector3 topLeft;
	private Vector3 topRight;
	private Vector3 bottomLeft;
	private Vector3 bottomRight;
	private Vector3 tempPosition;
	private bool tempPositionBackup = false;
	private bool tempPositionBackup2 = false;
	private Transform potTransform;
	private EnergyBar eBar;
	private float dist;
	public float bounceRange;
	public int damage;

	void Start() {
		potTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		eBar = GameObject.FindGameObjectWithTag("energyBar").GetComponent<EnergyBar>();
		transform.position = new Vector3(table.transform.position.x + 10, 0, table.transform.position.z);
		topRight = new Vector3(table.transform.position.x + 10, 0, table.transform.position.z + 20);
		topLeft = new Vector3(table.transform.position.x - 10, 0, table.transform.position.z + 20);
		bottomLeft = new Vector3(table.transform.position.x - 10, 0, table.transform.position.z - 20);
		bottomRight = new Vector3(table.transform.position.x + 10, 0, table.transform.position.z - 20);
	}
	
	// Update is called once per frame
	void Update () {
		dist = Vector3.Distance (potTransform.position, transform.position);

		if ((transform.position.x == (table.transform.position.x + 10)) && (transform.position.z < (table.transform.position.z + 20))) {
			if(tempPositionBackup == false) {
				tempPosition = transform.position;
				tempPositionBackup = true;
			}
			tempPositionBackup2 = false;
			transform.position = Vector3.MoveTowards (transform.position, topRight, moveSpeed * Time.deltaTime * speedUpDown);
			transform.rotation = Quaternion.LookRotation(topRight - tempPosition);
		} else if ((transform.position.x == (table.transform.position.x + 10)) && (transform.position.z > (table.transform.position.z + 20))) {
			transform.position = topRight;
		}
		if ((transform.position.x > (table.transform.position.x - 10)) && (transform.position.z == (table.transform.position.z + 20))) {
			if(tempPositionBackup2 == false) {
				tempPosition = transform.position;
				tempPositionBackup2 = true;
			}
			tempPositionBackup = false;
			transform.position = Vector3.MoveTowards (transform.position, topLeft, moveSpeed * Time.deltaTime);
			transform.rotation = Quaternion.LookRotation(topLeft - tempPosition);
		} else if ((transform.position.x < (table.transform.position.x - 10)) && (transform.position.z == (table.transform.position.z + 20))) {
			transform.position = topLeft;
		}
		if ((transform.position.x == (table.transform.position.x - 10)) && (transform.position.z > (table.transform.position.z - 20))) {
			if(tempPositionBackup == false) {
				tempPosition = transform.position;
				tempPositionBackup = true;
			}
			tempPositionBackup2 = false;
			transform.position = Vector3.MoveTowards (transform.position, bottomLeft, moveSpeed * Time.deltaTime * speedUpDown);
			transform.rotation = Quaternion.LookRotation(bottomLeft - tempPosition);
		} else if ((transform.position.x == (table.transform.position.x - 10)) && (transform.position.z < (table.transform.position.z - 20))) {
			transform.position = bottomLeft;
		}
		if ((transform.position.x < (table.transform.position.x + 10)) && (transform.position.z == (table.transform.position.z - 20))) {
			if(tempPositionBackup2 == false) {
				tempPosition = transform.position;
				tempPositionBackup2 = true;
			}
			tempPositionBackup = false;
			transform.position = Vector3.MoveTowards (transform.position, bottomRight, moveSpeed * Time.deltaTime);
			transform.rotation = Quaternion.LookRotation(bottomRight - tempPosition);
		} else if ((transform.position.x > (table.transform.position.x + 10)) && (transform.position.z == (table.transform.position.z - 20))) {
			transform.position = bottomRight;
		}

		if (dist < bounceRange) {
			//potTransform.position += transform.forward * moveSpeed * Time.deltaTime;
			potTransform.GetComponent<Player> ().pX = potTransform.position.x - transform.position.x;
			potTransform.GetComponent<Player> ().pZ = potTransform.position.z - transform.position.z;
			potTransform.GetComponent<Player> ().pushing = true; //enable bounce animation
			potTransform.GetComponent<Player> ().stopBackup = false;
			eBar.valueCurrent = eBar.valueCurrent - damage; //set energy bar value
		}
	}
}
