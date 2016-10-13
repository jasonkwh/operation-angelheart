using UnityEngine;
using System.Collections;

public class HealthPlayer : MonoBehaviour {

	public GameObject pot;
	public RectTransform bar;
	
	// Update is called once per frame
	void Update () {
		Vector2 pos = pot.transform.position;  // get the game object position
		Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);  //convert game object position to VievportPoint

		// set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
		bar.anchorMin = viewportPoint;  
		bar.anchorMax = viewportPoint; 
	}
}
