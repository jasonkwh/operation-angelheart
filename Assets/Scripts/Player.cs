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

	//variables for player ate any food...
	public bool ate = false;
	public GameObject smoke;
	public GameObject water;
	public float waterLevelSpeed;

	//variables for touch
	private bool directionUp = false;
	private bool directionRight = false;
	private bool directionDown = false;
	private bool directionLeft = false;
	private float minSwipeDist  = 10.0f;
	private float gestureDist = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;

	//for bouncing
	private float time;
	private float backupTime;
	public bool stopBackup;
	public float stayTime;
	public bool pushing;
	private float jumpSpeed = 0f;
	public float maxJumpSpeed;
	public float jumpMultiplier;
	public float pX;
	public float pZ;
	public float jumpSpaceMulti;
	private string fps;

	//for generating terrain
	//public bool generatedX1 = false;
	//public bool generatedX1key;
	//public bool generatedX2 = false;
	//public bool generatedX2key;
	//public bool generatedZ1 = false;

	//gain energy
	private EnergyBar eBar;
	public bool energyGain = false;
	public float energyGainSpeed;

	//to set the speed of accelerametor
	public float accelSpeedModifier;

    void Start () {
        anim = gameObject.GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
		eBar = GameObject.FindGameObjectWithTag("energyBar").GetComponent<EnergyBar>();
        //ani["potWalk"].speed = 2.0f;
    }

	//bouncing
	void FixedUpdate() {
		time += Time.deltaTime;

		if (pushing == true) {
			if (stopBackup == false) {
				backupTime = time;
				stopBackup = true;
			}
				
			if (time < (backupTime + (stayTime / 2))) {
				//anim.SetInteger("AnimPar", 3); //knock
				jumpUp ();
				potJumpTranslate (pX, pZ);
			} else {
				transform.position = new Vector3 (transform.position.x, 0, transform.position.z); //back to original position
				pushing = false;
			}
		}
	}

	void potJumpTranslate(float positionX, float positionZ) {
		transform.Translate (positionX * jumpSpaceMulti * Time.deltaTime, 0, positionZ * jumpSpaceMulti * Time.deltaTime);
	}

	void OnGUI() {
		fps = "FPS: " + (1 / Time.deltaTime); //to test fps on phone
		//fps = "Acceleration: " + Input.acceleration; //to test accelerator on phone
		GUI.contentColor = Color.black;
		GUI.Label(new Rect(10, 10, 400, 20), fps);
	}

	void gainHealth() {
		//gain some health
		if(energyGain == true) {
			if(eBar.valueCurrent < 100) {
				eBar.valueCurrent = eBar.valueCurrent + (int)(energyGainSpeed * Time.deltaTime);
			} else {
				eBar.valueCurrent = 100;
				energyGain = false;
			}
		}
	}

	void dead() {
		if(eBar.valueCurrent <= 0) {
			Destroy(water);
			anim.SetInteger("AnimPar", 4); //dead
		}
	}

	void Update () {
		//generatedX1key = false;
		//generatedX2key = false;

		//force garbage collection while frameCount is not 30+
		if (Time.frameCount % 30 == 0)
		{
			System.GC.Collect();
		}

		//things of player health...
		gainHealth();
		dead();

		if (pushing == false) {
			smoke.SetActive (ate);
			if ((ate == true) && (water.transform.position.y <= 1.98)) {
				water.transform.position += Vector3.up * Time.deltaTime * waterLevelSpeed;
			}

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
								movePot(Vector3.right * Time.deltaTime * speed);
								directionRight = true;
							} else {
								movePot(Vector3.left * Time.deltaTime * speed);
								directionLeft = true;
							}
						}

						if (swipeType.y != 0.0f) {
							if (swipeType.y > 0.0f) {
								movePot((Vector3.forward * Time.deltaTime * speed) * speedUpDown);
								directionUp = true;
							} else {
								movePot((Vector3.back * Time.deltaTime * speed) * speedUpDown);
								directionDown = true;
							}
						}
						//}
						break;

					case TouchPhase.Stationary:
						if (gestureDist > minSwipeDist)
						if (directionUp == true)
							movePot((Vector3.forward * Time.deltaTime * speed) * speedUpDown);
						if (directionRight == true)
							movePot(Vector3.right * Time.deltaTime * speed);
						if (directionLeft == true)
							movePot(Vector3.left * Time.deltaTime * speed);
						if (directionDown == true)
							movePot((Vector3.back * Time.deltaTime * speed) * speedUpDown);
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

				//use phone's accelerometer
				movePot(new Vector3(Input.acceleration.x * accelSpeedModifier * Time.deltaTime * speed, 0, Input.acceleration.y * accelSpeedModifier * Time.deltaTime * speed));

				//if(eBar.valueCurrent > 0) {
				if((eBar.valueCurrent > 0) && (Input.acceleration.x == 0.0f) && (Input.acceleration.y == 0.0f)) {
					anim.SetInteger("AnimPar", 0); //stable
				}

				if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
					movePot((Vector3.forward * Time.deltaTime * speed) * speedUpDown);
				}
				if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
					movePot((Vector3.back * Time.deltaTime * speed) * speedUpDown);
				}
				if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
					movePot(Vector3.left * Time.deltaTime * speed);
				}
				if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow))
				{
					movePot(Vector3.right * Time.deltaTime * speed);
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
	}

	void movePot(Vector3 movePosition) {
		if(eBar.valueCurrent > 0) {
			anim.SetInteger("AnimPar", 1); //moving
			controller.transform.position += movePosition;
			//smoke.transform.Rotate (0,0,0);
		}
	}

	void jumpUp() {
		//Come babe,
		/*if ((time - backupTime) < (stayTime / 2)) {
			//jumpSpeed = ((9.8f * (time - backupTime)) - (-maxJumpSpeed));
			jumpSpeed = Mathf.Sqrt ((2 * 9.8f) + Mathf.Pow(maxJumpSpeed, 2));
		}*/

		/*jumpSpeed = maxJumpSpeed;
		jumpTime = (stayTime / 2) / 5;
		origJumpTime = jumpTime;

		if (jumpTime < (stayTime / 2)) {
			jumpSpeed = maxJumpSpeed * jumpTime;
			jumpTime = jumpTime + origJumpTime;
		}*/

		jumpSpeed = maxJumpSpeed;
		if (jumpSpeed > 0) {
			jumpSpeed = maxJumpSpeed - (jumpSpeed * (time - backupTime) * jumpMultiplier);
		}

		transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime);
	}

	/*void jumpDown() {
		jumpSpeed = maxJumpSpeed;
		if (jumpSpeed > 0) {
			jumpSpeed = (jumpSpeed * (time - (backupTime + (stayTime / 2))));
		}
		transform.Translate(Vector3.down * jumpSpeed * Time.deltaTime);
	}*/
}
