using UnityEngine;
using System.Collections;

public class PatrollingAI : MonoBehaviour {

    //waypoint System
    public Transform[] Waypoints;
    public float Speed;
    public int curWayPoint;
    public bool doPatrol = true;
    public Vector3 Target;
    public Vector3 MoveDirection;
    public Vector3 Velocity;



    // Use this for initialization
    void Start () {
    }
	

	// Update is called once per frame
	void Update () {
	    if(curWayPoint < Waypoints.Length)
        {
            Target = Waypoints[curWayPoint].position;
            MoveDirection = Target - transform.position;
            Velocity = GetComponent<Rigidbody>().velocity;

            if (MoveDirection.magnitude < 1)
            {
                curWayPoint++;
            }
            else
                Velocity = MoveDirection.normalized * Speed;

        }
        else
        {
            if (doPatrol){
                curWayPoint = 0;
            }
            else
                Velocity = Vector3.zero;
        }
        GetComponent<Rigidbody>().velocity = Velocity;
        transform.LookAt(Target);
	}
}
