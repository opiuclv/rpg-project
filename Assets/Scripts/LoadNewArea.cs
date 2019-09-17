using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 進入另一個Area(Scene) + 建立進出兩點的連接

public class LoadNewArea : MonoBehaviour {

    public string levelToLoad;                  // 下一個Area ( Scene )

    public string exitPoint;                    // 此離開的點的名稱 ( 建立進出兩點的連接

    private PlayerController thePlayer;

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {           // 碰到該格 ( 觸發碰撞的物件 )
        if (other.gameObject.tag == "Player" )         // 該物件是player
        {
            // Application.LoadLevel(levelToLoad);      // 切換到下一個Area ; 該函數已過時雖還能用
            SceneManager.LoadScene(levelToLoad);        // Microsoft Visual Studio 推薦使用函數( using UnityEngine.SceneManagement
            thePlayer.startPoint = exitPoint;           // 根據exit point給定切換Area的start point ( startPoint 可以很多個
        }
    }
}
