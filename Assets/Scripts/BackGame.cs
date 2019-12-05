using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGame : MonoBehaviour {
    public string levelToLoad;                  // 下一個Area ( Scene )

    public string exitPoint;                    // 此離開的點的名稱 ( 建立進出兩點的連接

    private PlayerController thePlayer;

    // Use this for initialization
    public void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScence()
    {           // 碰到該格 ( 觸發碰撞的物件 )

        GameObject.Find("Player").SetActive(true);
        GameObject.Find("Main Camera").SetActive(true);
        GameObject.Find("Canvas").SetActive(true);
        // Application.LoadLevel(levelToLoad);      // 切換到下一個Area ; 該函數已過時雖還能用
        SceneManager.LoadScene(levelToLoad);        // Microsoft Visual Studio 推薦使用函數( using UnityEngine.SceneManagement
        thePlayer.startPoint = exitPoint;           // 根據exit point給定切換Area的start point ( startPoint 可以很多個
    }
}
