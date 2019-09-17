using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 金錢系統

public class MoneyManager : MonoBehaviour {

    public Text moneyText;
    public int currentGold;

	// Use this for initialization
	void Start () {

        if (PlayerPrefs.HasKey("CurrentMoney"))                     // 如果已經有"CurrentMoney"欄位 ( PlayerPrefs不懂是啥 特別的儲存空間?
        {
            currentGold = PlayerPrefs.GetInt("CurrentMoney");       // 從CurrentMoney欄位抓當前金錢
        }
        else
        {
            currentGold = 0;                                        // 尚未有"CurrentMoney"欄位
            PlayerPrefs.SetInt("CurrentMoney", 0);                  // 設定一個int欄位"CurrentMoney" 給定數值為0
        }

        moneyText.text = "Gold: " + currentGold;                    // 顯示當前金錢
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddMoney(int goldToAdd)                             // 加錢
    {
        currentGold += goldToAdd;
        PlayerPrefs.SetInt("CurrentMoney", currentGold);
        moneyText.text = "Gold: " + currentGold;
    }


    public void MinusMoney(int goldToMinus)                         // 扣錢
    {
        currentGold -= goldToMinus;
        PlayerPrefs.SetInt("CurrentMoney", currentGold);
        moneyText.text = "Gold: " + currentGold;
    }
}
