using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {


    public static bool isSoundOn = true;
    public static bool isMusicOn = true;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this);
	}

}
