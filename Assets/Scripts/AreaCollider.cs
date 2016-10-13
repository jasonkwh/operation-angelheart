using UnityEngine;
using System.Collections;

public class AreaCollider : MonoBehaviour {

    public AiCatNewArea cat;
    public bool potInArea;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        cat.setInArea(potInArea);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            potInArea = true;
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            potInArea = false;

    }


}
