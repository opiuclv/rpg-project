using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WolfControler : MonoBehaviour
{
    private Vector2 move;
    private Vector3 pos3D;

    private GameObject playerUnit;      //獲取玩家單位
    private Animator thisAnimator;      //自身動畫組件
    private Vector3 initialPosition;    //初始位置
    public GameObject enemy3D;          //自己的3D物件
    public GameObject attackObject;     //攻擊的特效(咬咬 射子彈)
    public GameObject attackObject_2;   //攻擊的特效(咬咬 射子彈)

    public float wanderRadius;          //遊走半徑，移動狀態下，如果超出遊走半徑會返回出生位置
    public float alertRadius;           //警戒半徑，玩家進入後怪物會發出警告，並一直面朝玩家
    public float defendRadius;          //自衛半徑，玩家進入後怪物會追擊玩家，當距離<攻擊距離則會發動攻擊（或者觸發戰鬥）
    public float chaseRadius;           //追擊半徑，當怪物超出追擊半徑後會放棄追擊，返回追擊起始位置

    public float attackRange;           //攻擊距離
    public float attackRange_2;         //遠距攻擊距離
    public float walkSpeed;             //移動速度
    public float runSpeed;              //跑動速度
    public float dashSpeed;             //衝刺速度
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
        ATTACK_1,           //攻擊玩家
        ATTACK_2
    }
    public MonsterState currentState = MonsterState.STAND;          //默認狀態為原地呼吸

    public float[] actionWeight = { 3000, 3000, 4000 };             //設置待機時各種動作的權重，順序依次為呼吸、觀察、移動
    public float[] attackWeight = { 1000, 2000 };                   //攻擊動作權重
    public float actRestTime;                   //更換待機指令的間隔時間
    private float lastActTime;                  //最近一次指令時間
    private float animationTime;                //最近一次移動的動畫開始時間

    public float attackCDTime;                  //攻擊間隔時間
    private float lastAttackTime;

    private float distanceToPlayer;             //怪物與玩家的距離
    private float distanceToInitial;            //怪物與初始位置的距離
    private float distanceToStartPoint;         //怪物與開始移動的位置的距離
    private float targetDistance;               //怪物與目標的距離
    private Vector3 startPosition;              //開始移動的座標
    private Vector3 targetPosition;             //目標的座標

    private bool is_Warned = false;
    private bool is_Walking = false;
    private bool is_Running = false;
    private bool is_Dashing = false;
    private bool canSwitchState = true;         // 是否可以切換狀態
    private float lastSwitchStateTime;          // 最近一次切換狀態時間
    public float switchStateDelay = 0.5f;       // 切換狀態延遲

    void Start()
    {
        playerUnit = GameObject.FindGameObjectWithTag("Player3D");
        thisAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        enemy3D = FindClosestEnemy3D();

        //保存初始位置信息
        initialPosition = gameObject.GetComponent<Transform>().position;

        //檢查並修正怪物設置
        //1. 自衛半徑不大於警戒半徑，否則就無法觸發警戒狀態，直接開始追擊了
        defendRadius = Mathf.Min(alertRadius, defendRadius);
        //2. 攻擊距離不大於自衛半徑，否則就無法觸發追擊狀態，直接開始戰鬥了
        attackRange_2 = Mathf.Min(defendRadius, attackRange_2);

        attackRange = Mathf.Min(attackRange_2, attackRange);
        //3. 遊走半徑不大於追擊半徑，否則怪物可能剛剛開始追擊就返回出生點
        wanderRadius = Mathf.Min(chaseRadius, wanderRadius);

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
        float number = Random.Range(0, actionWeight[0] + actionWeight[1] + actionWeight[2]);
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
        if (actionWeight[0] + actionWeight[1] < number && number <= actionWeight[0] + actionWeight[1] + actionWeight[2])
        {
            currentState = MonsterState.WALK;
            //附近隨機一個座標
            Vector2 targetPosOffset = new Vector2(Random.Range(-wanderRadius, wanderRadius), Random.Range(-wanderRadius, wanderRadius));
            targetPosition = new Vector3(transform.position.x + targetPosOffset.x, transform.position.y + targetPosOffset.y, transform.position.z);
        }
    }

    void RandomAttack()
    {
        Vector2 aimsDir = (playerUnit.transform.position - transform.position);
        aimsDir.Normalize();
        Vector3 aims = aimsDir;

        float number = Random.Range(0, attackWeight[0] + attackWeight[1]);
        if (number < attackWeight[0])
        {
            var clone = (GameObject)Instantiate(attackObject_2, transform.position + aims * 2f, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<Rigidbody2D>().velocity = new Vector2(aimsDir.x * shootingSpeed, aimsDir.y * shootingSpeed);
        }
        else if (number < attackWeight[0] + attackWeight[1])
        {
            targetPosition = new Vector3(playerUnit.transform.position.x, playerUnit.transform.position.y, transform.position.z);
            myRigidbody.velocity = aimsDir * dashSpeed;
            is_Dashing = true;
        }
    }

    void Update()
    {
        if (!canSwitchState && Time.time - lastSwitchStateTime > switchStateDelay)     // 切換狀態延遲
        {
            canSwitchState = true;
        }

        if (is_Dashing)
        {
            if (Vector3.Distance(transform.position, targetPosition) <= 0.5f)
            {
                myRigidbody.velocity = Vector2.zero;
                is_Dashing = false;
            }
        }

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

                //遊走狀態，根據狀態隨機時生成的目標位置移動
                case MonsterState.WALK:
                    if (Time.time - lastActTime > actRestTime)
                    {
                        RandomAction();
                    }

                    startPosition = transform.position;
                    enemy3D.GetComponent<AgentScript>().MoveAgent(targetPosition, walkSpeed);

                    if (!is_Walking)                    // 確保移動動畫在移動的時候會連續撥放
                    {
                        thisAnimator.SetTrigger("Walk");
                        animationTime = Time.time;
                        is_Walking = true;
                    }

                    UpdateLastMove(targetPosition);
                    WanderRadiusCheck();
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

                    UpdateLastMove(playerUnit.transform.position);
                    WarningCheck();
                    break;

                //追擊狀態，朝著玩家跑去
                case MonsterState.CHASE:

                    if (!is_Running)                    // 確保移動動畫在移動的時候會連續撥放
                    {
                        thisAnimator.SetTrigger("Run");
                        animationTime = Time.time;
                        is_Running = true;
                    }

                    enemy3D.GetComponent<AgentScript>().MoveAgent(playerUnit.transform.position, runSpeed);

                    UpdateLastMove(playerUnit.transform.position);
                    ChaseRadiusCheck();
                    break;

                //返回狀態，超出追擊範圍後返回出生位置
                case MonsterState.RETURN:

                    if (!is_Walking)
                    {
                        thisAnimator.SetTrigger("Walk");
                        animationTime = Time.time;
                        is_Walking = true;
                    }

                    enemy3D.GetComponent<AgentScript>().MoveAgent(initialPosition, walkSpeed);

                    UpdateLastMove(initialPosition);
                    ReturnCheck();
                    break;

                // 攻擊狀態 (近戰)
                case MonsterState.ATTACK_1:

                    is_Walking = false;
                    is_Running = false;

                    enemy3D.GetComponent<AgentScript>().MoveAgent(transform.position, 1f);

                    if (Time.time - lastAttackTime > attackCDTime)
                    {
                        lastAttackTime = Time.time;
                        thisAnimator.SetTrigger("Attack");

                        Vector2 aimsDir = (playerUnit.transform.position - transform.position);
                        aimsDir.Normalize();
                        Vector3 aims = aimsDir;

                        var clone = (GameObject)Instantiate(attackObject, transform.position + aims * 2f, Quaternion.Euler(Vector3.zero));
                    }

                    UpdateLastMove(playerUnit.transform.position);
                    EnemyDistanceCheck();
                    break;

                // 攻擊狀態 (遠距)
                case MonsterState.ATTACK_2:

                    is_Walking = false;
                    is_Running = false;

                    enemy3D.GetComponent<AgentScript>().MoveAgent(transform.position, 1f);

                    if (Time.time - lastAttackTime > attackCDTime)
                    {
                        lastAttackTime = Time.time;
                        thisAnimator.SetTrigger("Attack");

                        RandomAttack();
                    }

                    UpdateLastMove(playerUnit.transform.position);
                    EnemyDistanceCheck();
                    break;
            }
        }

        if (Time.time - animationTime > thisAnimator.GetCurrentAnimatorStateInfo(0).length)     // 如果還在走路或跑步就持續觸發動作
        {
            if (is_Walking)
            {
                thisAnimator.SetTrigger("Walk");
                animationTime = Time.time;
            }
            if (is_Running)
            {
                thisAnimator.SetTrigger("Run");
                animationTime = Time.time;
            }
        }
    }

    /// <summary>
    /// 跟著3D物件移動
    /// </summary>
    private void LateUpdate()
    {
        if (!is_Dashing)
        {
            pos3D = enemy3D.transform.position;
            transform.position = new Vector3(pos3D.x, pos3D.y, transform.position.z);
        }
        else
        {
            enemy3D.transform.position = new Vector3(transform.position.x, transform.position.y, enemy3D.transform.position.z);
        }
    }

    /// <summary>
    /// 更新最後面對的方向
    /// </summary>
    void UpdateLastMove(Vector3 targetPos)
    {
        move = (targetPos - transform.position);
        move.Normalize();

        thisAnimator.SetFloat("MoveX", move.x);
        thisAnimator.SetFloat("MoveY", move.y);
    }

    /// <summary>
    /// 原地呼吸、觀察狀態的檢測
    /// </summary>
    void EnemyDistanceCheck()
    {
        distanceToPlayer = Vector2.Distance(playerUnit.transform.position, transform.position);
        if (distanceToPlayer < attackRange)
        {
            currentState = MonsterState.ATTACK_1;
        }
        else if (distanceToPlayer < attackRange_2)
        {
            currentState = MonsterState.ATTACK_2;
        }
        else if (distanceToPlayer < defendRadius)
        {
            currentState = MonsterState.CHASE;
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
            if (distanceToPlayer < defendRadius)         // 進入防備半徑 進入追擊模式
            {
                is_Warned = false;
                currentState = MonsterState.CHASE;
            }
            else                                        // 慢慢朝向玩家移動
            {
                enemy3D.GetComponent<AgentScript>().MoveAgent(playerUnit.transform.position, 1f);
                UpdateLastMove(playerUnit.transform.position);
            }
        }

        if (distanceToPlayer > alertRadius)
        {
            is_Warned = false;
            RandomAction();
        }
    }

    /// <summary>
    /// 遊走狀態檢測，檢測敵人距離及遊走是否越界
    /// </summary>
    void WanderRadiusCheck()
    {
        distanceToPlayer = Vector2.Distance(transform.position, playerUnit.transform.position);
        distanceToInitial = Vector2.Distance(transform.position, initialPosition);
        distanceToStartPoint = Vector2.Distance(transform.position, startPosition);

        if (distanceToPlayer < attackRange)
        {
            is_Walking = false;
            currentState = MonsterState.ATTACK_1;
        }
        else if (distanceToPlayer < attackRange_2)
        {
            is_Walking = false;
            currentState = MonsterState.ATTACK_2;
        }
        else if (distanceToPlayer < defendRadius)
        {
            is_Walking = false;
            currentState = MonsterState.CHASE;
        }
        else if (distanceToPlayer < alertRadius)
        {
            is_Walking = false;
            currentState = MonsterState.WARN;
        }

        if (distanceToStartPoint > wanderRadius)        // 一次不給走太遠距離
        {
            enemy3D.GetComponent<AgentScript>().MoveAgent(transform.position, 1f);
            is_Walking = false;
        }
    }

    /// <summary>
    /// 追擊狀態檢測，檢測敵人是否進入攻擊範圍以及是否離開警戒範圍
    /// </summary>
    void ChaseRadiusCheck()
    {
        distanceToPlayer = Vector2.Distance(playerUnit.transform.position, transform.position);
        distanceToInitial = Vector2.Distance(transform.position, initialPosition);

        if (distanceToPlayer < attackRange)
        {
            enemy3D.GetComponent<AgentScript>().MoveAgent(transform.position, 1f);
            is_Running = false;
            currentState = MonsterState.ATTACK_1;
        }
        else if (distanceToPlayer < attackRange_2)
        {
            enemy3D.GetComponent<AgentScript>().MoveAgent(transform.position, 1f);
            is_Running = false;
            currentState = MonsterState.ATTACK_2;
        }
        //如果超出追擊範圍或者敵人的距離超出警戒距離就返回
        if (distanceToInitial > chaseRadius)
        {
            // myRigidbody.velocity = Vector2.zero;
            currentState = MonsterState.RETURN;
            is_Running = false;
        }
    }

    /// <summary>
    /// 超出追擊半徑，返回狀態的檢測，不再檢測敵人距離
    /// </summary>
    void ReturnCheck()
    {
        distanceToInitial = Vector2.Distance(transform.position, initialPosition);
        //如果已經接近初始位置，則隨機一個待機狀態
        if (distanceToInitial < 0.5f)
        {
            is_Walking = false;
            is_Running = false;
            enemy3D.GetComponent<AgentScript>().MoveAgent(transform.position, 1f);
            RandomAction();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (is_Dashing)
        {
            myRigidbody.velocity = Vector2.zero;
            is_Dashing = false;
        }
    }
}
