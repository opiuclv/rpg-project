using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour {

    public float charsPerSecond = 0.2f;//打字時間間隔
    private string words;//保存需要顯示的文字

    private float timer;//計時器
    private Text myText;
    private int currentPos=0;//當前打字位置

    private LoadLeadStory theLS;

    // Use this for initialization
    void Start () {
        theLS = FindObjectOfType<LoadLeadStory>();
        timer = 0;
        theLS.dialogueActive = true ;
        charsPerSecond = Mathf.Max (0.2f,charsPerSecond);
        myText = GetComponent<Text> ();
        words = theLS.dText.text;//獲取Text的文本信息，保存到words中，然後動態更新文本顯示內容，實現打字機的效果
    }

    // Update is called once per frame
    void Update () {
        OnStartWriter ();
        //Debug.Log (isActive);
    }

    public void StartEffect(){
        theLS.dialogueActive = true ;
    }
    /// <summary>
    /// 執行打字任務
    /// </summary>
    void OnStartWriter(){

        if(theLS.dialogueActive){
            timer += Time.deltaTime;
            if(timer>=charsPerSecond){//判斷計時器是否到達
                timer = 0;
                currentPos++;
                myText.text = myText.text + words.Substring (0,currentPos);//刷新文本顯示內容

                if(currentPos>=words.Length) {
                    OnFinish();
                }
            }

        }
    }
    /// <summary>
    /// 結束打字，初始化數據
    /// </summary>
    void OnFinish(){
        theLS.dialogueActive = false ;
        timer = 0;
        currentPos = 0;
        myText.text = words;
    }

}