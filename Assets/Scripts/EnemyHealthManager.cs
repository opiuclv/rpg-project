using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 管理敵人血量 + 經驗量

public class EnemyHealthManager : MonoBehaviour {

    public int MaxHealth;
    public int CurrentHealth;

    private PlayerStats thePlayerStats;     // 玩家經驗值系統

    public int expToGive;                   // 敵人死掉的經驗值

    public string enemyQuestName;
    private QuestManager theQM;

    // Use this for initialization
    void Start()
    {
        CurrentHealth = MaxHealth;

        thePlayerStats = FindObjectOfType<PlayerStats>();
        theQM = FindObjectOfType<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            theQM.enemyKilled = enemyQuestName;

            Destroy(gameObject);

            thePlayerStats.AddExperience(expToGive);    // 給玩家加經驗
        }
    }

    public void HurtEnemy(int damageToGive)             // 敵人受傷
    {
        CurrentHealth -= damageToGive;
    }

    public void SetMaxHealth()                          // 使滿血
    {
        CurrentHealth = MaxHealth;
    }
}
