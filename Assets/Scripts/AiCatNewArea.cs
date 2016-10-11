using UnityEngine;
using System.Collections;

public class AiCatNewArea : AiDuck {

	//private float backupDist;
	public float jumpDist;
	private float jumpSpeed = 1.0f;
	public float maxJumpSpeed;
	public float jumpAccelerate;
    public bool inArea = false;

	protected override void FixedUpdate() {
		time += Time.deltaTime;
		dist = Vector3.Distance (potTransform.position, transform.position);

		if(dist < maxDist && inArea) { //sight range
            duckRotate();

			if (potTransform.GetComponent<Player> ().pushing == false) {
				if (stopRanTime == false) {
						backupTime = time;
						randomTime = ranTime (minRandomTime, maxRandomTime);
						//backupDist = dist;
						stopRanTime = true;
				}
				if (time < (backupTime + randomTime)) {
					if((dist >= jumpDist) && (dist <= maxDist)) {
						anim.SetInteger("CatState", 3); //jump
						catJump();
					} else {
						anim.SetInteger("CatState", 1); //run
						speedAcceleration ();
					}
				} else if (time > (backupTime + randomTime)) {
					moveSpeed = 1.0f;
					jumpSpeed = 1.0f;
					stopRanTime = false;
				}
			}
		} else {
			anim.SetInteger("CatState", 0); //stay
		}

        if (dist < bounceRange)
        {
            anim.SetInteger("CatState", 2); //attack
            potTransform.GetComponent<Player>().pX = potTransform.position.x - transform.position.x;
            potTransform.GetComponent<Player>().pZ = potTransform.position.z - transform.position.z;
            potTransform.GetComponent<Player>().pushing = true;
            potTransform.GetComponent<Player>().stopBackup = false;
            eBar.valueCurrent = eBar.valueCurrent - damage; //set energy bar value
            backupTime = time;
        }

        else
        {
            //LookAt(StayNear);
        }
	}

	private void catJump() {
		if (jumpSpeed < maxJumpSpeed) {
			jumpSpeed = jumpSpeed + jumpAccelerate;
		}
		transform.position += transform.forward * jumpSpeed * Time.deltaTime;
	}

    public void setInArea( bool setter)
    {
        inArea = setter;
    }

    //private void LookAt(GameObject other)
    //{
    //    transform.LookAt(other.transform);
    //}
}
