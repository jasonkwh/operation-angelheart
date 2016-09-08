using UnityEngine;
using System.Collections;

public class RenderTerrain : MonoBehaviour {

	private GameObject pot;
	private int parameterX;
	private int parameterZ;
	private GameObject terrain10;
	private GameObject terrain10n;
	private GameObject terrain01;
	private GameObject terrain01n;
	//private float dist;
	//private float maxDist = 200f;

	// Use this for initialization
	void Start () {
		pot = GameObject.FindGameObjectWithTag ("Player");
		string[] terrainName = transform.name.Split ('_');
		parameterX = int.Parse(terrainName [1]);
		parameterZ = int.Parse(terrainName [2]);
	}
	
	// Update is called once per frame
	void Update () {
		//dist = Vector3.Distance (pot.transform.position, transform.position);

		//Instantiate new terrains
		if (pot.transform.position.x > transform.position.x) {
			terrainRender(terrain10, ("terrain_" + (parameterX + 1).ToString () + "_" + parameterZ.ToString ()), new Vector3 ((transform.position.x + transform.localScale.x), 0, transform.position.z));
		}
		if (pot.transform.position.x < transform.position.x) {
			terrainRender(terrain10n, ("terrain_" + (parameterX - 1).ToString () + "_" + parameterZ.ToString ()), new Vector3 ((transform.position.x - transform.localScale.x), 0, transform.position.z));
		}
		if (pot.transform.position.z > transform.position.z) {
			terrainRender(terrain01, ("terrain_" + parameterX.ToString () + "_" + (parameterZ + 1).ToString ()), new Vector3 (transform.position.x, 0, (transform.position.z + transform.localScale.z)));
		}
		if (pot.transform.position.z < transform.position.z) {
			terrainRender(terrain01n, ("terrain_" + parameterX.ToString () + "_" + (parameterZ - 1).ToString ()), new Vector3 (transform.position.x, 0, (transform.position.z - transform.localScale.z)));
		}

		//Destroy out of distance
		/*if (dist > maxDist) {
			Destroy (this.gameObject);
		}*/
	}

	void terrainRender(GameObject terrain, string terrainName, Vector3 terrainPosition) {
		if (GameObject.Find (terrainName) == null) {
			terrain = (GameObject)Instantiate (Resources.Load(terrainName), terrainPosition, transform.rotation);
			terrain.name = terrainName;
		}
	}
}
