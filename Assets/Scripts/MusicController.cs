using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    public static bool mcExists;

    public AudioSource[] musicTracks;

    public int currentTrack;

    public bool musicCanPlay;

	// Use this for initialization
	void Start () {
        //musicTracks = GetComponent<AudioSource>();

        if (!mcExists)
        {
            mcExists = true;
            DontDestroyOnLoad(transform.gameObject); //控制角色loadNewArea後的位置
        }
        else
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(musicCanPlay)
        {
            if(!musicTracks[currentTrack].isPlaying)
            {
                musicTracks[currentTrack].Play();
            }
        }
        else{
            musicTracks[currentTrack].Stop();
        }
	}

    public void SwitchTrack(int newTrack)
    {
        musicTracks[currentTrack].Stop();
        currentTrack = newTrack;
        musicTracks[currentTrack].Play();
    }
}
