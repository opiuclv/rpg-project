using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour {

    public VolumeController vcObjects;

    public float MaxVolumeLevel = 1.0f;
    public float currentVolumeLevel;

	// Use this for initialization
	void Start () {
        vcObjects = FindObjectOfType<VolumeController>();

        if(currentVolumeLevel > MaxVolumeLevel)
        {
            currentVolumeLevel = MaxVolumeLevel;
        }

        /*for (int i = 0; i < vcObjects.Length; i++)
        {
            Debug.Log("I'm on loop " + i);
            vcObjects[i].SetAudioLevel(currentVolumeLevel);
        }*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
