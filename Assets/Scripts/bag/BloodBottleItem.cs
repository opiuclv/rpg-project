using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBottleItem : MonoBehaviour
{
	private PlayerHealthManager thePlayerHealth;
	public int BloodBoost; // 增加多少血量
	private PlayerStats thePS;


	private void Start()
	{
		thePlayerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealthManager>();
	}

	public void Use(){
		thePlayerHealth.PlayerAddBlood(BloodBoost); // 補血
		Destroy (gameObject);
	}

}
