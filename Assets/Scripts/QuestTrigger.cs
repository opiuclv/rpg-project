using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 任務觸發 ( location 定點觸發 )

public class QuestTrigger : MonoBehaviour {

    private QuestManager theQM;

    public int questNumber;                         // 任務編號

    public bool startQuest;                         // 開始點 & 結束點
    public bool endQuest;

	// Use this for initialization
	void Start () {
        theQM = FindObjectOfType<QuestManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")          // 是玩家 + 任務尚未完成
        {
            if (!theQM.questCompleted[questNumber])
            {
                if (startQuest && !theQM.quests[questNumber].gameObject.activeSelf)     // 任務開始點 + 任務尚未啟動
                {
                    theQM.quests[questNumber].gameObject.SetActive(true);
                    theQM.quests[questNumber].StartQuest();
                }

                if (endQuest && theQM.quests[questNumber].gameObject.activeSelf)        // 任務結束點 + 任務已經啟動
                {
                    theQM.quests[questNumber].EndQuest();
                }
            }
        }
    }
}
