using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 任務本體 + 設定開始及結束動作 + 判斷任務物品是否已獲得

public class QuestObject : MonoBehaviour {

    public int questNumber;

    public QuestManager theQM;

    public string startText;
    public string endText;

    public bool isItemQuest;
    public string targetItem;

    public bool isEnemyQuest;
    public string targetEnemy;
    public int enemiesToKill;
    private int enemyKillCount;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isItemQuest)                            // 有收集物品任務
        {
            if(theQM.itemCollected == targetItem)   // 確定是指定物品
            {
                theQM.itemCollected = null;
                EndQuest();
            }
        }

        if (isEnemyQuest)                           // 有擊殺怪物任務
        {
            if (theQM.enemyKilled == targetEnemy)   // 確定是指定怪物
            {
                theQM.enemyKilled = null;
                enemyKillCount++;
            }

            if (enemyKillCount >= enemiesToKill)
            {
                EndQuest();
            }
        }
	}

    public void StartQuest()                        // 開始任務
    {
        theQM.ShowQuestText(startText);
        gameObject.SetActive(true);
    }

    public void EndQuest()                          // 結束任務
    {
        theQM.ShowQuestText(endText);
        theQM.questCompleted[questNumber] = true;
        gameObject.SetActive(false);
    }
}
