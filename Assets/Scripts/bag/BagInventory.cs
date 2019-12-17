using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagInventory : MonoBehaviour
{
	public int[] isFull; // 判斷每格藥水背包是否滿
	public GameObject[] slots; // 每格藥水背包


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (isFull[0] == 1 )
                slots[0].GetComponentInChildren<BloodBottleItem>().Use();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (isFull[1] == 1)
                slots[1].GetComponentInChildren<BloodBottleItem>().Use();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            if (isFull[2] == 1)
                slots[2].GetComponentInChildren<BloodBottleItem>().Use();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            if (isFull[3] == 1)
                slots[3].GetComponentInChildren<BloodBottleItem>().Use();
        }

    }
   
}
