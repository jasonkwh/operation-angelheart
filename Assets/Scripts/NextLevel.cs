using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player") {
			SceneManager.LoadScene("Main", LoadSceneMode.Single);
		}
	}
}
