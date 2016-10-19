using UnityEngine;
using System.Collections;

public class Points : MonoBehaviour {

	public GameObject points;

	// Update is called once per frame
	void Update () {
		points.GetComponent<GUIText> ().transform.position = Camera.main.WorldToViewportPoint (this.gameObject.transform.position);
	}
}
