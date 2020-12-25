using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject Player;
    public float viewY = 0.7f;
    public float viewZ = 3.0f;
    // public float rotateSpeed = 2.0f;

    // private GameObject mainCamera;
    // private GameObject playerCamera;
    //public Vector3 CameraDistance;

    // Start is called before the first frame update
    void Start()
    {
         //mainCamera = Camera.main.gameObject;
        // playerCamera = GameObject.Find("TestPlayer");
        ////プレイヤーキャラクターの情報を取得
        //this.Player = GameObject.Find("TestPlayer");

        ////メインカメラとプレイヤーの相対距離を求める
        //CameraDistance = transform.position - Player.transform.position;

    }

    //void Update()
    //{
    //    rotateCamera();
    //}

    //private void rotateCamera()
    //{
        
    //}

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 CameraScale = transform.localScale;
        transform.localScale = CameraScale;

        //トランスフォームの値を新しく代入
        transform.position = new Vector3 (Player.transform.position.x, Player.transform.position.y + viewY, Player.transform.position.z - viewZ);
    }
}
