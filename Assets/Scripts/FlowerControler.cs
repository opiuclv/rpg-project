using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowerControler : MonoBehaviour
{

    public Vector2 aimsDir;
    public Vector2 attackPos;

    private GameObject playerUnit;      //獲取玩家單位
    private Animator thisAnimator;      //自身動畫組件
    private Vector3 initialPosition;    //初始位置
    public GameObject enemy3D;          //自己的3D物件
    public GameObject attackObject;     //攻擊的特效(咬咬 射子彈)
    
    public float alertRadius;           //警戒半徑，玩家進入後怪物會發出警告，並一直面朝玩家
    public float attackRange;           //攻擊距離
    public float shootingSpeed;         //射擊速度

    private Rigidbody2D myRigidbody;

    public enum MonsterState
    {
        STAND,              //原地呼吸
        CHECK,              //原地觀察
        WALK,               //移動
        WARN,               //盯著玩家
        CHASE,              //追擊玩家
        RETURN,             //超出追擊範圍後返回
        ATTACK              //攻擊玩家
    }
    public MonsterState currentState = MonsterState.STAND;         //默認狀態為原地呼吸

    public float[] actionWeight = { 1000, 1000 };             //設置待機時各種動作的權重，順序依次為呼吸、觀察、移動
    public float actRestTime;                   //更換待機指令的間隔時間
    private float lastActTime;                  //最近一次指令時間

    public float attackCDTime;                  //攻擊間隔時間
    private float lastAttackTime;

    private float distanceToPlayer;             //怪物與玩家的距離
    private float targetDistance;               //怪物與目標的距離
    private Vector3 targetPosition;             //目標的座標

    private bool is_Warned = false;
    private bool canSwitchState = true;         // 是否可以切換狀態
    private float lastSwitchStateTime;          // 最近一次切換狀態時間
    private float switchStateDelay = 1.25f;     // 切換狀態延遲

    void Start()
    {
        playerUnit = GameObject.FindGameObjectWithTag("Player3D");
        thisAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        enemy3D = FindClosestEnemy3D();

        //保存初始位置信息
        initialPosition = gameObject.GetComponent<Transform>().position;

        //檢查並修正怪物設置
        //攻擊距離不大於警戒半徑
        attackRange = Mathf.Min(alertRadius, attackRange);

        //隨機一個待機動作

        RandomAction();
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

    /// <summary>
    /// 根據權重隨機待機指令
    /// </summary>
    void RandomAction()
    {
        myRigidbody.velocity = Vector2.zero;
        lastActTime = Time.time;            //更新行動時間

        //根據權重 隨機選擇待機,觀察,遊走模式
        float number = Random.Range(0, actionWeight[0] + actionWeight[1]);
        if (number <= actionWeight[0])
        {
            currentState = MonsterState.STAND;
            thisAnimator.SetTrigger("Stand");
        }
        else if (actionWeight[0] < number && number <= actionWeight[0] + actionWeight[1])
        {
            currentState = MonsterState.CHECK;
            thisAnimator.SetTrigger("Check");
        }
    }

    void Update()
    {

        enemy3D.GetComponent<AgentScript>().MoveAgent(initialPosition, 1f);

        if (!canSwitchState && Time.time - lastSwitchStateTime > switchStateDelay)     // 切換狀態延遲
            canSwitchState = true;

        if (canSwitchState)
        {
            switch (currentState)
            {
                //待機狀態（原地呼吸），等待actRestTime後重新隨機指令
                case MonsterState.STAND:
                    if (Time.time - lastActTime > actRestTime)
                    {
                        RandomAction(); //隨機切換指令
                    }

                    EnemyDistanceCheck();

                    break;

                //待機狀態2（觀察），等待actRestTime後重新隨機指令
                case MonsterState.CHECK:
                    if (Time.time - lastActTime > actRestTime)
                    {
                        RandomAction();
                    }

                    EnemyDistanceCheck();

                    break;
                    
                //警戒狀態，播放一次警告動畫（聲音）
                case MonsterState.WARN:
                    canSwitchState = false;             // 確保警告動畫能先播完
                    lastSwitchStateTime = Time.time;
                    if (!is_Warned)
                    {
                        thisAnimator.SetTrigger("Warn");
                        is_Warned = true;
                    }

                    WarningCheck();
                    break;
                    
                case MonsterState.ATTACK:
                    
                    if (Time.time - lastAttackTime > attackCDTime)
                    {
                        lastAttackTime = Time.time;
                        thisAnimator.SetTrigger("Attack");

                        var clone = (GameObject)Instantiate(attackObject, transform.position, Quaternion.Euler(Vector3.zero));
                        aimsDir = (playerUnit.transform.position - transform.position);
                        aimsDir.Normalize();

                        clone.GetComponent<Rigidbody2D>().velocity = new Vector2(aimsDir.x * shootingSpeed, aimsDir.y * shootingSpeed);
                    }

                    EnemyDistanceCheck();
                    break;
            }
        }
    }

    /// <summary>
    /// 跟著3D物件移動
    /// </summary>
    private void LateUpdate()
    {
        transform.position = new Vector3(enemy3D.transform.position.x, enemy3D.transform.position.y, transform.position.z);
    }

    /// <summary>
    /// 原地呼吸、觀察狀態的檢測
    /// </summary>
    void EnemyDistanceCheck()
    {
        distanceToPlayer = Vector2.Distance(playerUnit.transform.position, transform.position);
        if (distanceToPlayer < attackRange)
        {
            currentState = MonsterState.ATTACK;
        }
        else if (distanceToPlayer < alertRadius)
        {
            currentState = MonsterState.WARN;
        }
    }

    /// <summary>
    /// 警告狀態下的檢測，用於啟動追擊及取消警戒狀態
    /// </summary>
    public void WarningCheck()
    {
        distanceToPlayer = Vector2.Distance(playerUnit.transform.position, transform.position);
        if (distanceToPlayer < alertRadius)
        {
            if (distanceToPlayer < attackRange)         // 進入攻擊半徑 進入攻擊模式
            {
                is_Warned = false;
                currentState = MonsterState.ATTACK;
            }
        }

        if (distanceToPlayer > alertRadius)
        {
            is_Warned = false;
            RandomAction();
        }
    }
    
}
