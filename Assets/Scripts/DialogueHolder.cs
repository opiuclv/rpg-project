using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 對話框持有者 + 判斷觸發對話 ( 村民

public class DialogueHolder : MonoBehaviour {

    public string dialogue;
    private DialogueManager theDM;

    public string[] dialogueLines;

    // Use this for initialization
    void Start () {
        theDM = FindObjectOfType<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerStay2D(Collider2D other)                          // 待在村民附近 ( 待更新
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space))                    // 按下空白鍵 , 顯示對話框
            {
                // theDM.ShowBox(dialogue);

                if (!theDM.dialogueActive)                          // 尚未開啟對話框時
                {
                    theDM.dialogueLines = dialogueLines;
                    theDM.currentLine = 0;
                    theDM.ShowDialogue();
                }

                if (transform.parent.GetComponent<VillagerMovement>() != null)
                {
                    transform.parent.GetComponent<VillagerMovement>().canMove = false;
                }
            }
        }
    }
}
