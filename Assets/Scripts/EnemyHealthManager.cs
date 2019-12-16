using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 管理敵人血量 + 經驗量

public class EnemyHealthManager : MonoBehaviour {

	public float MaxHealth; // 一開始怪物血量
	public float CurrentHealth;

    private PlayerStats thePlayerStats;     // 玩家經驗值系統
	public GameObject[] goldToGive;           // 敵人死掉的掉落物
	private int rand;
    public int expToGive;                   // 敵人死掉的經驗值

	public Image healthBar; // 怪物血條

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

			rand = Random.Range (0, goldToGive.Length);
			var clone = (GameObject)Instantiate(goldToGive[rand], transform.position, Quaternion.Euler(Vector3.zero));
            Destroy(gameObject);

            thePlayerStats.AddExperience(expToGive);    // 給玩家加經驗
        }
    }

    public void HurtEnemy(int damageToGive)             // 敵人受傷
    {
        CurrentHealth -= damageToGive;
		healthBar.fillAmount = CurrentHealth / MaxHealth;

    }

    public void SetMaxHealth()                          // 使滿血
    {
        CurrentHealth = MaxHealth;
    }
}
