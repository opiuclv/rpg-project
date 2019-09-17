using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    public float moveSpeed;
    public string statsUpdateText;          // 更新的狀態文字
    public Text displayText;                // 顯示的數字

    void Update()
    {
        displayText.text = "" + statsUpdateText;     // 給定顯示的文字 ; 給定顯示的位置 ( 慢慢向上飄
        transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);
    }
}
