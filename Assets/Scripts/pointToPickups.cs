using UnityEngine;
using System.Collections;

public class pointToPickups : MonoBehaviour {

    
    public GameObject target1;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        if (target1.active) 
            this.transform.LookAt(target1.transform.position);
        else
            this.gameObject.SetActive(false);

        
    }
}
