using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {

    public Text moneyText;
    public int currentGold;

	// Use this for initialization
	void Start () {
		
        PlayerPrefs.SetInt("CurrentMoney", 5000);

        if(PlayerPrefs.HasKey("CurrentMoney"))
        {
            currentGold = PlayerPrefs.GetInt("CurrentMoney");
        }
        else{
            currentGold = 5000;
            PlayerPrefs.SetInt("CurrentMoney", 5000);
        }

        moneyText.text = "Money: " + currentGold;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddMoney(int goldToAdd)
    {
        currentGold += goldToAdd;
        PlayerPrefs.SetInt("CurrentMoney", currentGold);
        moneyText.text = "Money: " + currentGold;
    }
}
