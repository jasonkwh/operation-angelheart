using UnityEngine;
using System.Collections;

public class Rabbit : MonoBehaviour {

    public GameObject target;
    public GameObject Player;

    private Animator anim;
    private Animation ani;
    private bool sawPlayer = false;
    private bool inHole = false;
    public float moveSpeed = 10.0f;

    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (sawPlayer) {
            anim.SetInteger("RabbitAni", 1);
            transform.LookAt(target.transform);
            moveForward();
        }
        else { transform.LookAt(Player.transform); }


        if (inHole)
        {
            Debug.Log("inhole");
            this.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider Other)
    {
        switch (Other.tag)
        {
            case "Player":
                sawPlayer = true;
                break;
            case "FleeTarget":
                Debug.Log("got to hole");
                inHole = true;
                break;
        }

    }

    void moveForward()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }


}
