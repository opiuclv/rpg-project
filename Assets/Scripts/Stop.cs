using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour {

    public GameObject pause;
    CanvasGroup canvas_g; // 宣告canvas group方便管理
	// Use this for initialization

    private void Awake()
    {
        pause = GameObject.Find("Pause");
        canvas_g = pause.GetComponent<CanvasGroup>();
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for_stop() ;
	}
    public void for_stop() {
        if ( Input.GetKeyUp(KeyCode.P)){
            if ( Time.timeScale == 1 ) {
                PauseGame();
            }
            else{
                ResumeGame() ;
            }
        }
    }
    void PauseGame(){
        Time.timeScale = 0 ;
        canvas_g.alpha = 1 ;
    }
    void ResumeGame(){
        Time.timeScale = 1 ;
        canvas_g.alpha = 0 ;
    }
}
