using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smap_camera_Controller : MonoBehaviour {

    private Camera theCamera;               // Camera的資料 & 螢幕顯示長寬 ( Screen )

	// Use this for initialization
	void Start () {
		theCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName == "main") //在房間裡不能開啟小地圖
        {
            transform.position = new Vector3(40, -30, -45.5f); // 經過計算請勿隨意更改
	    }
        if (Application.loadedLevelName == "city") //在房間裡不能開啟小地圖
        {
            transform.position = new Vector3(20, -6, -33); // 經過計算請勿隨意更改
	    }
        if (Application.loadedLevelName == "suburbs") //在房間裡不能開啟小地圖
        {
            transform.position = new Vector3(15, -15, -25.5f); // 經過計算請勿隨意更改
	    }
        if (Application.loadedLevelName == "village") //在房間裡不能開啟小地圖
        {
            transform.position = new Vector3(29, -19.5f, -32.5f); // 經過計算請勿隨意更改
	    }
    }
}
