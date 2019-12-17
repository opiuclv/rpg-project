using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeController : MonoBehaviour {

    public float moveSpeed;

    private Rigidbody2D myRigidbody;

    private bool moving;                    // 是否移動中

    public float timeBetweenMove;           // 移動間隔時間 & counter
    private float timeBetweenMoveCounter;
    public float timeToMove;                // 移動時間 & counter
    private float timeToMoveCounter;

    private Vector3 moveDirection;          // 移動方向

    public float waitToReload;              // 等待復活時間
    private bool reloading;
    private GameObject thePlayer;


	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();

        // timeBetweenMoveCounter = timeBetweenMove;
        // timeToMoveCounter = timeToMove;

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);    // counter 的 range
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
        
    }
	
	// Update is called once per frame
	void Update () {
		
        if (moving)             // 移動中
        {
            timeToMoveCounter -= Time.deltaTime;        // counter逐個frame慢慢減
            myRigidbody.velocity = moveDirection;
            if (timeToMoveCounter < 0f)
            {
                moving = false;
                // timeBetweenMoveCounter = timeBetweenMove;
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
            }
        }
        else                    // 移動間的間隔 ( 停住 )
        {
            timeBetweenMoveCounter -= Time.deltaTime;
            myRigidbody.velocity = Vector2.zero;        // 速度歸零 ( 馬上停住
            if ( timeBetweenMoveCounter < 0f )
            {
                moving = true;
                // timeToMoveCounter = timeToMove;
                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
                // 設定移動方向 * 速度 ( 隨機移動
            }
        }

        /* if (reloading)                          // 復活中
        {
            waitToReload -= Time.deltaTime;     // 等時間結束
            if ( waitToReload < 0f)
            {
                // Application.LoadLevel(Application.loadedLevel);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   // 重新回到上一個地圖
                thePlayer.SetActive(true);      // 復活玩家 ( player active set true
            }
        } */
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        /* if (other.gameObject.name == "Player")   // 碰裝物件為玩家
        {
            // Destroy(other.gameObject);           // 殺掉玩家 ( delete player
            other.gameObject.SetActive(false);      // 殺掉玩家 ( player active set false
            reloading = true;
            thePlayer = other.gameObject;           // 抓住玩家資料 ( point to player

        } */
        
    }
}
