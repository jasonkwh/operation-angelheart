using UnityEngine;
using System.Collections;

public class tutBox : MonoBehaviour {

    public GameObject textBox;

	// Use this for initialization
	void Start () {
        textBox.SetActive(false);
	}

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textBox.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textBox.SetActive(false);
        }
    }

}
