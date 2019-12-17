using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpUi : MonoBehaviour
{

	public Text HPText;
	public EnemyHealthManager Monsterhealth;

	// Update is called once per frame
	void Update()
	{
		HPText.text = Monsterhealth.CurrentHealth + " / " + Monsterhealth.MaxHealth;
	}
}
