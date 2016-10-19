using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class newLevel : MonoBehaviour {

	// Use this for initialization
	public void loadLevel (string name) {
        SceneManager.LoadScene(name);
    }
	
}
