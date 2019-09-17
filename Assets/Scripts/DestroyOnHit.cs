using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour {

    public float onHitTime;
    
    void OnTriggerEnter2D(Collider2D other)         // 觸發碰撞事件
    {
        if ( other.gameObject.tag != "Player")
        {
            onHitTime -= Time.deltaTime;            // 短暫時間的倒數
        
            if (onHitTime <= 0)                     // 時間到
                Destroy(gameObject);                // 刪除此物件
        }
    }
}
