﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupbottle : MonoBehaviour
{
	private BagInventory inventory;
	public GameObject itemButton; 
	//public GameObject effect;

	private void Start()
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<BagInventory>();
	}

	private void OnTriggerEnter2D(Collider2D other)	{
		if (other.CompareTag("Player")) { // 碰撞的是玩家
			for (int i = 0; i < inventory.slots.Length; i++) {
				if (inventory.isFull[i] == false) { 
					//Instantiate(effect, transform.position, Quaternion.identity);
					inventory.isFull[i] = true;
					Instantiate(itemButton, inventory.slots[i].transform, false);// 產生UI圖片
					Destroy(gameObject);
					break;
				} // if
			} // for
		} // of

	}
}