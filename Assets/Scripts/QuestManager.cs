using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 管理任務系統 + 印出任務開始及完成文字

public class QuestManager : MonoBehaviour {

    public QuestObject[] quests;                                // 任務清單
    public bool[] questCompleted;                               // 是否完成

    public DialogueManager theDM;

    public string itemCollected;                                // 收集的任務物品

    public string enemyKilled;                                  // 擊殺的怪物

	// Use this for initialization
	void Start () {
        questCompleted = new bool[quests.Length];               // 給定任務總數
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowQuestText(string questText)
    {
        theDM.dialogueLines = new string[1];                    // 給定string空間 印出文字
        theDM.dialogueLines[0] = questText;

        theDM.currentLine = 0;
        theDM.ShowDialogue();
    }
}
