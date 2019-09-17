using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCanvasGroup : MonoBehaviour {
    CanvasGroup canvasGroup;
    // Use this for initialization
    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            if (canvasGroup.alpha == 1)
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
            else
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        }
    }
}
