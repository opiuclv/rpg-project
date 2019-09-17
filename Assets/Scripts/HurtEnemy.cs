using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 判斷 "武器" 是否擊中敵人 + 攻擊特效 + 傷害量顯示 + 攻擊力計算

public class HurtEnemy : MonoBehaviour {

    public int damageToGive;                // 武器攻擊力
    private int currentDamage;              // 總攻擊力 ( 傷害值
    public GameObject damageBurst;          // 濺血粒子效果
    public Transform hitPoint;              // 命中點
    public GameObject damageNumber;         // 顯示傷害值效果

    private PlayerStats thePS;

	// Use this for initialization
	void Start () {
        thePS = FindObjectOfType<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // Destroy(other.gameObject);
            currentDamage = damageToGive + thePS.currentAttack;                         // 計算傷害值

            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(currentDamage);
            Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
            // 建立新的物件( Object 物件, Vector3 位置, Quaternion 旋轉 ) ; rotation有四格, Euler函數可以變成只給XYZ ( 我也不懂

            var clone = (GameObject) Instantiate(damageNumber, hitPoint.position, Quaternion.Euler (Vector3.zero) );
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;         // 給定傷害值
        }
    }
}
