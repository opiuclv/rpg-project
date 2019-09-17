using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 管理音效 ( 且不用每次都要重新跑路徑抓音效出來用

public class SFXMnager : MonoBehaviour {

    public AudioSource playerHurt;          // 音效
    public AudioSource playerDead;
    public AudioSource playerAttack;

    // private static bool sfxManExists;


    // Use this for initialization
    void Start () {

        /* if (!sfxManExists)
        {
            sfxManExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        } */
    }
}
