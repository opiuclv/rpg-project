using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

// 管理對話框

public class DialogueManager : MonoBehaviour {

    public GameObject dBox;             // 對話框
    public Text dText;                  // 文字框

    public bool dialogueActive;         // 是否顯示對話框

    public string[] dialogueLines;      // 內容
    public int currentLine;

    private PlayerController thePlayer;

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
        
        dialogueLines = new string[1];  // 不給的話會在執行第47行的時候出現IndexOutOfRangeException的Error 因為沒給空間他會亂亂抓東西

    }
	
	// Update is called once per frame
	void Update () {
        
		if (dialogueActive && Input.GetKeyDown(KeyCode.Space))              // 繼續對話
        {
            // dBox.SetActive(false);
            // dialogActive = false;

            currentLine++;
        }

        if (dialogueActive && currentLine >= dialogueLines.Length)          // 關閉對話框
        {
            dBox.SetActive(false);
            dialogueActive = false;

            currentLine = 0;
            thePlayer.canMove = true;
        }

        // dText.text = dialogueLines[currentLine];                            // 顯示文字內容
        dText.text = ReadFile("C:\\Users\\User\\Desktop\\RPG_Data.txt", currentLine);
    }

    /* public void ShowBox(string dialogue)
    {
        dialogueActive = true;
        dBox.SetActive(true);
        dText.text = dialogue;
    } */

    public void ShowDialogue()                                              // 顯示對話框
    {
        dialogueActive = true;
        dBox.SetActive(true);
        thePlayer.canMove = false;
    }

    //按路徑讀取txt文本的内容，第一個参數是路徑名，第二個参數是第幾行，返回值是sring[]數組
    string ReadFile(string PathName, int linenumber)
    {
        string[] strs = File.ReadAllLines(PathName);//讀取txt文本的内容，返回sring数组的元素是每行内容
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
