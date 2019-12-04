using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Victory : MonoBehaviour
{
    private SFXMnager theSFXM;
    public string levelToLoad;
    private MusicControler theMusicControler;

    // Start is called before the first frame update
    void Start()
    {
        theSFXM = FindObjectOfType<SFXMnager>();
        theMusicControler = FindObjectOfType<MusicControler>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {           // 碰到該格 ( 觸發碰撞的物件 )
        theMusicControler.musicCanPlay = false;
        theSFXM.playerDead.Play();
        gameObject.SetActive(false);
        GameObject.Find("Player").SetActive(false) ;
        GameObject.Find("Main Camera").SetActive(false) ;
        GameObject.Find("Canvas").SetActive(false) ;
        //Destroy(GameObject.Find("Canvas")); // 把所有原本don't destroy的東西死掉後都destroy
        Application.LoadLevel(levelToLoad);
    }
}
