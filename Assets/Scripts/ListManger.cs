using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ListManger : MonoBehaviour {

    //sticky note image source:http://images.clipartbro.com/32/sticky-note-png-32848.png

    public GameObject _food1;
    public Text _foodName1;

    // Use this for initialization
    void Start () {
        _foodName1.text = _food1.gameObject.transform.name;
    }
	
	// Update is called once per frame
	void Update () {
        if (!_food1.active)
            _foodName1.color = Color.green;
	}
}
