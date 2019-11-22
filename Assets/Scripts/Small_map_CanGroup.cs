using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Small_map_CanGroup : MonoBehaviour {
    CanvasGroup canvasGroup;
    // Use this for initialization
    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    // Update is called once per frame
    void Update () {
        if (Application.loadedLevelName == "home_inside") //在房間裡不能開啟小地圖
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        else if (Application.loadedLevelName == "forest") //不能開啟小地圖
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        else if (Application.loadedLevelName == "Victory") //不能開啟小地圖
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        else if (Application.loadedLevelName == "Thanks") //不能開啟小地圖
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        else if ( Input.GetKeyDown(KeyCode.M) ) //可手動關閉小地圖
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
    /*
    void OnGUI(){
        if (Application.loadedLevelName == "main") // 到主地圖預設開啟小地圖，但使用OnGUI的話就無法手動關閉小地圖
        {
            if (canvasGroup.alpha == 0)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        }
    }
    */
}
