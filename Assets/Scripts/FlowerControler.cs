using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowerControler : MonoBehaviour {

	private GameObject playerUnit;      //獲取玩家單位
    private Animator thisAnimator;      //自身動畫組件
	public GameObject enemy3D;          //自己的3D物件
	public GameObject attackObject;     //攻擊的特效(咬咬 射子彈)
	public float alertRadius;           //警戒半徑，玩家進入後怪物會發出警告，並一直面朝玩家
	public float defendRadius;          //自衛半徑，玩家進入後怪物會追擊玩家，當距離<攻擊距離則會發動攻擊（或者觸發戰鬥）

	public float attackRange;           //攻擊距離
	public float shootingSpeed;         //射擊速度

	private Rigidbody2D myRigidbody;

	public float attackCDTime;                  //攻擊間隔時間
	private float lastAttackTime;

	private float distanceToPlayer;             //怪物與玩家的距離

	void Start()
	{
		playerUnit = GameObject.FindGameObjectWithTag("Player3D");
		thisAnimator = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();

		enemy3D = FindClosestEnemy3D();

		//檢查並修正怪物設置
		//1. 自衛半徑不大於警戒半徑，否則就無法觸發警戒狀態，直接開始追擊了
		defendRadius = Mathf.Min(alertRadius, defendRadius);
        //2. 攻擊距離不大於自衛半徑，否則就無法觸發追擊狀態，直接開始戰鬥了
        attackRange = Mathf.Min(defendRadius, attackRange);
        
	}

	/// <summary>
	/// 找出最屬於自己的3D物件
	/// </summary>
	/// <returns>  最近的tag有"Enemy3D"的3D物件  </returns>
	GameObject FindClosestEnemy3D()
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Enemy3D");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos)
		{
			if (go != gameObject)
			{
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance)
				{
					closest = go;
					distance = curDistance;
				}
			}
		}
		return closest;
	}

	void Update()
	{
		distanceToPlayer = Vector2.Distance(playerUnit.transform.position, transform.position);
		if (distanceToPlayer < attackRange)
		{
			enemy3D.GetComponent<AgentScript>().MoveAgent(transform.position, 0f);

			if (Time.time - lastAttackTime > attackCDTime)
			{
				lastAttackTime = Time.time;

				var clone = (GameObject)Instantiate(attackObject, transform.position, Quaternion.Euler(Vector3.zero));
				Vector2 aimsDir = Vector2.zero;
				aimsDir.x = Mathf.Clamp(playerUnit.transform.position.x - transform.position.x, -1, 1);
				aimsDir.y = Mathf.Clamp(playerUnit.transform.position.y - transform.position.y, -1, 1);

				clone.GetComponent<Rigidbody2D>().velocity = new Vector2(aimsDir.x * shootingSpeed, aimsDir.y * shootingSpeed);
			}
		}


	}


}
