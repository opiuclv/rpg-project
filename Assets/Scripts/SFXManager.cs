using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    public AudioSource playerHurt;
    //public AudioSource playerDead;
    public AudioSource playerAttack;

    private static bool sfxmanExists;


	// Use this for initialization
	void Start () {
        if (!sfxmanExists)
        {
            sfxmanExists = true;
            DontDestroyOnLoad(transform.gameObject); //控制角色loadNewArea後的位置
        }
        else
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
