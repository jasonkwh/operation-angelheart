using UnityEngine;
using System.Collections;

public class tutBox : MonoBehaviour {

    public GameObject textBox;

	// Use this for initialization
	void Start () {
        textBox.active = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textBox.active = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textBox.active = false;
        }
    }
}
