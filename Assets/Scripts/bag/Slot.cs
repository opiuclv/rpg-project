using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{

	private BagInventory inventory;
	public int i;


	private void Start()
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<BagInventory>();
	}

	private void Update()
	{
		if (inventory.slots[i].transform.childCount <= 0) {
			inventory.isFull [i] = false;
		}
	}

	public void DropItem(){ // 將藥瓶丟棄
		foreach( Transform child in transform ){
			child.GetComponent<Spawn>().SpawnDroppedItem();
			GameObject.Destroy (child.gameObject);
		}
	}


}
