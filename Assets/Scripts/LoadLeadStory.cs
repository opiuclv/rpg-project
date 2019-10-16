using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

// 管理對話框

public class LoadLeadStory : MonoBehaviour {

    public string levelToLoad;                  // 下一個Area ( Scene )
    //public GameObject dBox;             // 對話框
    public Text dText;                  // 文字框

    public bool dialogueActive;         // 是否顯示對話框

    public string[] dialogueLines;      // 內容
    public int currentLine;
    public int totalLine;

 

    // Use this for initialization
    void Start () {
        dialogueLines = new string[1];  // 不給的話會在執行第47行的時候出現IndexOutOfRangeException的Error 因為沒給空間他會亂亂抓東西
        dialogueActive = true;
        currentLine = 1 ;
        dText.text = ReadFile("C:\\Users\\User\\Desktop\\Lead_story.txt", currentLine);
    }
	
	// Update is called once per frame
	void Update () {
        
		if (dialogueActive && Input.GetKeyDown(KeyCode.Space))              // 繼續對話
        {
            currentLine++;
        }

        // if (dialogueActive && currentLine >= dialogueLines.Length)          // 關閉對話框
        if ( dialogueActive && currentLine >= totalLine && Input.GetKeyDown(KeyCode.Space) )          // 關閉對話框 結束前導劇情
        {
            if ( currentLine <= totalLine ) 
            {
                SceneManager.LoadScene(levelToLoad);        // Microsoft Visual Studio 推薦使用函數( using UnityEngine.SceneManagement
            }
        }

        dText.text = ReadFile("C:\\Users\\User\\Desktop\\Lead_story.txt", currentLine); // 目前只有找到利用絕對路徑來存取的方法，希望能改成相對路徑(同一資料夾讀取)
    }

    public void ShowDialogue()                                              // 顯示對話框
    {
        dialogueActive = true;
    }

    //按路徑讀取txt文本的内容，第一個参數是路徑名，第二個参數是第幾行，返回值是sring[]數組
    string ReadFile(string PathName, int linenumber)
    {
        string[] strs = File.ReadAllLines(PathName);//讀取txt文本的内容，返回sring数组的元素是每行内容
        totalLine = strs.Length + 1; // 把總共有幾行讀出來
        if (linenumber == 0)
        {
            return "";
        }
        else
        {
            return strs[linenumber - 1];   //返回第linenumber行内容
        }
    }
}
