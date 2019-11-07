using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 控制UI -- HP + Level + 處理UI重疊問題

public class UIManager : MonoBehaviour {

    public Slider healthBar;
    public Text HPText;
    public PlayerHealthManager playerHealth;

    private PlayerStats thePS;
    public Text LevelText;
    public Camera theCamera; // 小地圖的camera

    private static bool UIExists;

	// Use this for initialization
	void Start () {

        if (!UIExists)
        {                                               // 沒有相同player  ( bool預設為false
            UIExists = true;
            DontDestroyOnLoad(transform.gameObject);    // 切換畫面不改變gameObject參數
        }
        else
        {                                               // 已存在相同player
            Destroy(gameObject);                        // 刪除當前gameObject
        }

        thePS = GetComponent<PlayerStats>();
        theCamera = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        // 給定個個UI的值
        healthBar.maxValue = playerHealth.playerMaxHealth;
        healthBar.value = playerHealth.playerCurrentHealth;
        HPText.text = "HP: " + playerHealth.playerCurrentHealth + "/" + playerHealth.playerMaxHealth;
        LevelText.text = "Lvl: " + thePS.currentLevel;
	}

    void OnGUI(){
        if ( Application.loadedLevelName == "home_inside" ) // 換到別的地圖要改變小地圖camera的位置
        {
            //Debug.Log("HHH");
            //theCamera.transform.position =  new Vector3(50, 50, 40);
        }
    }
}
