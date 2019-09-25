using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 管理玩家血量 + 受傷動畫

public class PlayerHealthManager : MonoBehaviour {

    public int playerMaxHealth;
    public int playerCurrentHealth;

    private bool flashActive;               // 是否要顯示受傷動畫
    public float flashLength;               // 長度 & counter
    private float flashCounter;

    public GameObject panelGameOver;

    private SpriteRenderer playerSprite;
    
    private SFXMnager theSFXM;
    public string levelToLoad;
    private MusicControler theMusicControler;

    // Use this for initialization
    void Start () {
        playerCurrentHealth = playerMaxHealth;
        theSFXM = FindObjectOfType<SFXMnager>();
        theMusicControler = FindObjectOfType<MusicControler>();

        playerSprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerCurrentHealth <= 0)           // 玩家死亡
        {
            theMusicControler.musicCanPlay = false;
            theSFXM.playerDead.Play();
            gameObject.SetActive(false);
            Destroy(GameObject.Find("Main Camera"));
            Destroy(GameObject.Find("Canvas")); // 把所有原本don't destroy的東西死掉後都destroy
            Application.LoadLevel(levelToLoad); // load到死亡畫面
            // GetComponent<PlayerController>().canMove = false;
            // playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);

            // panelGameOver.SetActive(true);
        }

        if (flashActive)                        // 玩家受傷動畫
        {
            if (flashCounter > flashLength * 0.66f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 0.33f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > 0f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                flashActive = false;
            }

            flashCounter -= Time.deltaTime;
        }
	}

    public void HurtPlayer(int damageToGive)        // 玩家受傷
    {
        playerCurrentHealth -= damageToGive;

        flashActive = true;
        flashCounter = flashLength;

        theSFXM.playerHurt.Play();
    }

    public void SetMaxHealth()                      // 使滿血
    {
        playerCurrentHealth = playerMaxHealth;
    }
}
