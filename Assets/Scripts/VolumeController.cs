using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 控制音量 ( 各個音效 音樂

public class VolumeController : MonoBehaviour {

    private AudioSource theAudio;

    private float audioLevel;                       // 當前音量
    public float defaultAudio;                      // 預設最大音量

	// Use this for initialization
	void Start () {
        theAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetAudioLevel(float volume)         // 設定此音效(音樂)的音量
    {
        if (theAudio == null)
        {
            theAudio = GetComponent<AudioSource>();
        }

        audioLevel = defaultAudio * volume;
        theAudio.volume = audioLevel;
    }
}
