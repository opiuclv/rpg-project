using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 管理音效 + 音樂 + 音量 ( 自己加的

public class AudioManager : MonoBehaviour {

    public static bool amExists;

    // Use this for initialization
    void Start () {
        if (!amExists)
        {
            amExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
