using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 切換音樂 ( 進入後觸發 or 踩點觸發 )

public class MusicSwitcher : MonoBehaviour {

    private MusicControler theMC;

    public int newTrack;

    public bool switchOnStart;

	// Use this for initialization
	void Start () {
        theMC = FindObjectOfType<MusicControler>();

        if (switchOnStart)                          // 一進入這個Scene就要切
        {
            theMC.SwitchTrack(newTrack);
            gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)         // 踩到某個地點才切
    {
        if (other.gameObject.tag == "Player")      
        {
            theMC.SwitchTrack(newTrack);            // 觸發後切音樂就關掉這個collision 這樣就不會一直重複觸發
            gameObject.SetActive(false);
        }
    }
}
