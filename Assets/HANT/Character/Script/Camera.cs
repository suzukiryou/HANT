using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CameraScale = transform.localScale;
        transform.localScale = CameraScale;

        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, 10);
        //if (transform.position.x < transform.position.z)
        //{
        //    transform.position = new Vector3(0, Player.transform.position.y, -10);
        //}
        //if (transform.position.y > 192)
        //{
        //    transform.position = new Vector3(transform.position.x, 192, -10);
        //}
    }
}

