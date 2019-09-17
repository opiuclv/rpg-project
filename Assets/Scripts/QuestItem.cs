using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 任務觸發 ( Item )

public class QuestItem : MonoBehaviour {

    public int questNumber;

    private QuestManager theQM;

    public string itemName;

	// Use this for initialization
	void Start () {
        theQM = FindObjectOfType<QuestManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")          // 是玩家 + 任務尚未完成 + 任務已經啟動
        {
            if (!theQM.questCompleted[questNumber] && theQM.quests[questNumber].gameObject.activeSelf)
            {
                theQM.itemCollected = itemName;
                gameObject.SetActive(false);
            }
        }
    }
}
