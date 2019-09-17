using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 管理音樂

public class MusicControler : MonoBehaviour {

    // public static bool mcExists;

    public AudioSource[] musicTracks;                       // 每一首音樂

    public int currentTrack;                                // 當前音樂

    public bool musicCanPlay;

	// Use this for initialization
	void Start () {

        /* if (!mcExists)
        {
            mcExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        } */
    }
	
	// Update is called once per frame
	void Update () {
		if (musicCanPlay)                                   // 要放音樂
        {
            if (!musicTracks[currentTrack].isPlaying)
            {
                musicTracks[currentTrack].Play();
            }
        }
        else                                                // 不放音樂
        {
            musicTracks[currentTrack].Stop();
        }
	}

    public void SwitchTrack(int newTrack)                   // 切換音樂
    {
        musicTracks[currentTrack].Stop();
        currentTrack = newTrack;
        musicTracks[currentTrack].Play();
    }
}
