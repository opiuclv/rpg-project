using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

    public int playerMaxHealth;
    public int playerCurrentHealth;

    private bool flashActive;
    public float flashLengh;
    private float flashCounter;


    private SpriteRenderer playerSprite;

    private SFXManager sfxMan;

    public string levelToLoad;
    private Camera thecCamara;


    // Use this for initialization
    void Start () {
        playerCurrentHealth = playerMaxHealth;
        sfxMan = FindObjectOfType<SFXManager>();

        playerSprite = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        if(playerCurrentHealth <= 0)
        {
            //sfxMan.playerDead.play();
            gameObject.SetActive(false);
            Destroy(GameObject.Find("Main Camera"));
            Destroy(GameObject.Find("Canvas")); // 把所有原本don't destroy的東西死掉後都destroy
            Application.LoadLevel(levelToLoad); // load到死亡畫面


        }	

        if(flashActive)
        {
            if(flashCounter > flashLengh * .66f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.3f); // RGB f
            }
            else if( flashCounter > flashLengh * .33f )
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f); // RGB f f為透明度 值越小越看不到
            }
            else if (flashCounter > 0f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.3f); // RGB f
            }
            else
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f); // RGB f
                flashActive = false;
            }

            flashCounter -= Time.deltaTime;
        }
	}

    public void HurtPlayer(int damageToGive)
    {
        playerCurrentHealth -= damageToGive;

        flashActive = true;
        flashCounter = flashLengh;

        sfxMan.playerHurt.Play();

    }

    public void SetMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }
}
