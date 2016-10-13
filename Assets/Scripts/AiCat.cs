using UnityEngine;
using System.Collections;

public class AiCat : MonoBehaviour {

	private Transform potTransform;
	private float time;
	public float minStayTime;
	public float maxStayTime;
	public float jumpTime;
	private float stayTime;
	private float backupTime;
	private bool stopRanTime = false;
	public float rotateSpeed;

	//for jumping
	//private float jumpStartVelocityY;
	//public float jumpDuration;
	public float moveSpeed;
	private bool jumping = false;
	private float dist;
	public int jumpHeight;
	//public float maxJumpDist;
	private Rigidbody objectRig;

	void Start () {
		potTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		objectRig = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		time += Time.deltaTime;
		if (stopRanTime == false) {
			backupTime = time;
			stayTime = ranTime (minStayTime, maxStayTime);
			stopRanTime = true;
		}
		if (time < (backupTime + stayTime)) {
			catRotate ();
		} else {
			dist = Vector3.Distance (potTransform.position, transform.position);
			/*if ((dist < maxJumpDist) && (jumping == false)) {
				jumpStartVelocityY = -jumpDuration * Physics.gravity.y / 4;
				StartCoroutine (Jump (potTransform.position * dist));
			}*/
			transform.position += transform.forward * moveSpeed * Time.deltaTime;
			if (jumping == false) {
				objectRig.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
				jumping = true;
			}
		}
	}

	void catRotate() {
		//Look at me please!
		Quaternion rotation = Quaternion.LookRotation (potTransform.position - transform.position);
		rotation.x = 0.0f; //freeze x axis
		rotation.z = 0.0f; //freeze z axis
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, (rotateSpeed * Time.deltaTime));
	}

	float ranTime(float min, float max) {
		return Random.Range (min, max);
	}

	/*private IEnumerator Jump(Vector3 direction)
	{
		jumping = true;
		Vector3 startPoint = transform.position;
		Vector3 targetPoint = startPoint + direction;
		float time2 = 0;
		float jumpProgress = 0;
		float velocityY = jumpStartVelocityY;
		float height = startPoint.y;

		while (jumping)
		{
			jumpProgress = time2 / jumpDuration;

			if (jumpProgress > 1)
			{
				jumping = false;
				jumpProgress = 1;
			}

			Vector3 currentPos = Vector3.Lerp(startPoint, targetPoint, jumpProgress);
			currentPos.y = height;
			transform.position = currentPos;

			//Wait until next frame.
			yield return null;

			height += velocityY * Time.deltaTime;
			velocityY += Time.deltaTime * Physics.gravity.y;
			time2 += Time.deltaTime;
		}

		transform.position = targetPoint;
		yield break;
	}*/
}
