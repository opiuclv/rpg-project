using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 控制Camera移動(跟隨player) + 處理Camera重疊問題 + 設定邊界

public class CameraController : MonoBehaviour {

    public float moveSpeed;
    public GameObject followTarget;         // 跟隨的目標

    private Vector3 targetPos;              // 目標座標

    private static bool cameraExists;       // 是否已存在相同camera ( avoid duplicate camera

    public BoxCollider2D boundBox;          // 邊界區域 & 最小端點 & 最大端點
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private Camera theCamera;               // Camera的資料 & 螢幕顯示長寬 ( Screen )
    private float halfHeight;
    private float halfWidth;

    // Use this for initialization
    void Start ()
    {
        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (boundBox == null)                                       // 若因某些原因導致沒抓到邊界 就重新再抓一次 ( 切換Scene時邊界先給原本的然後被殺掉 使當前的抓不到
        {
            boundBox = FindObjectOfType<Bounds>().GetComponent<BoxCollider2D>();
            minBounds = boundBox.bounds.min;
            maxBounds = boundBox.bounds.max;
        }

        minBounds = boundBox.bounds.min;                            // 抓邊界
        maxBounds = boundBox.bounds.max;

        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;                    // 抓 Camera 高度 & 寬度按顯示比例計算
        halfWidth = halfHeight * Screen.width / Screen.height;
    }
	
	// Update is called once per frame
	void Update () {
        targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        // 先抓目標座標, 改變自己座標 以線性方式改變( 當前座標, 目標座標, 改變速度 ) ; Time.deltaTime 以一個frame為單位的時間

        if (boundBox == null)
        {
            boundBox = FindObjectOfType<Bounds>().GetComponent<BoxCollider2D>();
            minBounds = boundBox.bounds.min;
            maxBounds = boundBox.bounds.max;
        }

        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        // 用 Clamp 使目標數值不超過給定邊界( 目標數值, 最大值, 最小值 )
    }

    public void SetBounds(BoxCollider2D newBounds)
    {
        boundBox = newBounds;

        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;


    }
}
