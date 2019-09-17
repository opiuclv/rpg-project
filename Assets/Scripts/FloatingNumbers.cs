using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 控制傷害數字的顯示

public class FloatingNumbers : MonoBehaviour {

    public float moveSpeed;
    public int damageNumber;        // 傷害值
    public Text displayNumber;      // 顯示的數字

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        displayNumber.text = "" + damageNumber;     // 給定顯示的傷害值 ; 給定顯示的位置 ( 慢慢向上飄
        transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);
	}
}
