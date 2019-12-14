using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 判斷 "敵人" 是否擊中玩家

public class HurtPlayer : MonoBehaviour {
    
    public int damageToGive;                // 武器攻擊力
    private int currentDamage;              // 總攻擊力
    public GameObject damageNumber;         // 顯示傷害值效果
    public bool destroyOnTrigger = false;         // 使自身碰撞後消失

    private PlayerStats thePS;

    // Use this for initialization
    void Start () {
		thePS = FindObjectOfType<PlayerStats>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            currentDamage = damageToGive - thePS.currentDefence; 
            if ( currentDamage < 0)
            {
                currentDamage = 0;
            }

            other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(currentDamage);

            // 建立新的物件( Object 物件, Vector3 位置, Quaternion 旋轉 ) ; rotation有四格, Euler函數可以變成只給XYZ ( 我也不懂
            var clone = (GameObject)Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;         // 給定傷害值
            
        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            currentDamage = damageToGive - thePS.currentDefence;
            if (currentDamage < 0)
            {
                currentDamage = 0;
            }

            other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(currentDamage);

            // 建立新的物件( Object 物件, Vector3 位置, Quaternion 旋轉 ) ; rotation有四格, Euler函數可以變成只給XYZ ( 我也不懂
            var clone = (GameObject)Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;         // 給定傷害值

            if (destroyOnTrigger)
            {
                Destroy(transform.gameObject);
            }
        }
    }
}
