using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {

	public float moveSpeed;
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime, 0f, moveSpeed*Input.GetAxis("Vertical")*Time.deltaTime*2);
	}
}
