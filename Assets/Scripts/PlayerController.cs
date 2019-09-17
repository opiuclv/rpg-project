using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 控制玩家移動 + 動畫 ( 移動 + 面向 + 停下 + 攻擊 ) + 斜向速度微調 + 處理Player重疊問題

public class PlayerController : MonoBehaviour {

    private Color originalColor;                // 原始顏色
    private float originalSpeed;                // 原始速度
    private bool chargeReady = true;            // 可以狂暴化
    private int chargeAttack;                   // 狂暴加成攻擊力
    private bool chargeOver = true;

    public float chargeTime;                    // 狂暴化時間 & counter
    private float chargeTimeCounter;
    public float chargeCDTime;                  // 狂暴化冷卻時間 & counter
    private float chargeCDCounter;
    private SpriteRenderer playerSprite;        // 抓Player Sprite 用來變色
    private PlayerStats thePS;                  // 用來更新能力值

    public int goldCost;                // 單次錢鏢消費
    private bool shooting = false;      // 射擊中
    public GameObject coinbullet;       // 錢鏢物件
    public float shootingSpeed;         // 錢鏢速度
    private MoneyManager theMM;         // 管理錢系統
    public GameObject statsUpdateText;  // 角色狀態更新文字

    public float moveSpeed;
    private float currentMoveSpeed;
    // public float diagonalMoveModifier;  // 對角移動速度修改

    private Animator anim;              // 動畫變數
    private Rigidbody2D myRigidbody;    // 碰撞變數 ( 剛體2D

    private bool playerMoving;          // 是否移動中 ( for animation
    public Vector2 lastMove;            // 最後面向 ( x, y )
    private Vector2 moveInput;          // 輸入方向

    private static bool playerExists;   // 是否已存在相同player ( avoid duplicate player

    private bool attacking;             // 是否攻擊中
    public float attackTime;            // 攻擊時間 & counter ( 揮劍 or 斧頭 時間長度不一
    private float attackTimeCounter;
    
    public string startPoint;           // 切換場景的起始點

    public bool canMove ;                // 是否可移動的狀態

    private SFXMnager theSFXM;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        originalColor = playerSprite.color;
        originalSpeed = moveSpeed;

        theSFXM = FindObjectOfType<SFXMnager>();
        theMM = FindObjectOfType<MoneyManager>();
        thePS = FindObjectOfType<PlayerStats>();

        if (!playerExists)
        {                                               // 沒有相同player  ( bool預設為false
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);    // 切換畫面不改變gameObject參數
        }
        else
        {                                               // 已存在相同player
            Destroy(gameObject);                        // 刪除當前gameObject
        }

        canMove = true;
        lastMove = new Vector2(0, -1f);
    }
	
	// Update is called once per frame
	void Update () {

        playerMoving = false;

        if (Input.GetKeyDown(KeyCode.K) && chargeReady)                            // 狂暴化
        {
            chargeTimeCounter = chargeTime;                 // 狂暴中
            chargeCDCounter = chargeCDTime;
            chargeReady = false;
            chargeOver = false;

            chargeAttack = (int)(thePS.currentAttack * 0.5f);
            thePS.PowerUp(chargeAttack);                    // 加攻擊 & 變色 & 加速
            playerSprite.color = new Color(1f, 0.6f, 0.6f, 1f);
            moveSpeed = moveSpeed * 1.5f;
        }

        if (chargeTimeCounter > 0)
            chargeTimeCounter -= Time.deltaTime;
        if (chargeTimeCounter <= 0 && !chargeOver)                         // 狂暴化結束
        {
            thePS.PowerDown(chargeAttack);
            playerSprite.color = originalColor;
            moveSpeed = originalSpeed;
            chargeOver = true;
        }

        if (chargeCDCounter > 0)
            chargeCDCounter -= Time.deltaTime;
        if (chargeCDCounter <= 0)
            chargeReady = true;

        if (!canMove)
        {
            myRigidbody.velocity = Vector2.zero;
            return;
        }

        if (!attacking )
        {

            /* if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)    // 按左右鍵 ( 實為 +1, -1 )
            {   // X速度為+-1 *moveSpeed, Y速度維持原樣, 紀錄最後方向
                // transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * currentMoveSpeed, myRigidbody.velocity.y);
                playerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            }

            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)        // 按上下鍵
            {   // X速度維持原樣, Y速度為+-1 *moveSpeed, 紀錄最後方向
                // transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * currentMoveSpeed);
                playerMoving = true;
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)    // 沒有按左右鍵
            {   // X速度歸零, Y速度維持原樣
                myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
            }

            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)        // 沒有按上下鍵
            {   // X速度維持原樣, Y速度歸零
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f);
            } */

            // 輸入方向並柔和化 ( 斜向速度自動調整
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            if (moveInput != Vector2.zero)                          // 正在移動
            {
                myRigidbody.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
                playerMoving = true;
                lastMove = moveInput;
            }
            else
            {
                myRigidbody.velocity = Vector2.zero;
            }
            

            // GetKey 長按會持續作用 ; GetKeyDown 只會做一次動作 ( Trigger )
            if (Input.GetKeyDown(KeyCode.J))                            // 揮劍
            {
                attackTimeCounter = attackTime;         // 攻擊中
                myRigidbody.velocity = Vector2.zero;    // 停止移動
                attacking = true;

                anim.SetBool("Attack", true);           // 設定動畫跟音效
                theSFXM.playerAttack.Play();
            }

            if (Input.GetKeyDown(KeyCode.U) && !shooting)               // 丟錢鏢
            {
                if (goldCost <= theMM.currentGold)      // 錢夠的話
                {
                    attackTimeCounter = attackTime;     // 攻擊中
                    shooting = true;
                    // attacking = true;
                    // myRigidbody.velocity = Vector2.zero;
                                                        // 產生錢鏢 給定動能
                    var clone = (GameObject)Instantiate(coinbullet, transform.position, Quaternion.Euler(Vector3.zero));
                    clone.GetComponent<Rigidbody2D>().velocity = new Vector2(lastMove.x * shootingSpeed, lastMove.y * shootingSpeed);

                    theMM.MinusMoney(goldCost);         // 扣錢
                }

                else
                {                                       // 顯示金錢不足文字
                    var clone = (GameObject)Instantiate(statsUpdateText, transform.position, Quaternion.Euler(Vector3.zero));
                    clone.GetComponent<FloatingText>().statsUpdateText = "Not enough Gold !";
                }
                theSFXM.playerAttack.Play();            // 音效
            }
            

            // Mathf.Abs() 取絕對值 ; if ( 正在左或右 且 正在上或下 )
            /* if (Mathf.Abs (Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
            {
                currentMoveSpeed = moveSpeed * diagonalMoveModifier;
            }
            else
            {
                currentMoveSpeed = moveSpeed;
            } */
        }

        if (attackTimeCounter > 0)
            attackTimeCounter -= Time.deltaTime;
        if (attackTimeCounter <= 0)         // 回復正常狀態
        {
            attacking = false;
            shooting = false;
            anim.SetBool("Attack", false);
        }


        // variables for animation
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }
}
