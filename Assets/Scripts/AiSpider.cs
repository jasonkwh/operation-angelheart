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
	private float accelerateBackup;
	private bool backupUsed = false;

	void Start() {
		accelerateBackup = accelerateBasic;
	}

	void FixedUpdate() {
		time += Time.deltaTime;
		if (time < gap) {
			transform.Translate (0, -(accelerateBasic * Time.deltaTime), 0);
			if (accelerateBasic < maxSpeed) {
				accelerateBasic += accelerateRate + 0.4f;
			}
		}
		else if ((time > (gap + stay)) && (time < end)) {
			if (backupUsed == false) {
				accelerateBasic = accelerateBackup;
				backupUsed = true;
			}
			transform.Translate (0, -(accelerateBasic * Time.deltaTime), 0);
			if (accelerateBasic < maxSpeed) {
				accelerateBasic += accelerateRate + 0.8f;
			}
		}
		print (time);
	}
}
