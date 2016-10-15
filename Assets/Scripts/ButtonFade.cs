using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonFade : MonoBehaviour {
	// Use this for initialization
	void Start () {
        this.GetComponent<Button>().onClick.AddListener(clicked);
    }
	
	// Update is called once per frame
	void clicked () {
        Debug.Log("You have clicked the button!");
    }
}
