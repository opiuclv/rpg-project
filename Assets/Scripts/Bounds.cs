using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Load Scene 時 給Camera邊界

public class Bounds : MonoBehaviour {

    private BoxCollider2D bounds;
    private CameraController theCamera;

	// Use this for initialization
	void Start () {
        bounds = GetComponent<BoxCollider2D>();
        theCamera = FindObjectOfType<CameraController>();
        theCamera.SetBounds(bounds);
	}
}
