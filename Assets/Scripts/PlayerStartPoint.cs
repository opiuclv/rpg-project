using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 玩家起始點設定

public class PlayerStartPoint : MonoBehaviour {

    private PlayerController thePlayer;     // playercontroller變數
    private CameraController theCamera;     // cameracontroller變數

    public Vector2 startDirection;          // 起始面向

    public string pointName;                // 此起始點的名稱

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerController>();           // 抓playercontroller變數

        if (thePlayer.startPoint == pointName)                      // 是否要在此起始點
        {
            thePlayer.transform.position = transform.position;      // 設定player座標
            thePlayer.lastMove = startDirection;                    // 設定player面向

            theCamera = FindObjectOfType<CameraController>();       // 抓cameracontroller變數
            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
        }                                                           // 設定camera座標
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
