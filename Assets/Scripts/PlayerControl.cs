using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private bool directionUp = false;
	private bool directionRight = false;
	private bool directionDown = false;
	private bool directionLeft = false;
	private float minSwipeDist  = 10.0f;
	private float gestureDist = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;
	public float speed;
	
	// Update is called once per frame
	void Update () {
		
		//touch control
		if (Input.touchCount > 0) {
			foreach (Touch touch in Input.touches) {
				switch (touch.phase) {
				case TouchPhase.Began:
					directionUp = false;
					directionDown = false;
					directionRight = false;
					directionLeft = false;
					fingerStartPos = touch.position;
					break;
				
				case TouchPhase.Moved:
					gestureDist = (touch.position - fingerStartPos).magnitude;

				//if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist) {
					Vector2 direction = touch.position - fingerStartPos;
					Vector2 swipeType = Vector2.zero;

					if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y)) {
						// the swipe is horizontal:
						swipeType = Vector2.right * Mathf.Sign (direction.x);
					} else {
						// the swipe is vertical:
						swipeType = Vector2.up * Mathf.Sign (direction.y);
					}

					if (swipeType.x != 0.0f) {
						if (swipeType.x > 0.0f) {
							moveRight ();
							directionRight = true;
						} else {
							moveLeft ();
							directionLeft = true;
						}
					}

					if (swipeType.y != 0.0f) {
						if (swipeType.y > 0.0f) {
							moveUp ();
							directionUp = true;
						} else {
							moveDown ();
							directionDown = true;
						}
					}
				//}
					break;

				case TouchPhase.Stationary:
					if (gestureDist > minSwipeDist)
					if (directionUp == true)
						moveUp ();
					if (directionRight == true)
						moveRight ();
					if (directionLeft == true)
						moveLeft ();
					if (directionDown == true)
						moveDown ();
					break;

				case TouchPhase.Canceled:
					directionUp = false;
					directionDown = false;
					directionLeft = false;
					directionRight = false;
					break;
				}
			}
		}

		//keyboard control
		else if (Input.touchCount == 0) {
			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
				moveUp();
			}
			if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
				moveDown();
			}
			if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
				moveLeft();
			}
			if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow))
			{
				moveRight();
			}
		}
	}

	void moveUp() {
		transform.position += Vector3.forward * Time.deltaTime * speed; 
	}

	void moveDown() {
		transform.position += Vector3.back * Time.deltaTime * speed;
	}

	void moveRight() {
		transform.position += Vector3.right * Time.deltaTime * speed;
	}

	void moveLeft() {
		transform.position += Vector3.left * Time.deltaTime * speed;
	}
}
