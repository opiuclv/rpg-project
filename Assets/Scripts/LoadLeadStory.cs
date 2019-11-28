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



    /// <summary>
    /// 間隔時間
    /// </summary>
    private float letterPause = 0.1f;

    public AudioClip clip;

    private AudioSource source;
    /// <summary>
    /// 暫存中間值
    /// </summary>
    private string word;
    /// <summary>
    /// 要顯示的內容
    /// </summary>
    private string text;

    public bool ok_to_Enter = false ; // 跑完整段文字才能繼續下一行 不然會有錯誤

 

    // Use this for initialization
    void Start () {
        dialogueLines = new string[1];  // 不給的話會在執行第47行的時候出現IndexOutOfRangeException的Error 因為沒給空間他會亂亂抓東西
        dialogueActive = true;
        currentLine = 1 ;
        dText.text = ReadFile("Assets\\Text\\Lead_story.txt", currentLine);

        text = dText.text ;
        source = GetComponent<AudioSource>();
        word = text;
        text = "";
        StartCoroutine(TypeText());
        ok_to_Enter = true ;
    }
	
	// Update is called once per frame
	void Update () {
        
        if ( currentLine == 1 ) currentLine ++ ;

        if ( dialogueActive && currentLine == totalLine && Input.GetKeyDown(KeyCode.Space) ) // 放在這是因為如果超過可讀取行數會當掉，所以先load並初始化
        {
            currentLine = 1 ;
            dialogueActive = false;
            SceneManager.LoadScene(levelToLoad);        // Microsoft Visual Studio 推薦使用函數( using UnityEngine.SceneManagement
        }
        if ( dialogueActive && currentLine <= totalLine ) // 可以讀的行數才讀
        {
            dText.text = ReadFile("Assets\\Text\\Lead_story.txt", currentLine); // 目前只有找到利用絕對路徑來存取的方法，希望能改成相對路徑(同一資料夾讀取)
        }

		if (dialogueActive && Input.GetKeyDown(KeyCode.Space) && ok_to_Enter == true )              // 繼續對話
        {
            currentLine++;
            text = dText.text ;
            word = text;
            text = "";
            StartCoroutine(TypeText());
            ok_to_Enter = true ;
        }

    }

    public void ShowDialogue()                                              // 顯示對話框
    {
        dialogueActive = true;
    }

    void OnGUI()
    {
        GUIStyle style =new GUIStyle();
        style.normal.background = null;    //这是设置背景填充的
        style.normal.textColor= Color.white;   //设置字体颜色的
        style.fontSize = 20;       //当然，这是字体大小
        style.wordWrap = true ;
        GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 150, 200, 200), "序章", style); // 使用GUI函數來固定text顯示位置
        GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 300), text, style);
        GUI.Label(new Rect(Screen.width / 2 + 200, Screen.height / 2 + 100, 300, 200), "Press Space to continue...", style);
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
    /// <summary>
    /// 打字機效果
    /// </summary>
    /// <returns></returns>
    private IEnumerator TypeText()
    {
        foreach (char letter in word.ToCharArray())
        {
            text += letter;
            ok_to_Enter = false ;
            if(clip )
            {
                source.PlayOneShot(clip);
            }
            yield return new WaitForSeconds(letterPause);
        }
        ok_to_Enter = true ; // 這句跑完了之後才能換下一行
    }
}
