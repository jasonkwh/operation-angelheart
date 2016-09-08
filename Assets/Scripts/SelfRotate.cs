using UnityEngine;
using System.Collections;

public class SelfRotate : MonoBehaviour {

	public float rotateSpeed;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, 0, rotateSpeed * Time.deltaTime));
	}
}
