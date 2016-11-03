using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class butterflyStatus : MonoBehaviour {

    public EnergyBar ebar;
    private Color white = Color.white;
    private Color clear = Color.clear;


    // Update is called once per frame
    void Update () {
	    if(ebar.valueCurrent >= 150)
        {
            this.gameObject.GetComponent<RawImage>().color = white;
        }
        else
        {
            this.gameObject.GetComponent<RawImage>().color = clear;
        }
	}
}
