using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 玩家經驗值系統

public class PlayerStats : MonoBehaviour {

    public int currentLevel;            // 當前等級
    public int currentExp;              // 當前經驗值
    public int[] toLevelUp;             // 各等級所需經驗

    public int[] HPLevels;              // 各等級基礎HP Attack Defence
    public int[] AttackLevels;
    public int[] defenceLevels;

    public int currentHP;               // 目前基礎HP Attack Defence
    public int currentAttack;
    public int currentDefence;

    private PlayerHealthManager thePlayerHealth;

    // Use this for initialization
    void Start() {
        currentHP = HPLevels[1];
        currentAttack = AttackLevels[1];
        currentDefence = defenceLevels[1];

        thePlayerHealth = FindObjectOfType<PlayerHealthManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentExp >= toLevelUp[currentLevel])      // 是否升等
        {
            LevelUp();
        }
	}


    public void PowerUp(int bonusAttack)                // 能力上升
    {
        currentAttack = currentAttack + bonusAttack;         // 攻擊力上升
    }


    public void PowerDown(int bonusAttack)              // 能力下降
    {
        currentAttack = currentAttack - bonusAttack;         // 攻擊力下降
    }


    public void AddExperience(int experienceToAdd)      // 玩家經驗增加
    {
        currentExp += experienceToAdd;
    }


    public void LevelUp()                               // 升級 HP Attack Defence
    {
        currentLevel++;
        currentHP = HPLevels[currentLevel];
        thePlayerHealth.playerMaxHealth = currentHP;
        thePlayerHealth.playerCurrentHealth += currentHP - HPLevels[currentLevel - 1];
        // 加上升等增加的血量 ( 30/50 -> 35/55 )

        currentAttack = AttackLevels[currentLevel];
        currentDefence = defenceLevels[currentLevel];
    }
}
