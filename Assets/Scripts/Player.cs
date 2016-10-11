using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

    private Animator anim;
    private Animation ani;
    private CharacterController controller;
    public float speed;
    public float Defspeed;
    public float speedUpDown;
    public float slowFactor;
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
	public float accelMidY;
	//private float backupTimeAccel;
	//private float timeAccelNegativeRate = 0;

	//control selection
	public bool touchControl = false;
	public bool motionControl = false;
	public bool bearCollider = false;

	//sound effects
	AudioSource audio;

	//control buttons
	private bool buttonUp = false;
	private bool buttonDown = false;
	private bool buttonRight = false;
	private bool buttonLeft = false;
    private bool buttonUpleft = false;
    private bool buttonUpright = false;
    private bool buttonDownLeft = false;
    private bool buttonDownRight = false;

    Camera cam;
	private bool cameraZoom = false;
	public float cameraOrig = 25f;
	public float cameraFinal = 30f;
	public float cameraSteps = 1f;

    //scoreing mechanics
    private int numOfPickUps;
    private int score;
    public Text pickupText;
    public Text scoreText;

    //timer mechanic 
    public float timer = 180.0f;
    public Text timerText;

    //end conditions
    public GameObject Gameover;
    public GameObject Win;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject menu;

    //cat ai test
    //public static bool isInCatSpace = false; 

    void Start () {
        anim = gameObject.GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
		eBar = GameObject.FindGameObjectWithTag("energyBar").GetComponent<EnergyBar>();
		audio = gameObject.GetComponent<AudioSource>();
		audio.time = 0.9f;
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		cam.orthographicSize = cameraOrig;
        //ani["potWalk"].speed = 2.0f;
        score = 0;
        numOfPickUps = 0;
        setPickupText();
        setScoreText();
        setTimerText();
        Gameover.SetActive(false);
        Win.SetActive(false);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        menu.SetActive(false);
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
				if(audio.isPlaying == false) {
					audio.Play();
				}
				if(bearCollider == false) {
					jumpUp ();
				}
				potJumpTranslate (pX, pZ);
			} else {
				transform.position = new Vector3 (transform.position.x, 0, transform.position.z); //back to original position
				bearCollider = false;
				pushing = false;
			}
		}
	}

    // colliding with other objects
    void OnTriggerEnter(Collider other)
    {
 
        if (other.gameObject.CompareTag("Pickups")) //colliding with pickups
        {
            other.gameObject.SetActive(false);
            numOfPickUps = numOfPickUps + 1;
            setPickupText();

            if (numOfPickUps > 0)
                speed -= slowFactor;
        }

        if (other.gameObject.CompareTag("Table")) //collding with Table
        {
            if (numOfPickUps <= 3)
                score += (numOfPickUps * 50) + (numOfPickUps * 10);
            else
            {
                score += (numOfPickUps * 50) + (numOfPickUps * 50 / 2);
            }
            numOfPickUps = 0;
            setPickupText();
            setScoreText();
            speed = Defspeed;

        }
        if (other.gameObject.CompareTag("Exit")) //collding with exit area
        {
            if(score >= 100) {  //score more than 100
                Win.SetActive(true);
                star1.SetActive(true);
                menu.SetActive(true);
                Time.timeScale = 0f;
            }

            if (score >= 250) //score more than 100
            {
                Win.SetActive(true);
                star1.SetActive(true);
                star2.SetActive(true);
                menu.SetActive(true);
                Time.timeScale = 0f;
            }

            if (score >= 400) //score more than 100
            {
                Win.SetActive(true);
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                menu.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        //if (other.gameObject.CompareTag("CatSpace1"))  //enters cat space
        //    isInCatSpace = true;
    }

    //void OnTriggerExit (Collider other)
    //{
    //    if (other.gameObject.CompareTag("CatSpace1")) //exits cat space
    //        isInCatSpace = false;
        
    //}

    void setPickupText() // update the text UI
    {
        pickupText.text = numOfPickUps.ToString(); 
    }

    void setScoreText() // update the text UI
    {
        scoreText.text = score.ToString();
    }

    void setTimerText()// update the text UI
    {
        timerText.text = Mathf.Round(timer).ToString();
    }

    //timer
    void countDownTime()
    {
        timer -= Time.deltaTime;
        setTimerText();
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
            Gameover.SetActive(true);
            menu.SetActive(true);
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

        //count down
        countDownTime();

        //things of player health...
        gainHealth();
		dead();

		//fade out control
		if(cameraZoom == true) {
			fadeOutCamera();
		}

		if (pushing == false) {
			smoke.SetActive (ate);
			if ((ate == true) && (water.transform.position.y <= 1.98)) {
				water.transform.position += Vector3.up * Time.deltaTime * waterLevelSpeed;
			}

			//button control
			if(buttonUp == true) {
				moveUp();
			}

            if (buttonUpleft == true)
            {
                moveUp();
                moveLeft();
            }

            if (buttonUpright == true)
            {
                moveUp();
                moveRight();
            }

            if (buttonDown == true) {
				moveDown();
			}

            if (buttonDownLeft == true)
            {
                moveDown();
                moveLeft();
            }

            if (buttonDownRight == true)
            {
                moveDown();
                moveRight();
            }

            if (buttonRight == true) {
				moveRight();
			}
			if(buttonLeft == true) {
				moveLeft();
			}

			//touch control
			if (Input.touchCount > 0) {
				//StartCoroutine (GenerateTrails (trailsWaitTime));
				if(touchControl == true) {
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
									moveRight();
									directionRight = true;
								} else {
									moveLeft();
									directionLeft = true;
								}
							}

							if (swipeType.y != 0.0f) {
								if (swipeType.y > 0.0f) {
									moveUp();
									directionUp = true;
								} else {
									moveDown();
									directionDown = true;
								}
							}
							//}
							break;

						case TouchPhase.Stationary:
							if (gestureDist > minSwipeDist)
							if (directionUp == true)
								moveUp();
							if (directionRight == true)
								moveRight();
							if (directionLeft == true)
								moveLeft();
							if (directionDown == true)
								moveDown();
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
			}

			//keyboard control
			else if (Input.touchCount == 0) {
				//StopCoroutine (GenerateTrails (trailsWaitTime));
				fadeInCamera();

				if(eBar.valueCurrent > 0) {
					anim.SetInteger("AnimPar", 0); //stable
				}

				if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
					cameraZoom = true;
					moveUp();
				}
				if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
					cameraZoom = true;
					moveDown();
				}
				if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
					cameraZoom = true;
					moveLeft();
				}
				if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
					cameraZoom = true;
					moveRight();
				}
			}

			//use phone's accelerometer
			if(motionControl == true) {
				if(Input.acceleration.y >= accelMidY) {
					movePot(new Vector3(Input.acceleration.x * accelSpeedModifier * Time.deltaTime * speed, 0, 1 * accelSpeedModifier * Time.deltaTime * speed));
				} else {
					movePot(new Vector3(Input.acceleration.x * accelSpeedModifier * Time.deltaTime * speed, 0, (Input.acceleration.y + accelMidY) * accelSpeedModifier * Time.deltaTime * speed));
				}

				//stable animation
				if((eBar.valueCurrent > 0) && (Input.acceleration.x == 0.0f) && (Input.acceleration.y == -0.5f)) {
					anim.SetInteger("AnimPar", 0); //stable
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
			cam.orthographicSize = cameraFinal;
			anim.SetInteger("AnimPar", 1); //moving
			controller.transform.position += movePosition;
			//smoke.transform.Rotate (0,0,0);
		}
	}

	void moveUp() {
		movePot((Vector3.forward * Time.deltaTime * speed) * speedUpDown);
    }

	void moveDown() {
		movePot((Vector3.back * Time.deltaTime * speed) * speedUpDown);
	}

	void moveRight() {
		movePot(Vector3.right * Time.deltaTime * speed);
	}

	void moveLeft() {
		movePot(Vector3.left * Time.deltaTime * speed);
	}

	public void enterUp() {
		buttonUp = true;
		cameraZoom = true;
        anim.SetInteger("AnimPar", 1);
    }

    public void enterUpLeft()
    {
        buttonUpleft = true;
        cameraZoom = true;
    }

    public void enterUpRight()
    {
        buttonUpright = true;
        cameraZoom = true;
    }


    public void enterDown() {
		buttonDown = true;
		cameraZoom = true;
	}

    public void enterDownLeft()
    {
        buttonDownLeft = true;
        cameraZoom = true;
    }

    public void enterDownRight()
    {
        buttonDownRight = true;
        cameraZoom = true;
    }

    public void enterRight() {
		buttonRight = true;
		cameraZoom = true;
	}

	public void enterLeft() {
		buttonLeft = true;
		cameraZoom = true;
	}

	public void exitUp() {
		buttonUp = false;
	}

    public void exitUpleft()
    {
        buttonUpleft = false;
    }

    public void exitUpright()
    {
        buttonUpright = false;
    }

    public void exitDown() {
		buttonDown = false;
	}

    public void exitDownLeft()
    {
        buttonDownLeft = false;
    }

    public void exitDownRight()
    {
        buttonDownRight = false;
    }

    public void exitRight() {
		buttonRight = false;
	}

	public void exitLeft() {
		buttonLeft = false;
	}

	void jumpUp() {
		jumpSpeed = maxJumpSpeed;
		if (jumpSpeed > 0) {
			jumpSpeed = maxJumpSpeed - (jumpSpeed * (time - backupTime) * jumpMultiplier);
		}

		transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime);
	}

	void fadeInCamera() {
		if(cam.orthographicSize > cameraOrig) {
			cam.orthographicSize -= cameraSteps;
		}
		cameraZoom = false;
	}

	void fadeOutCamera() {
		if(cam.orthographicSize < cameraFinal) {
			cam.orthographicSize += cameraSteps;
		}
	}
}
