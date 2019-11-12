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
		if (Application.loadedLevelName == "main") 
        {
            transform.position = new Vector3(40, -31, -46); // 經過計算請勿隨意更改 (9, 6.5, -46)
	    }
        if (Application.loadedLevelName == "suburbs") 
        {
            transform.position = new Vector3(20, -25, -33); // 經過計算請勿隨意更改 (20, -6, -33)
	    }
        if (Application.loadedLevelName == "village") 
        {
            transform.position = new Vector3(30, -18, -33); // 經過計算請勿隨意更改 (20, -6, -33)
	    }
    }
}
