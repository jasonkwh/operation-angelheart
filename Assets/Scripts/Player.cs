using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private Animator anim;
    private Animation ani;
    private CharacterController controller;
    public float speed;
	public float speedUpDown;
    //public float turnSpeed = 60.0f;
    //private Vector3 moveDirection = Vector3.zero;
    //public float gravity = 20.0f;
	public bool ate = false;
	public GameObject smoke;

	//variables for touch
	private bool directionUp = false;
	private bool directionRight = false;
	private bool directionDown = false;
	private bool directionLeft = false;
	private float minSwipeDist  = 10.0f;
	private float gestureDist = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;

    void Start () {
        anim = gameObject.GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        ani["potWalk"].speed = 2.0f;
    }
	
	// Update is called once per frame
	void Update () {
		//Frame rate Per Second
		//Debug.Log("FPS: " + 1/Time.deltaTime);

		smoke.SetActive (ate);

		//touch control
		if (Input.touchCount > 0) {
			//StartCoroutine (GenerateTrails (trailsWaitTime));
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
			//StopCoroutine (GenerateTrails (trailsWaitTime));
			anim.SetInteger("AnimPar", 0); //stable

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

        /*if (Input.GetKey("up"))
        {
            anim.SetInteger("AnimPar", 1); //moving
        }
        else
        {
            anim.SetInteger("AnimPar", 0); //stable
        }
        if (controller.isGrounded)
        {
            moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
        }

        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        controller.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;*/
	}

	void moveUp() {
		anim.SetInteger("AnimPar", 1); //moving
		controller.transform.position += (Vector3.forward * Time.deltaTime * speed) * speedUpDown;
		//smoke.transform.Rotate (0,0,0);
	}

	void moveDown() {
		anim.SetInteger("AnimPar", 1); //moving
		controller.transform.position += (Vector3.back * Time.deltaTime * speed) * speedUpDown;
		//smoke.transform.Rotate (180,0,0);
	}

	void moveRight() {
		anim.SetInteger("AnimPar", 1); //moving
		controller.transform.position += Vector3.right * Time.deltaTime * speed;
		//smoke.transform.Rotate (0,90,0);
	}

	void moveLeft() {
		anim.SetInteger("AnimPar", 1); //moving
		controller.transform.position += Vector3.left * Time.deltaTime * speed;
		//smoke.transform.Rotate (Vector3.left * Time.deltaTime);
	}
}
