using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

    public void ChangeScence(string scenename)
    {
        Application.LoadLevel(scenename);
    }
}
