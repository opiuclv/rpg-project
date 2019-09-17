using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 管理音量

public class VolumeManager : MonoBehaviour {

    public VolumeController[] vcObjects;

    public float maxVolumeLevel = 1.0f;
    public float currentVolumeLevel;

	// Use this for initialization
	void Start () {
        vcObjects = FindObjectsOfType<VolumeController>();

        if (currentVolumeLevel > maxVolumeLevel)
        {
            currentVolumeLevel = maxVolumeLevel;
        }

        for (int i = 0; i < vcObjects.Length; i++)
        {
            vcObjects[i].SetAudioLevel(currentVolumeLevel);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
