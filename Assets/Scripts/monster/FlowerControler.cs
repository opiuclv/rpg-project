using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowerControler : MonoBehaviour
{
    private Vector2 move;
    private Vector3 pos3D;
    
    private Animator thisAnimator;      //自身動畫組件
    private Vector3 initialPosition;    //初始位置
    public GameObject enemy3D;          //自己的3D物件

    public float wanderRadius;          //遊走半徑，移動狀態下，如果超出遊走半徑會返回出生位置
    public float chaseRadius;           //追擊半徑，當怪物超出追擊半徑後會放棄追擊，返回追擊起始位置
    
    public float walkSpeed;             //移動速度

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
    public float actRestTime;                   //更換待機指令的間隔時間
    private float lastActTime;                  //最近一次指令時間
    private float animationTime;                //最近一次移動的動畫開始時間
    
    private float distanceToInitial;            //怪物與初始位置的距離
    private float distanceToStartPoint;         //怪物與開始移動的位置的距離
    private float distanceToTargetPos;

    private Vector3 startPosition;              //開始移動的座標
    private Vector3 targetPosition;             //目標的座標

    private bool is_Warned = false;
    private bool is_Walking = false;

    void Start()
    {
        thisAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        enemy3D = FindClosestEnemy3D();

        //保存初始位置信息
        initialPosition = gameObject.GetComponent<Transform>().position;

        //檢查並修正怪物設置
        //遊走半徑不大於追擊半徑，否則怪物可能剛剛開始追擊就返回出生點
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
    
    void Update()
    {

        switch (currentState)
        {
            //待機狀態（原地呼吸），等待actRestTime後重新隨機指令
            case MonsterState.STAND:
                if (Time.time - lastActTime > actRestTime)
                {
                    RandomAction(); //隨機切換指令
                }

                break;

            //待機狀態2（觀察），等待actRestTime後重新隨機指令
            case MonsterState.CHECK:
                if (Time.time - lastActTime > actRestTime)
                {
                    RandomAction();
                }
                
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
                
        }

        if (Time.time - animationTime > thisAnimator.GetCurrentAnimatorStateInfo(0).length)     // 如果還在走路或跑步就持續觸發動作
        {
            if (is_Walking)
            {
                thisAnimator.SetTrigger("Walk");
                animationTime = Time.time;
            }
        }
    }

    /// <summary>
    /// 跟著3D物件移動
    /// </summary>
    private void LateUpdate()
    {
        pos3D = enemy3D.transform.position;
        transform.position = new Vector3(pos3D.x, pos3D.y, transform.position.z);
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
    /// 遊走狀態檢測，檢測敵人距離及遊走是否越界
    /// </summary>
    void WanderRadiusCheck()
    {
        distanceToInitial = Vector2.Distance(transform.position, initialPosition);
        distanceToStartPoint = Vector2.Distance(transform.position, startPosition);
        distanceToTargetPos = Vector2.Distance(transform.position, targetPosition);

        if (distanceToStartPoint > wanderRadius || distanceToTargetPos < 0.5f)        // 一次不給走太遠距離
        {
            enemy3D.GetComponent<AgentScript>().MoveAgent(transform.position, 1f);
            is_Walking = false;
        }

        if (distanceToInitial > chaseRadius)
        {
            currentState = MonsterState.RETURN;
            is_Walking = false;
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
            enemy3D.GetComponent<AgentScript>().MoveAgent(transform.position, 1f);
            RandomAction();
        }
    }
    
}
