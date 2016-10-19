using UnityEngine;
using System.Collections;

public class PointToExit : MonoBehaviour
{
    private Player player;
    public GameObject exit;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update () {
        if (player.TotalPickups >= 4)
        {
            transform.LookAt(exit.transform.position);
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
      
    }
}
